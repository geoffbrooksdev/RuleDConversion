namespace RuleDConversion;
partial class FrmStreamMemberUpdaate
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
        label4 = new Label();
        txtResults = new TextBox();
        txtRuleName = new TextBox();
        label3 = new Label();
        btnTest = new Button();
        btnSave = new Button();
        label1 = new Label();
        txtRuleNumber = new TextBox();
        label5 = new Label();
        txtOracle = new TextBox();
        label6 = new Label();
        lblMatch = new Label();
        lblConnString = new Label();
        btnConverted = new Button();
        txtUserUpdated = new TextBox();
        txtDateUpdated = new TextBox();
        lblRuleSet = new Label();
        ddStreams = new ComboBox();
        label9 = new Label();
        label10 = new Label();
        txtGoldenCalc = new TextBox();
        textBox1 = new TextBox();
        btnClearOracle = new Button();
        lblUpdated = new Label();
        btnTestSQL = new Button();
        chkConvertOnly = new CheckBox();
        SuspendLayout();
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 12F);
        label4.Location = new Point(708, 564);
        label4.Name = "label4";
        label4.Size = new Size(60, 21);
        label4.TabIndex = 24;
        label4.Text = "Results";
        // 
        // txtResults
        // 
        txtResults.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtResults.Location = new Point(708, 588);
        txtResults.Multiline = true;
        txtResults.Name = "txtResults";
        txtResults.ScrollBars = ScrollBars.Vertical;
        txtResults.Size = new Size(681, 147);
        txtResults.TabIndex = 23;
        // 
        // txtRuleName
        // 
        txtRuleName.Location = new Point(291, 37);
        txtRuleName.Name = "txtRuleName";
        txtRuleName.Size = new Size(1098, 23);
        txtRuleName.TabIndex = 22;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 12F);
        label3.Location = new Point(291, 13);
        label3.Name = "label3";
        label3.Size = new Size(87, 21);
        label3.TabIndex = 21;
        label3.Text = "Rule Name";
        // 
        // btnTest
        // 
        btnTest.Font = new Font("Segoe UI", 12F);
        btnTest.Location = new Point(708, 498);
        btnTest.Name = "btnTest";
        btnTest.Size = new Size(95, 31);
        btnTest.TabIndex = 20;
        btnTest.Text = "Test";
        btnTest.UseVisualStyleBackColor = true;
        btnTest.Click += BtnTest_Click;
        // 
        // btnSave
        // 
        btnSave.Font = new Font("Segoe UI", 12F);
        btnSave.Location = new Point(1295, 498);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(94, 32);
        btnSave.TabIndex = 18;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(174, 13);
        label1.Name = "label1";
        label1.Size = new Size(103, 21);
        label1.TabIndex = 14;
        label1.Text = "Rule Number";
        // 
        // txtRuleNumber
        // 
        txtRuleNumber.Location = new Point(174, 37);
        txtRuleNumber.Name = "txtRuleNumber";
        txtRuleNumber.Size = new Size(103, 23);
        txtRuleNumber.TabIndex = 13;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 12F);
        label5.Location = new Point(997, 71);
        label5.Name = "label5";
        label5.Size = new Size(105, 21);
        label5.TabIndex = 27;
        label5.Text = "Oracle Syntax";
        // 
        // txtOracle
        // 
        txtOracle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtOracle.Location = new Point(708, 100);
        txtOracle.Multiline = true;
        txtOracle.Name = "txtOracle";
        txtOracle.ScrollBars = ScrollBars.Vertical;
        txtOracle.Size = new Size(681, 387);
        txtOracle.TabIndex = 26;
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
        // btnConverted
        // 
        btnConverted.Font = new Font("Segoe UI", 12F);
        btnConverted.Location = new Point(1150, 498);
        btnConverted.Name = "btnConverted";
        btnConverted.Size = new Size(123, 32);
        btnConverted.TabIndex = 31;
        btnConverted.Text = "Mark as Done";
        btnConverted.UseVisualStyleBackColor = true;
        btnConverted.Click += BtnConverted_Click;
        // 
        // txtUserUpdated
        // 
        txtUserUpdated.Location = new Point(315, 504);
        txtUserUpdated.Name = "txtUserUpdated";
        txtUserUpdated.Size = new Size(103, 23);
        txtUserUpdated.TabIndex = 32;
        // 
        // txtDateUpdated
        // 
        txtDateUpdated.Location = new Point(424, 504);
        txtDateUpdated.Name = "txtDateUpdated";
        txtDateUpdated.Size = new Size(148, 23);
        txtDateUpdated.TabIndex = 33;
        // 
        // lblRuleSet
        // 
        lblRuleSet.AutoSize = true;
        lblRuleSet.Font = new Font("Segoe UI", 12F);
        lblRuleSet.Location = new Point(12, 561);
        lblRuleSet.Name = "lblRuleSet";
        lblRuleSet.Size = new Size(74, 21);
        lblRuleSet.TabIndex = 38;
        lblRuleSet.Text = "Rule Set: ";
        // 
        // ddStreams
        // 
        ddStreams.FormattingEnabled = true;
        ddStreams.Location = new Point(12, 37);
        ddStreams.Name = "ddStreams";
        ddStreams.Size = new Size(134, 23);
        ddStreams.TabIndex = 40;
        ddStreams.SelectedIndexChanged += DDStreams_SelectedIndexChanged;
        // 
        // label9
        // 
        label9.Location = new Point(0, 0);
        label9.Name = "label9";
        label9.Size = new Size(100, 23);
        label9.TabIndex = 51;
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Font = new Font("Segoe UI", 12F);
        label10.Location = new Point(268, 76);
        label10.Name = "label10";
        label10.Size = new Size(110, 21);
        label10.TabIndex = 43;
        label10.Text = "Golden Syntax";
        // 
        // txtGoldenCalc
        // 
        txtGoldenCalc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
        txtGoldenCalc.Location = new Point(12, 100);
        txtGoldenCalc.Multiline = true;
        txtGoldenCalc.Name = "txtGoldenCalc";
        txtGoldenCalc.ScrollBars = ScrollBars.Vertical;
        txtGoldenCalc.Size = new Size(681, 387);
        txtGoldenCalc.TabIndex = 42;
        // 
        // textBox1
        // 
        textBox1.Font = new Font("Segoe UI", 12F);
        textBox1.Location = new Point(708, 750);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(145, 29);
        textBox1.TabIndex = 46;
        textBox1.Text = "RW-@RULEID";
        textBox1.Visible = false;
        // 
        // btnClearOracle
        // 
        btnClearOracle.Font = new Font("Segoe UI", 12F);
        btnClearOracle.Location = new Point(1150, 536);
        btnClearOracle.Name = "btnClearOracle";
        btnClearOracle.Size = new Size(239, 36);
        btnClearOracle.TabIndex = 47;
        btnClearOracle.Text = "Remove Oracle Syntax";
        btnClearOracle.UseVisualStyleBackColor = true;
        btnClearOracle.Click += BtnClearOracle_Click;
        // 
        // lblUpdated
        // 
        lblUpdated.AutoSize = true;
        lblUpdated.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblUpdated.ForeColor = Color.Red;
        lblUpdated.Location = new Point(1291, 71);
        lblUpdated.Name = "lblUpdated";
        lblUpdated.Size = new Size(98, 21);
        lblUpdated.TabIndex = 48;
        lblUpdated.Text = "Needs Save";
        // 
        // btnTestSQL
        // 
        btnTestSQL.Font = new Font("Segoe UI", 12F);
        btnTestSQL.Location = new Point(12, 497);
        btnTestSQL.Name = "btnTestSQL";
        btnTestSQL.Size = new Size(132, 35);
        btnTestSQL.TabIndex = 49;
        btnTestSQL.Text = "Test on Oracle";
        btnTestSQL.UseVisualStyleBackColor = true;
        btnTestSQL.Click += BtnTestSQL_Click;
        // 
        // chkConvertOnly
        // 
        chkConvertOnly.AutoSize = true;
        chkConvertOnly.Location = new Point(14, 74);
        chkConvertOnly.Name = "chkConvertOnly";
        chkConvertOnly.Size = new Size(145, 19);
        chkConvertOnly.TabIndex = 52;
        chkConvertOnly.Text = "Need Conversion Only";
        chkConvertOnly.UseVisualStyleBackColor = true;
        chkConvertOnly.CheckedChanged += ChkConvertOnly_CheckedChanged;
        // 
        // frmStreamMemberUpdaate
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1716, 833);
        Controls.Add(chkConvertOnly);
        Controls.Add(btnTestSQL);
        Controls.Add(lblUpdated);
        Controls.Add(btnClearOracle);
        Controls.Add(textBox1);
        Controls.Add(label10);
        Controls.Add(txtGoldenCalc);
        Controls.Add(label9);
        Controls.Add(ddStreams);
        Controls.Add(lblRuleSet);
        Controls.Add(txtDateUpdated);
        Controls.Add(txtUserUpdated);
        Controls.Add(btnConverted);
        Controls.Add(lblConnString);
        Controls.Add(lblMatch);
        Controls.Add(label6);
        Controls.Add(label5);
        Controls.Add(txtOracle);
        Controls.Add(label4);
        Controls.Add(txtResults);
        Controls.Add(txtRuleName);
        Controls.Add(label3);
        Controls.Add(btnTest);
        Controls.Add(btnSave);
        Controls.Add(label1);
        Controls.Add(txtRuleNumber);
        Name = "frmStreamMemberUpdaate";
        StartPosition = FormStartPosition.WindowsDefaultBounds;
        Text = "Review/Edit Stream Members";
        WindowState = FormWindowState.Maximized;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private Label label4;
    private TextBox txtResults;
    private TextBox txtRuleName;
    private Label label3;
    private Button btnTest;
    private Button btnSave;
    private Label label1;
    private TextBox txtRuleNumber;
    private Label label5;
    private TextBox txtOracle;
    private Label label6;
    private Label lblMatch;
    private Label lblConnString;
    private Button btnConverted;
    private TextBox txtUserUpdated;
    private TextBox txtDateUpdated;
    private Label lblRuleSet;
    private ComboBox ddStreams;
    private Label label9;
    private Label label10;
    private TextBox txtGoldenCalc;
    private TextBox textBox1;
    private Button btnClearOracle;
    private Label lblUpdated;
    private Button btnTestSQL;
    private CheckBox chkConvertOnly;
}