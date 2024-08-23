using System.IO;

namespace RuleDConversion;

public partial class FrmFinal : Form
{
    static string OracleConnString;

    public FrmFinal()
    {
        InitializeComponent();

    }
    private void FrmFinal_Load(object sender, EventArgs e)
    {       
        Application.DoEvents();
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnectionSB();
        Text = ocsb.DataSource;
        OracleConnString = ocsb.ConnectionString;

        lblStatus.Text = $"Connected to {ocsb.DataSource} ... Creating script files...";
        Application.DoEvents();

        bool ok = ScriptGenerator.GenerateRegionals(OracleConnString, txtScriptOutputFolder.Text);

        if (ok)
        {
            lblStatus.Text = $"Script creation complete. Result {ok}";
            Application.DoEvents();
        }

    }
}
