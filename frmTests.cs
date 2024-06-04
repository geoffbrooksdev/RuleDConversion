using static RuleManager.Utils;

namespace RuleManager;
public partial class FrmTests : Form
{
    // static string OracleConnString;
    //List<RegionalStream> streams = [];

    public FrmTests()
    {
        InitializeComponent();
    }

    private void FrmScripts_Load(object sender, EventArgs e)
    {
        ddStreams.DataSource = GetRegionalRuleStreamList();

        ddStreams.DisplayMember = "Name";
        ddStreams.ValueMember = "Id";

        ddStreams.SelectedIndex = 0;

    }

    private void BtnCopyFromGolden_Click(object sender, EventArgs e)
    {
        string productId = txtProduct.Text;




    }
}
