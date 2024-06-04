namespace RuleManager;
public partial class frmMain : Form
{
    public frmMain()
    {
        InitializeComponent();
    }

    void StreamsToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        FrmStreamMemberUpdaate newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();
    }

    private void ScriptsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmScripts newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        FrmScripts newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();
    }

    private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmSingleUpdate newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();
    }

    private void ProductTestsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmTests newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();

    }
}
