using System.IO;
using static RuleDConversion.Utils;

namespace RuleDConversion;
public partial class FrmStreamMemberUpdaate : Form
{
    static string OracleConnString;

    List<int> ruleNumbers = [];

    int selectedRuleStream;
    public FrmStreamMemberUpdaate()
    {
        InitializeComponent();

        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnection();
        Text = ocsb.DataSource;
        OracleConnString = ocsb.ConnectionString;

        Text = ocsb.DataSource;

        ddStreams.DataSource = GetRegionalRuleStreamList();

        ddStreams.DisplayMember = "Name";
        ddStreams.ValueMember = "Id";
    }

    void BtnTest_Click(object sender, EventArgs e)
    {
        string queryToRun = Utils.ModifyForRun(txtOracle.Text);
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        txtResults.Text = DataUtils.ExecuteTest(queryToRun, ruleNumber, OracleConnString);
    }

    void BtnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtOracle.Text))
        {
            string queryToRun = $"UD_RUNSQLD('{txtOracle.Text} ')";
            string currentSql = $"UD_RUNSQLD('{txtGoldenCalc.Text} ')";
            string ruleName = txtRuleName.Text;
            int ruleNumber = int.Parse(txtRuleNumber.Text);
            string ret = DataUtils.SaveChanges(true, currentSql, queryToRun, ruleName, ruleNumber, OracleConnString);
            txtResults.Text = ret;
        }
        GetRuleStreamRules(selectedRuleStream);
        FetchNext();

    }

    void SetStatus(string msg)
    {
        lblRuleSet.Text = msg;
        Application.DoEvents();
    }

    void GetRuleStreamRules(int streamId)
    {
        SetStatus($"Working on getting rules for stream {streamId}...");

        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        string ruleDSql = "";

        if (chkConvertOnly.Checked)
        {

            ruleDSql = @"SELECT d.F_RULE_NUMBER  
                        FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s,T_D_RULES d1
                        WHERE d.F_RULE_NUMBER = m.F_RULE_NUMBER
                        AND d.F_RULE_NUMBER = d1.F_RULE_NUMBER
                        AND NVL(d1.F_CONVERTED,0) = 0
                        AND m.F_GROUP_ID = s.F_GROUP_ID
                        AND s.F_STREAM_ID = :SID
                        ORDER BY 1";
        }
        else
        {
            ruleDSql = @"SELECT d.F_RULE_NUMBER  
                        FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s,T_D_RULES d1
                        WHERE d.F_RULE_NUMBER = m.F_RULE_NUMBER
                        AND d.F_RULE_NUMBER = d1.F_RULE_NUMBER
                        AND NVL(d1.F_REVIEWED,0) = 0
                        AND m.F_GROUP_ID = s.F_GROUP_ID
                        AND s.F_STREAM_ID = :SID
                        ORDER BY 1";
        }

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = ruleDSql,
            CommandType = CommandType.Text
        };

        cmd.Parameters.Clear();
        cmd.Parameters.Add(":SID", streamId);

        OracleDataReader dr = cmd.ExecuteReader();

        ruleNumbers.Clear();

        while (dr.Read())
        {
            ruleNumbers.Add(dr["F_RULE_NUMBER"] as int? ?? 0);
        }

        dr.Close();
        conn.Close();

        ruleNumbers = [.. ruleNumbers.OrderBy(r => r)];

        SetStatus($"Rule set: {ruleNumbers.Count}");
    }

    void GetNextRule(int ruleNumber)
    {
        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        string nextSQL = @"SELECT d.F_RULE_NUMBER , d.F_CALC AS GOLDENCALC,  d1.F_CALC_ORA AS ORASYNTAX, d.F_RULE_NAME,d.F_USER_UPDATED, d.F_DATE_CREATED
                            FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s , T_D_RULES d1
                            WHERE d1.F_RULE_NUMBER = d.F_RULE_NUMBER
                            ANd d.F_RULE_NUMBER = m.F_RULE_NUMBER
                            AND m.F_GROUP_ID = s.F_GROUP_ID
                            AND d.F_RULE_NUMBER = :RN
                            ORDER BY 1";

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = nextSQL,
            CommandType = CommandType.Text
        };

        cmd.Parameters.Clear();

        cmd.Parameters.Add("RN", ruleNumber);

        OracleDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
            txtRuleNumber.Text = (dr["F_RULE_NUMBER"] as int? ?? 0).ToString();
            txtGoldenCalc.Text = Utils.FormatQueryText(dr["GOLDENCALC"] as string);
            txtOracle.Text = Utils.FormatQueryText(dr["ORASYNTAX"] as string);
            txtRuleName.Text = dr["F_RULE_NAME"] as string;
            txtUserUpdated.Text = dr["F_USER_UPDATED"] as string;
            txtDateUpdated.Text = (dr["F_DATE_CREATED"] as DateTime? ?? default).ToString();
        }
        dr.Close();
        conn.Dispose();
    }


    void FetchNext()
    {
        txtRuleNumber.Text = "";
        txtGoldenCalc.Text = "";
        txtOracle.Text = "";
        txtDateUpdated.Text = "";
        txtResults.Text = "";
        txtRuleName.Text = "";
        txtUserUpdated.Text = "";

        lblUpdated.Visible = false;

        int ruleNumber = ruleNumbers.FirstOrDefault();
        if (ruleNumber > 0)
        {
            GetNextRule(ruleNumber);
        }
        else
        {
            SetStatus($"Rule count is 0!");
        }

        if (txtOracle.Text.Contains($"'RW-{ruleNumber}'"))
        {
            txtOracle.Text = txtOracle.Text.Replace($"'RW-{ruleNumber}'", "'RW-@RULEID'");
            lblUpdated.Visible = true;
        }

    }

    private void DDStreams_SelectedIndexChanged(object sender, EventArgs e)
    {
        RegionalStream item = (RegionalStream)ddStreams.SelectedItem;
        selectedRuleStream = item.Id;
        GetRuleStreamRules(selectedRuleStream);
        FetchNext();
    }

    private void BtnConverted_Click(object sender, EventArgs e)
    {
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        string ret = DataUtils.SaveChanges(false, "", "", "", ruleNumber, OracleConnString);
        txtResults.Text = ret;
        GetRuleStreamRules(selectedRuleStream);
        FetchNext();
    }

    private void BtnClearOracle_Click(object sender, EventArgs e)
    {
        int ruleNumber = int.Parse(txtRuleNumber.Text);

        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        string sql = @"UPDATE T_D_RULES SET F_CALC_ORA = '' WHERE F_RULE_NUMBER = :RN";

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = sql,
            CommandType = CommandType.Text
        };

        cmd.Parameters.Add("RN", ruleNumber);

        cmd.ExecuteNonQuery();

        conn.Dispose();

        string ret = DataUtils.UpdateConvertedFlag(ruleNumber, OracleConnString);
        txtResults.Text = ret;
        GetRuleStreamRules(selectedRuleStream);
        FetchNext();

    }

    private void BtnTestSQL_Click(object sender, EventArgs e)
    {
        string queryToRun = Utils.ModifyForRun(txtGoldenCalc.Text);
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        txtResults.Text = "SQL Server version tested: " + DataUtils.ExecuteTest(queryToRun, ruleNumber, OracleConnString);
    }

    private void ChkConvertOnly_CheckedChanged(object sender, EventArgs e)
    {
        txtRuleNumber.Text = "";
        txtGoldenCalc.Text = "";
        txtOracle.Text = "";
        txtDateUpdated.Text = "";
        txtResults.Text = "";
        txtRuleName.Text = "";
        txtUserUpdated.Text = "";

        lblUpdated.Visible = false;

        GetRuleStreamRules(selectedRuleStream);
        FetchNext();
    }
}
