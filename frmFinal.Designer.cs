namespace RuleDConversion;
partial class FrmFinal
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
        label6 = new Label();
        lblMatch = new Label();
        lblConnString = new Label();
        lblStatus = new Label();
        label2 = new Label();
        label3 = new Label();
        button1 = new Button();
        txtScriptOutputFolder = new TextBox();
        SuspendLayout();
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 12F);
        label6.Location = new Point(828, 73);
        label6.Name = "label6";
        label6.Size = new Size(0, 21);
        label6.TabIndex = 28;
        // 
        // lblMatch
        // 
        lblMatch.AutoSize = true;
        lblMatch.Font = new Font("Segoe UI", 12F);
        lblMatch.Location = new Point(997, 71);
        lblMatch.Name = "lblMatch";
        lblMatch.Size = new Size(0, 21);
        lblMatch.TabIndex = 29;
        // 
        // lblConnString
        // 
        lblConnString.AutoSize = true;
        lblConnString.Location = new Point(-10, 671);
        lblConnString.Name = "lblConnString";
        lblConnString.Size = new Size(0, 15);
        lblConnString.TabIndex = 30;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.BackColor = Color.Transparent;
        lblStatus.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblStatus.Location = new Point(39, 276);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(44, 21);
        lblStatus.TabIndex = 35;
        lblStatus.Text = "Idle...";
        // 
        // label2
        // 
        label2.BackColor = Color.Transparent;
        label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        label2.Location = new Point(39, 73);
        label2.Name = "label2";
        label2.Size = new Size(538, 24);
        label2.TabIndex = 34;
        label2.Text = "When all rule updates are complete: generate a new set of scripts";
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.BackColor = Color.Transparent;
        label3.Font = new Font("Segoe UI", 12F);
        label3.Location = new Point(39, 145);
        label3.Name = "label3";
        label3.Size = new Size(151, 21);
        label3.TabIndex = 32;
        label3.Text = "Script Output Folder";
        // 
        // button1
        // 
        button1.BackColor = SystemColors.ButtonFace;
        button1.Font = new Font("Segoe UI", 12F);
        button1.Location = new Point(37, 214);
        button1.Name = "button1";
        button1.Size = new Size(83, 34);
        button1.TabIndex = 33;
        button1.Text = "Go";
        button1.UseVisualStyleBackColor = false;
        button1.Click += Button1_Click;
        // 
        // txtScriptOutputFolder
        // 
        txtScriptOutputFolder.Font = new Font("Segoe UI", 12F);
        txtScriptOutputFolder.Location = new Point(39, 175);
        txtScriptOutputFolder.Name = "txtScriptOutputFolder";
        txtScriptOutputFolder.Size = new Size(298, 29);
        txtScriptOutputFolder.TabIndex = 31;
        txtScriptOutputFolder.Text = "c:\\oratemp\\test\\";
        // 
        // frmFinal
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1716, 833);
        Controls.Add(lblStatus);
        Controls.Add(label2);
        Controls.Add(label3);
        Controls.Add(button1);
        Controls.Add(txtScriptOutputFolder);
        Controls.Add(lblConnString);
        Controls.Add(lblMatch);
        Controls.Add(label6);
        Name = "frmFinal";
        StartPosition = FormStartPosition.WindowsDefaultBounds;
        Text = "Generate Scripts";
        WindowState = FormWindowState.Maximized;
        Load += FrmFinal_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Label label6;
    private Label lblMatch;
    private Label lblConnString;
    private Label lblStatus;
    private Label label2;
    private Label label3;
    private Button button1;
    private TextBox txtScriptOutputFolder;
}