namespace RuleDConversion;
partial class frmMain
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        rulesToolStripMenuItem = new ToolStripMenuItem();
        scriptsToolStripMenuItem = new ToolStripMenuItem();
        updateToolStripMenuItem = new ToolStripMenuItem();
        productTestsToolStripMenuItem = new ToolStripMenuItem();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { rulesToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(978, 24);
        menuStrip1.TabIndex = 4;
        menuStrip1.Text = "menuStrip1";
        // 
        // rulesToolStripMenuItem
        // 
        rulesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { scriptsToolStripMenuItem, updateToolStripMenuItem, productTestsToolStripMenuItem });
        rulesToolStripMenuItem.Name = "rulesToolStripMenuItem";
        rulesToolStripMenuItem.Size = new Size(46, 20);
        rulesToolStripMenuItem.Text = "Tools";
        // 
        // scriptsToolStripMenuItem
        // 
        scriptsToolStripMenuItem.Name = "scriptsToolStripMenuItem";
        scriptsToolStripMenuItem.Size = new Size(247, 22);
        scriptsToolStripMenuItem.Text = "Migrate Golden  && Create Scripts";
        scriptsToolStripMenuItem.Click += ScriptsToolStripMenuItem_Click;
        // 
        // updateToolStripMenuItem
        // 
        updateToolStripMenuItem.Name = "updateToolStripMenuItem";
        updateToolStripMenuItem.Size = new Size(247, 22);
        updateToolStripMenuItem.Text = "Edit Single Rule";
        updateToolStripMenuItem.Click += UpdateToolStripMenuItem_Click;
        // 
        // productTestsToolStripMenuItem
        // 
        productTestsToolStripMenuItem.Name = "productTestsToolStripMenuItem";
        productTestsToolStripMenuItem.Size = new Size(247, 22);
        productTestsToolStripMenuItem.Text = "Product Tests";
        productTestsToolStripMenuItem.Click += ProductTestsToolStripMenuItem_Click;
        // 
        // frmMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(978, 667);
        Controls.Add(menuStrip1);
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Name = "frmMain";
        Text = "Rule Manager";
        WindowState = FormWindowState.Maximized;
        Load += FrmMain_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
	private ToolStripMenuItem rulesToolStripMenuItem;
	private ToolStripMenuItem scriptsToolStripMenuItem;
    private ToolStripMenuItem updateToolStripMenuItem;
    private ToolStripMenuItem productTestsToolStripMenuItem;
}