using static RuleManager.Utils;

namespace RuleManager;
public partial class FrmScripts : Form
{
    static string OracleConnString;
    List<RegionalStream> streams = [];

    public FrmScripts()
    {
        InitializeComponent();

        ShowStreamCounts();
    }

    private void FrmScripts_Load(object sender, EventArgs e)
    {
        ddStreams.DataSource = GetRegionalRuleStreamList();

        streams = MigrateFromGolden.GetStreamCounts();

        ddStreams.DisplayMember = "Name";
        ddStreams.ValueMember = "Id";

        ddStreams.SelectedIndex = 0;

    }

    private void Button1_Click(object sender, EventArgs e)
    {
        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnection();
        Text = ocsb.DataSource;
        OracleConnString = ocsb.ConnectionString;

        lblStatus.Text = $"Connected to {ocsb.DataSource}";

        bool ok = ScriptGenerator.GenerateRegionals(OracleConnString, txtScriptOutputFolder.Text);

        if (ok)
        {
            lblStatus.Text = $"Script creation complete. Result {ok}";
        }
    }

    private void BtnLoadGolden_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Running Migration";
        Application.DoEvents();

        bool ok = MigrateFromGolden.PopulateOraFromGolden();

        lblStatus.Text = $"Finished Migration, result {ok}";
        Application.DoEvents();
    }

    private void BtnCompare_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Running Compare";
        Application.DoEvents();

        lblStatus.Text = $"Finished Compare, result {true}";
        Application.DoEvents();
    }

    private void BtnRebuild_Click(object sender, EventArgs e)
    {
        lblStatus.Text = "Rebuilding groups and Streams...";
        Application.DoEvents();

        MigrateFromGolden.RebuildStreamsAndGroups();

        lblStatus.Text = "Finished Rebuilding groups and Streams...";

    }

    void ShowStreamCounts()
    {
        lblStatus.Text = "Getting Region Stream counts...";
        Application.DoEvents();

        List<RegionalStream> streams = MigrateFromGolden.GetStreamCounts();

        tableRegions.Controls.Add(new Label() { Text = "Region" }, 0, 0);
        tableRegions.Controls.Add(new Label() { Text = "# Total" }, 1, 0);
        tableRegions.Controls.Add(new Label() { Text = "# Converted" }, 2, 0);
        tableRegions.Controls.Add(new Label() { Text = "# To Check" }, 3, 0);

        int row = 1;

        foreach (RegionalStream stream in streams)
        {
            tableRegions.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableRegions.Controls.Add(new Label() { Text = stream.Name }, 0, row);
            tableRegions.Controls.Add(new Label() { Text = stream.Total.ToString() }, 1, row);
            tableRegions.Controls.Add(new Label() { Text = stream.Converted.ToString() }, 2, row);
            tableRegions.Controls.Add(new Label() { Text = stream.UnConverted.ToString() }, 3, row);
            row++;
        }

        lblStatus.Text = "";
    }

    private void BtnTestRules_Click(object sender, EventArgs e)
    {
        RegionalStream stream = (RegionalStream)ddStreams.SelectedItem;

        txtRuleResults.Text = $"Testing RuleStream {stream.Name} {Environment.NewLine}";
        txtRuleResults.Text += $"-------------------------------- {Environment.NewLine}";
        lblCountMessage.Text = "";

        int errorCount = 0;
        int execCount = 0;
        int ruleCount = streams.FirstOrDefault(s => s.Id == stream.Id).Total;

        OracleConnectionStringBuilder ocsbCont = Utils.GetOracleConnection();

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

        OracleConnectionStringBuilder ocsbCS = Utils.GetCSOracleConnection();

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

            string execResult = DataUtils.ExecuteTest(queryToRun, ruleNumber, ocsbCS.ConnectionString);

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

        lblCountMessage.Text = $"Rules in stream {ruleCount}, # Rules tested: {execCount}, Errors: {errorCount}";

    }

   
}
