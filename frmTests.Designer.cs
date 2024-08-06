namespace RuleDConversion
{
    partial class FrmTests
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
            label9 = new Label();
            ddStreams = new ComboBox();
            txtRuleResults = new TextBox();
            groupBox3 = new GroupBox();
            label2 = new Label();
            BtnRunAllTEsts = new Button();
            btnRunTests = new Button();
            lblStatus = new Label();
            txtProduct = new TextBox();
            label1 = new Label();
            btnCopyFromGolden = new Button();
            txtCompareResults = new TextBox();
            lblCompareResults = new Label();
            lblCompareDiff = new Label();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(38, 29);
            label9.Name = "label9";
            label9.Size = new Size(94, 21);
            label9.TabIndex = 45;
            label9.Text = "Rule Stream";
            // 
            // ddStreams
            // 
            ddStreams.FormattingEnabled = true;
            ddStreams.Location = new Point(38, 53);
            ddStreams.Name = "ddStreams";
            ddStreams.Size = new Size(250, 29);
            ddStreams.TabIndex = 44;
            ddStreams.SelectedIndexChanged += DDStreams_SelectedIndexChanged;
            // 
            // txtRuleResults
            // 
            txtRuleResults.Location = new Point(38, 119);
            txtRuleResults.Multiline = true;
            txtRuleResults.Name = "txtRuleResults";
            txtRuleResults.ScrollBars = ScrollBars.Vertical;
            txtRuleResults.Size = new Size(643, 608);
            txtRuleResults.TabIndex = 46;
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.Control;
            groupBox3.Controls.Add(lblCompareDiff);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(BtnRunAllTEsts);
            groupBox3.Controls.Add(btnRunTests);
            groupBox3.Controls.Add(txtRuleResults);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(lblStatus);
            groupBox3.Controls.Add(ddStreams);
            groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(297, 32);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1442, 763);
            groupBox3.TabIndex = 49;
            groupBox3.TabStop = false;
            groupBox3.Text = "Region Testing";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(38, 89);
            label2.Name = "label2";
            label2.Size = new Size(113, 21);
            label2.TabIndex = 58;
            label2.Text = "Testing Activity";
            // 
            // BtnRunAllTEsts
            // 
            BtnRunAllTEsts.BackColor = SystemColors.ButtonFace;
            BtnRunAllTEsts.Font = new Font("Segoe UI", 12F);
            BtnRunAllTEsts.Location = new Point(576, 48);
            BtnRunAllTEsts.Name = "BtnRunAllTEsts";
            BtnRunAllTEsts.Size = new Size(105, 37);
            BtnRunAllTEsts.TabIndex = 55;
            BtnRunAllTEsts.Text = "Run All Tests";
            BtnRunAllTEsts.UseVisualStyleBackColor = false;
            BtnRunAllTEsts.Click += BtnRunAllTests_Click;
            // 
            // btnRunTests
            // 
            btnRunTests.BackColor = SystemColors.ButtonFace;
            btnRunTests.Font = new Font("Segoe UI", 12F);
            btnRunTests.Location = new Point(308, 48);
            btnRunTests.Name = "btnRunTests";
            btnRunTests.Size = new Size(88, 37);
            btnRunTests.TabIndex = 54;
            btnRunTests.Text = "Run Tests";
            btnRunTests.UseVisualStyleBackColor = false;
            btnRunTests.Click += BtnRunTests_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.Location = new Point(38, 739);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(44, 21);
            lblStatus.TabIndex = 50;
            lblStatus.Text = "Idle...";
            // 
            // txtProduct
            // 
            txtProduct.Font = new Font("Segoe UI", 12F);
            txtProduct.Location = new Point(29, 68);
            txtProduct.Name = "txtProduct";
            txtProduct.Size = new Size(244, 29);
            txtProduct.TabIndex = 51;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(29, 32);
            label1.Name = "label1";
            label1.Size = new Size(168, 21);
            label1.TabIndex = 52;
            label1.Text = "Product ID to be tested";
            // 
            // btnCopyFromGolden
            // 
            btnCopyFromGolden.BackColor = SystemColors.ButtonFace;
            btnCopyFromGolden.Font = new Font("Segoe UI", 12F);
            btnCopyFromGolden.Location = new Point(29, 113);
            btnCopyFromGolden.Name = "btnCopyFromGolden";
            btnCopyFromGolden.Size = new Size(244, 37);
            btnCopyFromGolden.TabIndex = 53;
            btnCopyFromGolden.Text = "Populate from Golden SQL";
            btnCopyFromGolden.UseVisualStyleBackColor = false;
            btnCopyFromGolden.Click += BtnCopyFromGolden_Click;
            // 
            // txtCompareResults
            // 
            txtCompareResults.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCompareResults.Location = new Point(1035, 151);
            txtCompareResults.Multiline = true;
            txtCompareResults.Name = "txtCompareResults";
            txtCompareResults.ScrollBars = ScrollBars.Vertical;
            txtCompareResults.Size = new Size(692, 608);
            txtCompareResults.TabIndex = 56;
            // 
            // lblCompareResults
            // 
            lblCompareResults.AutoSize = true;
            lblCompareResults.BackColor = SystemColors.Control;
            lblCompareResults.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCompareResults.Location = new Point(1035, 121);
            lblCompareResults.Name = "lblCompareResults";
            lblCompareResults.Size = new Size(131, 21);
            lblCompareResults.TabIndex = 57;
            lblCompareResults.Text = "Compare Results:";
            // 
            // lblCompareDiff
            // 
            lblCompareDiff.AutoSize = true;
            lblCompareDiff.Font = new Font("Segoe UI", 12F);
            lblCompareDiff.Location = new Point(738, 730);
            lblCompareDiff.Name = "lblCompareDiff";
            lblCompareDiff.Size = new Size(0, 21);
            lblCompareDiff.TabIndex = 59;
            // 
            // FrmTests
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1751, 823);
            Controls.Add(lblCompareResults);
            Controls.Add(txtCompareResults);
            Controls.Add(btnCopyFromGolden);
            Controls.Add(label1);
            Controls.Add(txtProduct);
            Controls.Add(groupBox3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmTests";
            Text = "Testing for Product";
            WindowState = FormWindowState.Maximized;
            Load += FrmScripts_Load;
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label9;
        private ComboBox ddStreams;
        private TextBox txtRuleResults;
        private GroupBox groupBox3;
        private Label lblStatus;
        private TextBox txtProduct;
        private Label label1;
        private Button btnCopyFromGolden;
        private Button btnRunTests;
        private Button BtnRunAllTEsts;
        private TextBox txtCompareResults;
        private Label lblCompareResults;
        private Label label2;
        private Label lblCompareDiff;
    }
}