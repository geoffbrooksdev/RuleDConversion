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
            lblCountMessage = new Label();
            groupBox3 = new GroupBox();
            BtnRunAllTEsts = new Button();
            btnRunTests = new Button();
            lblStatus = new Label();
            txtProduct = new TextBox();
            label1 = new Label();
            btnCopyFromGolden = new Button();
            txtCompareResults = new TextBox();
            lblCompareResults = new Label();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(424, 61);
            label9.Name = "label9";
            label9.Size = new Size(94, 21);
            label9.TabIndex = 45;
            label9.Text = "Rule Stream";
            // 
            // ddStreams
            // 
            ddStreams.FormattingEnabled = true;
            ddStreams.Location = new Point(424, 85);
            ddStreams.Name = "ddStreams";
            ddStreams.Size = new Size(250, 23);
            ddStreams.TabIndex = 44;
            ddStreams.SelectedIndexChanged += DDStreams_SelectedIndexChanged;
            // 
            // txtRuleResults
            // 
            txtRuleResults.Location = new Point(38, 119);
            txtRuleResults.Multiline = true;
            txtRuleResults.Name = "txtRuleResults";
            txtRuleResults.ScrollBars = ScrollBars.Vertical;
            txtRuleResults.Size = new Size(552, 484);
            txtRuleResults.TabIndex = 46;
            // 
            // lblCountMessage
            // 
            lblCountMessage.AutoSize = true;
            lblCountMessage.BackColor = SystemColors.Control;
            lblCountMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCountMessage.Location = new Point(427, 638);
            lblCountMessage.Name = "lblCountMessage";
            lblCountMessage.Size = new Size(116, 21);
            lblCountMessage.TabIndex = 47;
            lblCountMessage.Text = "Rules in Group:";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.Control;
            groupBox3.Controls.Add(BtnRunAllTEsts);
            groupBox3.Controls.Add(btnRunTests);
            groupBox3.Controls.Add(txtRuleResults);
            groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(386, 32);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(614, 700);
            groupBox3.TabIndex = 49;
            groupBox3.TabStop = false;
            groupBox3.Text = "Region Testing";
            // 
            // BtnRunAllTEsts
            // 
            BtnRunAllTEsts.BackColor = SystemColors.ButtonFace;
            BtnRunAllTEsts.Font = new Font("Segoe UI", 12F);
            BtnRunAllTEsts.Location = new Point(448, 43);
            BtnRunAllTEsts.Name = "BtnRunAllTEsts";
            BtnRunAllTEsts.Size = new Size(142, 37);
            BtnRunAllTEsts.TabIndex = 55;
            BtnRunAllTEsts.Text = "Run All Tests";
            BtnRunAllTEsts.UseVisualStyleBackColor = false;
            BtnRunAllTEsts.Click += BtnRunAllTests_Click;
            // 
            // btnRunTests
            // 
            btnRunTests.BackColor = SystemColors.ButtonFace;
            btnRunTests.Font = new Font("Segoe UI", 12F);
            btnRunTests.Location = new Point(320, 43);
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
            lblStatus.Location = new Point(65, 563);
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
            txtProduct.Size = new Size(298, 29);
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
            btnCopyFromGolden.Size = new Size(298, 37);
            btnCopyFromGolden.TabIndex = 53;
            btnCopyFromGolden.Text = "Populate from Golden SQL";
            btnCopyFromGolden.UseVisualStyleBackColor = false;
            btnCopyFromGolden.Click += BtnCopyFromGolden_Click;
            // 
            // txtCompareResults
            // 
            txtCompareResults.Location = new Point(1035, 151);
            txtCompareResults.Multiline = true;
            txtCompareResults.Name = "txtCompareResults";
            txtCompareResults.ScrollBars = ScrollBars.Vertical;
            txtCompareResults.Size = new Size(485, 484);
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
            Controls.Add(lblStatus);
            Controls.Add(lblCountMessage);
            Controls.Add(label9);
            Controls.Add(ddStreams);
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
        private Label lblCountMessage;
        private GroupBox groupBox3;
        private Label lblStatus;
        private TextBox txtProduct;
        private Label label1;
        private Button btnCopyFromGolden;
        private Button btnRunTests;
        private Button BtnRunAllTEsts;
        private TextBox txtCompareResults;
        private Label lblCompareResults;
    }
}