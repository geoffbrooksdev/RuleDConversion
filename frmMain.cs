namespace RuleDConversion;
public partial class FrmMain : Form
{
    public FrmMain()
    {
        InitializeComponent();
    }

    //void StreamsToolStripMenuItem1_Click(object sender, EventArgs e)
    //{
    //    FrmStreamMemberUpdaate newMDIChild = new()
    //    {
    //        MdiParent = this
    //    };
    //    newMDIChild.Show();
    //}

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

    private void GenerateOutputScriptsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmFinal newMDIChild = new()
        {
            MdiParent = this
        };
        newMDIChild.Show();

    }
}
