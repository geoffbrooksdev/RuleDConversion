using System.Data.SqlClient;
using System.IO;
using static RuleDConversion.Utils;

namespace RuleDConversion;
public partial class FrmTests : Form
{
    // static string OracleConnString;
    List<RegionalStream> streams = [];

    public FrmTests()
    {
        InitializeComponent();
    }

    private void FrmScripts_Load(object sender, EventArgs e)
    {
        ddStreams.DataSource = GetRegionalRuleStreamList();
        streams = MigrateFromGolden.GetStreamCounts();
        ddStreams.DisplayMember = "Name";
        ddStreams.ValueMember = "Id";

        ddStreams.SelectedIndex = 0;

        txtProduct.Text = "ISHL-01";

    }

    private void BtnCopyFromGolden_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Copying product from Golden to Oracle...";
        Application.DoEvents();
        string productId = txtProduct.Text;

        bool ok = ProductTesting.CopyProduct(productId);

        lblStatus.Text = $"Copy finished: Result: {ok}";
    }

    private void BtnRunTests_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Running tests...";
        lblCompareDiff.Text = "";
        Application.DoEvents();

        string productId = txtProduct.Text;
        int totaldiff = 0;

        RegionalStream stream = (RegionalStream)ddStreams.SelectedItem;

        txtRuleResults.Text = $"Testing RuleStream {stream.Name} {Environment.NewLine}";
        txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";
        txtCompareResults.Text = $"Compare Golden to Oracle (Text and Data) {Environment.NewLine}";
        txtCompareResults.Text += $"-------------------------------- {Environment.NewLine}";
        lblStatus.Text = $"Running rules for {stream.Name}";

        Application.DoEvents();

        int errorCount = 0;
        int execCount = 0;
        int ruleCount = streams.FirstOrDefault(s => s.Id == stream.Id).Total;

        OracleConnectionStringBuilder ocsbCont = Utils.GetOracleConnectionSB();       

        OracleConnection conn = new()
        {
            ConnectionString = ocsbCont.ConnectionString
        };

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = @"SELECT d.F_RULE_NUMBER, r.F_RULE_NAME, d.F_CALC_ORA, d.F_CALC
								FROM T_D_RULES d
								JOIN T_GROUP_MEMBERS m ON m.F_RULE_NUMBER = d.F_RULE_NUMBER
								JOIN T_RULES r ON r.F_RULE_NUMBER = d.F_RULE_NUMBER
								WHERE m.F_GROUP_ID IN 
								(SELECT F_GROUP_ID FROM T_RULE_STREAM_MEMBERS
								WHERE F_STREAM_ID = :SID)"
        };

        cmd.Parameters.Add("SID", OracleDbType.Int32);
        cmd.Parameters[0].Value = Convert.ToInt32(stream.Id);

        conn.Open();
        OracleDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            string ruleText = dr["F_CALC_ORA"] as string;

            if (string.IsNullOrEmpty(ruleText))
            {
                ruleText = dr["F_CALC"] as string;
            }

            string queryToRun = Utils.ModifyForRun(ruleText);
            queryToRun = Utils.FormatQueryText(queryToRun);

            int ruleNumber = dr["F_RULE_NUMBER"] as int? ?? 0;

            string execResult = DataUtils.ExecuteTest(productId, queryToRun, ruleNumber, ocsbCont.ConnectionString, true);

            txtRuleResults.Text += $"# {ruleNumber} :: {execResult}{Environment.NewLine}";
            txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";

            SqlConnectionStringBuilder sscb = Utils.GetSqlServerConnectionSB();
          
            string compareResults = DataUtils.CompareTestResults(productId, ruleNumber, ocsbCont.ConnectionString, sscb.ConnectionString);
            int diffCount = 0;

            if (!string.IsNullOrEmpty(compareResults))
            {
                txtCompareResults.Text += compareResults;
                txtCompareResults.Text += $"-------------------------------- {Environment.NewLine}";
                diffCount++;
            }

            if (diffCount > 0)
            {
                totaldiff += diffCount;
                lblCompareDiff.Text = $"Differences detected: {totaldiff}";
            }

            execCount++;
            if (execResult.StartsWith("Exception"))
            {
                errorCount++;
            }

            Application.DoEvents();
        }

        dr.Close();
        conn.Close();

        txtRuleResults.Text += $"Finished {Environment.NewLine}";
        txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";
        lblStatus.Text = $"Finished: Rules in stream {ruleCount}, # Rules tested: {execCount}, Errors: {errorCount}";
    }

    private void BtnRunAllTests_Click(object sender, EventArgs e)
    {
        string productId = txtProduct.Text;

        List<RegionalStream> streams = GetRegionalRuleStreamList();
        int errorCount = 0;
        int execCount = 0;
        int ruleCount = 0;

        foreach (RegionalStream stream in streams)
        {
            txtRuleResults.Text += $"Testing RuleStream {stream.Name} {Environment.NewLine}";
            txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";
            lblStatus.Text = $"Running rules for {stream.Name}";

            Application.DoEvents();
           
            ruleCount += streams.FirstOrDefault(s => s.Id == stream.Id).Total;

            OracleConnectionStringBuilder ocsbCont = Utils.GetOracleConnectionSB();

            string OracleConnStringCont = ocsbCont.ConnectionString;

            OracleConnection connCont = new()
            {
                ConnectionString = OracleConnStringCont
            };

            OracleCommand cmd = new()
            {
                Connection = connCont,
                CommandText = @"SELECT d.F_RULE_NUMBER, r.F_RULE_NAME, d.F_CALC_ORA, d.F_CALC
								FROM T_D_RULES d
								JOIN T_GROUP_MEMBERS m ON m.F_RULE_NUMBER = d.F_RULE_NUMBER
								JOIN T_RULES r ON r.F_RULE_NUMBER = d.F_RULE_NUMBER
								WHERE m.F_GROUP_ID IN 
								(SELECT F_GROUP_ID FROM T_RULE_STREAM_MEMBERS
								WHERE F_STREAM_ID = :SID)"
            };

            cmd.Parameters.Add("SID", OracleDbType.Int32);
            cmd.Parameters[0].Value = Convert.ToInt32(stream.Id);

            OracleConnectionStringBuilder ocsbCS = Utils.GetOracleConnectionSB();

            connCont.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string ruleText = dr["F_CALC_ORA"] as string;

                if (string.IsNullOrEmpty(ruleText))
                {
                    ruleText = dr["F_CALC"] as string;
                }

                string queryToRun = Utils.ModifyForRun(ruleText);
                queryToRun = Utils.FormatQueryText(queryToRun);

                int ruleNumber = dr["F_RULE_NUMBER"] as int? ?? 0;

                string execResult = DataUtils.ExecuteTest(productId, queryToRun, ruleNumber, ocsbCS.ConnectionString, true);

                txtRuleResults.Text += $"# {ruleNumber} :: {execResult}{Environment.NewLine}";
                txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";

                execCount++;
                if (execResult.StartsWith("Exception"))
                {
                    errorCount++;
                }

                Application.DoEvents();
            }

            dr.Close();
            connCont.Close();

            txtRuleResults.Text += $"Finished {Environment.NewLine}";
            txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";
            lblStatus.Text = $"Finished: Rules in stream {ruleCount}, # Rules tested: {execCount}, Errors: {errorCount}";
        }
    }

    private void DDStreams_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRuleResults.Text = "";
        lblStatus.Text = "";
        txtCompareResults.Text = "";
        lblCompareDiff.Text = "";
    }       
}
