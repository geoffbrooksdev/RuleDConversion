namespace RuleDConversion
{
    partial class FrmScripts
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
            txtScriptOutputFolder = new TextBox();
            label3 = new Label();
            button1 = new Button();
            lblStatus = new Label();
            btnLoadGolden = new Button();
            btnRebuild = new Button();
            tableRegions = new TableLayoutPanel();
            btnTestRules = new Button();
            label9 = new Label();
            ddStreams = new ComboBox();
            txtRuleResults = new TextBox();
            lblCountMessage = new Label();
            groupBox1 = new GroupBox();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtScriptOutputFolder
            // 
            txtScriptOutputFolder.Font = new Font("Segoe UI", 12F);
            txtScriptOutputFolder.Location = new Point(12, 598);
            txtScriptOutputFolder.Name = "txtScriptOutputFolder";
            txtScriptOutputFolder.Size = new Size(298, 29);
            txtScriptOutputFolder.TabIndex = 2;
            txtScriptOutputFolder.Text = "c:\\oratemp\\test\\";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.GradientInactiveCaption;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(12, 568);
            label3.Name = "label3";
            label3.Size = new Size(151, 21);
            label3.TabIndex = 3;
            label3.Text = "Script Output Folder";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.Font = new Font("Segoe UI", 12F);
            button1.Location = new Point(10, 637);
            button1.Name = "button1";
            button1.Size = new Size(83, 34);
            button1.TabIndex = 4;
            button1.Text = "Go";
            button1.UseVisualStyleBackColor = false;
            button1.Click += Button1_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 12F);
            lblStatus.Location = new Point(12, 247);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(44, 21);
            lblStatus.TabIndex = 5;
            lblStatus.Text = "Idle...";
            // 
            // btnLoadGolden
            // 
            btnLoadGolden.BackColor = SystemColors.ButtonFace;
            btnLoadGolden.Font = new Font("Segoe UI", 12F);
            btnLoadGolden.Location = new Point(6, 105);
            btnLoadGolden.Name = "btnLoadGolden";
            btnLoadGolden.Size = new Size(203, 37);
            btnLoadGolden.TabIndex = 8;
            btnLoadGolden.Text = "Populate from Golden SQL";
            btnLoadGolden.UseVisualStyleBackColor = false;
            btnLoadGolden.Click += BtnLoadGolden_Click;
            // 
            // btnRebuild
            // 
            btnRebuild.BackColor = SystemColors.ButtonFace;
            btnRebuild.Font = new Font("Segoe UI", 12F);
            btnRebuild.Location = new Point(6, 171);
            btnRebuild.Name = "btnRebuild";
            btnRebuild.Size = new Size(203, 42);
            btnRebuild.TabIndex = 9;
            btnRebuild.Text = "Rebuild Groups && Streams";
            btnRebuild.UseVisualStyleBackColor = false;
            btnRebuild.Click += BtnRebuild_Click;
            // 
            // tableRegions
            // 
            tableRegions.AutoSize = true;
            tableRegions.ColumnCount = 4;
            tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableRegions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableRegions.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableRegions.Location = new Point(6, 53);
            tableRegions.Name = "tableRegions";
            tableRegions.RowCount = 1;
            tableRegions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableRegions.Size = new Size(439, 52);
            tableRegions.TabIndex = 10;
            // 
            // btnTestRules
            // 
            btnTestRules.Location = new Point(1209, 79);
            btnTestRules.Name = "btnTestRules";
            btnTestRules.Size = new Size(155, 32);
            btnTestRules.TabIndex = 12;
            btnTestRules.Text = "Test D Rules in Stream";
            btnTestRules.UseVisualStyleBackColor = true;
            btnTestRules.Click += BtnTestRules_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.Control;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(1040, 61);
            label9.Name = "label9";
            label9.Size = new Size(94, 21);
            label9.TabIndex = 45;
            label9.Text = "Rule Stream";
            // 
            // ddStreams
            // 
            ddStreams.FormattingEnabled = true;
            ddStreams.Location = new Point(1040, 85);
            ddStreams.Name = "ddStreams";
            ddStreams.Size = new Size(134, 23);
            ddStreams.TabIndex = 44;
            // 
            // txtRuleResults
            // 
            txtRuleResults.Location = new Point(1040, 137);
            txtRuleResults.Multiline = true;
            txtRuleResults.Name = "txtRuleResults";
            txtRuleResults.ScrollBars = ScrollBars.Vertical;
            txtRuleResults.Size = new Size(586, 484);
            txtRuleResults.TabIndex = 46;
            // 
            // lblCountMessage
            // 
            lblCountMessage.AutoSize = true;
            lblCountMessage.BackColor = SystemColors.Control;
            lblCountMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCountMessage.Location = new Point(1043, 638);
            lblCountMessage.Name = "lblCountMessage";
            lblCountMessage.Size = new Size(116, 21);
            lblCountMessage.TabIndex = 47;
            lblCountMessage.Text = "Rules in Group:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.CausesValidation = false;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnRebuild);
            groupBox1.Controls.Add(btnLoadGolden);
            groupBox1.Controls.Add(lblStatus);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(txtScriptOutputFolder);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(12, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(386, 708);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Script Actions";
            // 
            // label2
            // 
            label2.BackColor = SystemColors.GradientInactiveCaption;
            label2.Location = new Point(12, 455);
            label2.Name = "label2";
            label2.Size = new Size(348, 51);
            label2.TabIndex = 11;
            label2.Text = "When all rule updates are complete: generate a new set of scripts";
            // 
            // label1
            // 
            label1.Location = new Point(10, 29);
            label1.Name = "label1";
            label1.Size = new Size(332, 62);
            label1.TabIndex = 10;
            label1.Text = "These actions are done when a new golden release is available";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.GradientInactiveCaption;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(0, 442);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(376, 260);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.Control;
            groupBox2.Controls.Add(tableRegions);
            groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(404, 32);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(592, 708);
            groupBox2.TabIndex = 48;
            groupBox2.TabStop = false;
            groupBox2.Text = "Region Counts";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.Control;
            groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(1002, 32);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(718, 708);
            groupBox3.TabIndex = 49;
            groupBox3.TabStop = false;
            groupBox3.Text = "Region Testing";
            // 
            // FrmScripts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1751, 823);
            Controls.Add(lblCountMessage);
            Controls.Add(txtRuleResults);
            Controls.Add(label9);
            Controls.Add(ddStreams);
            Controls.Add(btnTestRules);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmScripts";
            Text = "Script Generator";
            WindowState = FormWindowState.Maximized;
            Load += FrmScripts_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private TextBox txtScriptOutputFolder;
        private Label label3;
        private Button button1;
        private Label lblStatus;
        private Button btnLoadGolden;
        private Button btnRebuild;
        private TableLayoutPanel tableRegions;
        private Button btnTestRules;
        private Label label9;
        private ComboBox ddStreams;
        private TextBox txtRuleResults;
        private Label lblCountMessage;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
    }
}