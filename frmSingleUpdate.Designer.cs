namespace RuleDConversion;
partial class FrmSingleUpdate
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        txtRuleNumber = new TextBox();
        label1 = new Label();
        txtGoldenCalc = new TextBox();
        label2 = new Label();
        btnFind = new Button();
        btnSave = new Button();
        btnTest = new Button();
        label3 = new Label();
        txtRuleName = new TextBox();
        txtResults = new TextBox();
        label4 = new Label();
        label5 = new Label();
        txtOraSyntax = new TextBox();
        label6 = new Label();
        txtOracleCalc = new TextBox();
        btnConverted = new Button();
        textBox1 = new TextBox();
        txtSourceRuleNumber = new TextBox();
        label7 = new Label();
        btnGenerate = new Button();
        label8 = new Label();
        tableRegions = new TableLayoutPanel();
        button1 = new Button();
        SuspendLayout();
        // 
        // txtRuleNumber
        // 
        txtRuleNumber.Location = new Point(30, 31);
        txtRuleNumber.Name = "txtRuleNumber";
        txtRuleNumber.Size = new Size(153, 23);
        txtRuleNumber.TabIndex = 0;
        txtRuleNumber.KeyDown += TxtRuleNumber_KeyDown;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(30, 7);
        label1.Name = "label1";
        label1.Size = new Size(103, 21);
        label1.TabIndex = 1;
        label1.Text = "Rule Number";
        // 
        // txtGoldenCalc
        // 
        txtGoldenCalc.Font = new Font("Segoe UI", 12F);
        txtGoldenCalc.Location = new Point(38, 103);
        txtGoldenCalc.Multiline = true;
        txtGoldenCalc.Name = "txtGoldenCalc";
        txtGoldenCalc.ScrollBars = ScrollBars.Vertical;
        txtGoldenCalc.Size = new Size(805, 325);
        txtGoldenCalc.TabIndex = 2;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Font = new Font("Segoe UI", 12F);
        label2.Location = new Point(32, 76);
        label2.Name = "label2";
        label2.Size = new Size(130, 21);
        label2.TabIndex = 3;
        label2.Text = "Golden  (F_CALC)";
        // 
        // btnFind
        // 
        btnFind.Location = new Point(189, 30);
        btnFind.Name = "btnFind";
        btnFind.Size = new Size(75, 24);
        btnFind.TabIndex = 4;
        btnFind.Text = "Find";
        btnFind.UseVisualStyleBackColor = true;
        btnFind.Click += BtnFind_Click;
        // 
        // btnSave
        // 
        btnSave.Font = new Font("Segoe UI", 12F);
        btnSave.Location = new Point(1718, 664);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(75, 35);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += BtnSave_Click;
        // 
        // btnTest
        // 
        btnTest.Font = new Font("Segoe UI", 12F);
        btnTest.Location = new Point(1016, 664);
        btnTest.Name = "btnTest";
        btnTest.Size = new Size(75, 35);
        btnTest.TabIndex = 7;
        btnTest.Text = "Test";
        btnTest.UseVisualStyleBackColor = true;
        btnTest.Click += BtnTest_Click;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Font = new Font("Segoe UI", 12F);
        label3.Location = new Point(299, 6);
        label3.Name = "label3";
        label3.Size = new Size(87, 21);
        label3.TabIndex = 8;
        label3.Text = "Rule Name";
        // 
        // txtRuleName
        // 
        txtRuleName.Location = new Point(299, 30);
        txtRuleName.Name = "txtRuleName";
        txtRuleName.Size = new Size(1494, 23);
        txtRuleName.TabIndex = 9;
        // 
        // txtResults
        // 
        txtResults.Location = new Point(881, 741);
        txtResults.Multiline = true;
        txtResults.Name = "txtResults";
        txtResults.ScrollBars = ScrollBars.Vertical;
        txtResults.Size = new Size(912, 93);
        txtResults.TabIndex = 10;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Font = new Font("Segoe UI", 12F);
        label4.Location = new Point(882, 717);
        label4.Name = "label4";
        label4.Size = new Size(60, 21);
        label4.TabIndex = 11;
        label4.Text = "Results";
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Segoe UI", 12F);
        label5.Location = new Point(881, 76);
        label5.Name = "label5";
        label5.Size = new Size(160, 21);
        label5.TabIndex = 13;
        label5.Text = "Oracle (F_CALC_ORA)";
        // 
        // txtOraSyntax
        // 
        txtOraSyntax.Font = new Font("Segoe UI", 12F);
        txtOraSyntax.Location = new Point(881, 103);
        txtOraSyntax.Multiline = true;
        txtOraSyntax.Name = "txtOraSyntax";
        txtOraSyntax.ScrollBars = ScrollBars.Vertical;
        txtOraSyntax.Size = new Size(912, 555);
        txtOraSyntax.TabIndex = 12;
        // 
        // label6
        // 
        label6.AutoSize = true;
        label6.Font = new Font("Segoe UI", 12F);
        label6.Location = new Point(882, 837);
        label6.Name = "label6";
        label6.Size = new Size(125, 21);
        label6.TabIndex = 15;
        label6.Text = "Update statment";
        label6.Visible = false;
        // 
        // txtOracleCalc
        // 
        txtOracleCalc.Font = new Font("Segoe UI", 12F);
        txtOracleCalc.Location = new Point(883, 861);
        txtOracleCalc.Multiline = true;
        txtOracleCalc.Name = "txtOracleCalc";
        txtOracleCalc.ScrollBars = ScrollBars.Vertical;
        txtOracleCalc.Size = new Size(805, 306);
        txtOracleCalc.TabIndex = 14;
        txtOracleCalc.Visible = false;
        // 
        // btnConverted
        // 
        btnConverted.Font = new Font("Segoe UI", 12F);
        btnConverted.Location = new Point(52, 741);
        btnConverted.Name = "btnConverted";
        btnConverted.Size = new Size(154, 33);
        btnConverted.TabIndex = 32;
        btnConverted.Text = "Save To File";
        btnConverted.UseVisualStyleBackColor = true;
        btnConverted.Visible = false;
        btnConverted.Click += BtnConverted_Click_1;
        // 
        // textBox1
        // 
        textBox1.Font = new Font("Segoe UI", 12F);
        textBox1.Location = new Point(225, 700);
        textBox1.Name = "textBox1";
        textBox1.Size = new Size(145, 29);
        textBox1.TabIndex = 33;
        textBox1.Text = "RW-@RULEID";
        textBox1.Visible = false;
        // 
        // txtSourceRuleNumber
        // 
        txtSourceRuleNumber.Font = new Font("Segoe UI", 12F);
        txtSourceRuleNumber.Location = new Point(61, 700);
        txtSourceRuleNumber.Name = "txtSourceRuleNumber";
        txtSourceRuleNumber.Size = new Size(145, 29);
        txtSourceRuleNumber.TabIndex = 34;
        txtSourceRuleNumber.Visible = false;
        // 
        // label7
        // 
        label7.AutoSize = true;
        label7.Font = new Font("Segoe UI", 12F);
        label7.Location = new Point(61, 678);
        label7.Name = "label7";
        label7.Size = new Size(106, 21);
        label7.TabIndex = 35;
        label7.Text = "Source Rule #";
        label7.Visible = false;
        // 
        // btnGenerate
        // 
        btnGenerate.Font = new Font("Segoe UI", 12F);
        btnGenerate.Location = new Point(61, 642);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(249, 31);
        btnGenerate.TabIndex = 36;
        btnGenerate.Text = "Generate Update Statement";
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Visible = false;
        btnGenerate.Click += BtnGenerate_Click;
        // 
        // label8
        // 
        label8.AutoSize = true;
        label8.Font = new Font("Segoe UI", 12F);
        label8.Location = new Point(38, 472);
        label8.Name = "label8";
        label8.Size = new Size(232, 21);
        label8.TabIndex = 37;
        label8.Text = "Rule Streams including this rule:";
        // 
        // tableRegions
        // 
        tableRegions.AutoSize = true;
        tableRegions.ColumnCount = 3;
        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
        tableRegions.Location = new Point(38, 514);
        tableRegions.Name = "tableRegions";
        tableRegions.RowCount = 1;
        tableRegions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableRegions.Size = new Size(805, 52);
        tableRegions.TabIndex = 38;
        // 
        // button1
        // 
        button1.Font = new Font("Segoe UI", 12F);
        button1.Location = new Point(674, 444);
        button1.Name = "button1";
        button1.Size = new Size(169, 35);
        button1.TabIndex = 39;
        button1.Text = "Test on Oracle";
        button1.UseVisualStyleBackColor = true;
        button1.Click += Button1_Click;
        // 
        // FrmSingleUpdate
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1817, 841);
        Controls.Add(button1);
        Controls.Add(tableRegions);
        Controls.Add(label8);
        Controls.Add(btnGenerate);
        Controls.Add(label7);
        Controls.Add(txtSourceRuleNumber);
        Controls.Add(textBox1);
        Controls.Add(btnConverted);
        Controls.Add(label6);
        Controls.Add(txtOracleCalc);
        Controls.Add(label5);
        Controls.Add(txtOraSyntax);
        Controls.Add(label4);
        Controls.Add(txtResults);
        Controls.Add(txtRuleName);
        Controls.Add(label3);
        Controls.Add(btnTest);
        Controls.Add(btnSave);
        Controls.Add(btnFind);
        Controls.Add(label2);
        Controls.Add(txtGoldenCalc);
        Controls.Add(label1);
        Controls.Add(txtRuleNumber);
        Name = "FrmSingleUpdate";
        StartPosition = FormStartPosition.WindowsDefaultBounds;
        Text = "Single Rule Update";
        WindowState = FormWindowState.Maximized;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private TextBox txtRuleNumber;
    private Label label1;
    private TextBox txtGoldenCalc;
    private Label label2;
    private Button btnFind;
    private Button btnSave;
    private Button btnTest;
    private Label label3;
    private TextBox txtRuleName;
    private TextBox txtResults;
    private Label label4;
    private Label label5;
    private TextBox txtOraSyntax;
    private Label label6;
    private TextBox txtOracleCalc;
    private Button btnConverted;
    private TextBox textBox1;
    private TextBox txtSourceRuleNumber;
    private Label label7;
    private Button btnGenerate;
    private Label label8;
    private TableLayoutPanel tableRegions;
    private Button button1;
}