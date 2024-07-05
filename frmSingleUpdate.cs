namespace RuleDConversion;

public partial class FrmSingleUpdate : Form
{
    static string OracleConnString;
    public FrmSingleUpdate()
    {
        InitializeComponent();

        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnectionSB();
        Text = $"Single Rule Update {ocsb.DataSource}";
        OracleConnString = ocsb.ConnectionString;
    }

    void FetchRule()
    {
        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };
        conn.Open();

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = @"select d.F_CALC_ORA AS ORASYNTAX, d.F_CALC AS ORACALC, g.F_CALC AS GOLDENCALC, g.F_RULE_NAME AS RULENAME, d.F_RULE_NUMBER AS RULENUMBER
                            FROM T_D_RULES d, T_D_RULES_GOLDEN g 
                            WHERE d.F_RULE_NUMBER = g.F_RULE_NUMBER 
                            AND g.F_RULE_NUMBER = :RNUM",
            CommandType = CommandType.Text
        };

        bool ok = long.TryParse(txtRuleNumber.Text, out long rnum);

        if (!ok) return;

        cmd.Parameters.Add("RNUM", rnum);

        OracleDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                txtRuleNumber.Text = (dr["RULENUMBER"] as int? ?? 0).ToString();
                txtGoldenCalc.Text = dr["GOLDENCALC"] as string;
                txtOraSyntax.Text = dr["ORASYNTAX"] as string;
                txtRuleName.Text = dr["RULENAME"] as string;

                txtGoldenCalc.Text = txtGoldenCalc.Text.Replace("UD_RUNSQLD('", "");
                txtGoldenCalc.Text = txtGoldenCalc.Text.TrimEnd("')".ToCharArray());

                txtOraSyntax.Text = txtOraSyntax.Text.Replace("UD_RUNSQLD('", "");
                txtOraSyntax.Text = txtOraSyntax.Text.TrimEnd("')".ToCharArray());
            }
            dr.Close();
            conn.Dispose();
        }
        else
        {
            txtRuleName.Text = "<<Not Found>>";
        }

        BuildWhereUsedTable();

        btnConverted.Enabled = false;
    }


    private void BuildWhereUsedTable()
    {
        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };
        conn.Open();

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = @"SELECT TO_CHAR(s.F_STREAM_ID) AS STREAMID, s.F_NAME , TO_CHAR(g.F_GROUP_ID) AS GROUPID
                            FROM  T_GROUP_MEMBERS g
                            JOIN T_RULE_STREAM_MEMBERS sm ON sm.F_GROUP_ID = g.F_GROUP_ID
                            JOIN T_RULE_STREAMS s ON s.F_STREAM_ID = sm.F_STREAM_ID
                            WHERE s.F_STREAM_ID IN (86,74,104,101,89,93,108,90,107,92,94,97,95,96,102,106,103,98,75,83,84,99,105,109,121,115,100,111,87,91,117)
                            AND g.F_RULE_NUMBER = :RNUM",
            CommandType = CommandType.Text
        };

        bool ok = long.TryParse(txtRuleNumber.Text, out long rnum);

        if (!ok) return;

        cmd.Parameters.Add("RNUM", rnum);

        OracleDataReader dr = cmd.ExecuteReader();

        tableRegions.Controls.Clear();
        tableRegions.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

        tableRegions.Controls.Add(new Label() { Text = "Streeam ID" }, 0, 0);
        tableRegions.Controls.Add(new Label() { Text = " Stream Name" }, 1, 0);
        tableRegions.Controls.Add(new Label() { Text = "Group ID" }, 2, 0);

        int row = 1;

        while (dr.Read())
        {
            Label l1 = new()
            {
                Text = dr["STREAMID"] as string,
                AutoSize = true
            };

            Label l2 = new()
            {
                Text = dr["F_NAME"] as string,
                AutoSize = true
            };


            Label l3 = new()
            {
                Text = dr["GROUPID"] as string,
                AutoSize = true
            };

            tableRegions.Controls.Add(l1, 0, row);
            tableRegions.Controls.Add(l2, 1, row);
            tableRegions.Controls.Add(l3, 2, row);
            row++;
        }
        dr.Close();
        conn.Dispose();
    }

    private void BtnFind_Click(object sender, EventArgs e)
    {
        ClearControls();
        FetchRule();
    }

    private void BtnTest_Click(object sender, EventArgs e)
    {
        txtResults.Text = "";
        string queryToRun = Utils.ModifyForRun(txtOraSyntax.Text);
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        txtResults.Text = DataUtils.ExecuteTest("@",queryToRun, ruleNumber, OracleConnString);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (txtOraSyntax.Text.StartsWith("UD_RUNSQLD('"))
        {
            txtOraSyntax.Text = txtOraSyntax.Text.TrimStart("UD_RUNSQLD('".ToCharArray());
            txtOraSyntax.Text = txtOraSyntax.Text.TrimEnd("')".ToCharArray());
        }

        string queryToRun = $"UD_RUNSQLD('{txtOraSyntax.Text} ')";
        string currentSql = $"UD_RUNSQLD('{txtGoldenCalc.Text} ')";
        string ruleName = txtRuleName.Text;
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        string ret = DataUtils.SaveChanges(true, currentSql, queryToRun, ruleName, ruleNumber, OracleConnString);
        txtResults.Text = ret;
    }

    private void BtnConverted_Click(object sender, EventArgs e)
    {
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        string ret = DataUtils.UpdateConvertedFlag(ruleNumber, OracleConnString);
        txtResults.Text = ret;
    }

    private void BtnGenerate_Click(object sender, EventArgs e)
    {
        string calc = $" TO_NCLOB(q''^' || {txtOraSyntax.Text} || '^'') ";

        string updateSQL = $" UPDATE T_D_RULES SET F_CALC = {calc} WHERE F_RULE_NUMBER = {txtSourceRuleNumber.Text};";

        txtOracleCalc.Text = updateSQL;

        btnConverted.Enabled = true;
    }

    private void BtnConverted_Click_1(object sender, EventArgs e)
    {
        File.WriteAllText($"c:/temp/drules/{txtSourceRuleNumber.Text}.txt", txtOracleCalc.Text);
        btnConverted.Enabled = false;
    }

    private void TxtRuleNumber_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ClearControls();
            FetchRule();
            e.Handled = true;
        }
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        txtResults.Text = "";
        string queryToRun = Utils.ModifyForRun(txtGoldenCalc.Text);
        int ruleNumber = int.Parse(txtRuleNumber.Text);
        txtResults.Text = DataUtils.ExecuteTest("@",queryToRun, ruleNumber, OracleConnString);
    }

    void ClearControls()
    {
        txtGoldenCalc.Text = "";
        txtOracleCalc.Text = "";
        txtOraSyntax.Text = "";
        txtRuleName.Text = "";
        txtResults.Text = "";
    }
}