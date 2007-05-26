namespace VistaControls
{
    partial class frmExample
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowShield = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowCueBanner = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.shieldLink2 = new System.Windows.Forms.Vista.ShieldLink();
            this.shieldLink1 = new System.Windows.Forms.Vista.ShieldLink();
            this.cueBannerTextBox = new System.Windows.Forms.Vista.CueBannerTextBox();
            this.shieldButton = new System.Windows.Forms.Vista.ShieldButton();
            this.btnTreeViewEx = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowShield);
            this.groupBox1.Controls.Add(this.shieldButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 113);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Shield Button";
            // 
            // chkShowShield
            // 
            this.chkShowShield.AutoSize = true;
            this.chkShowShield.Checked = true;
            this.chkShowShield.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowShield.Location = new System.Drawing.Point(21, 78);
            this.chkShowShield.Name = "chkShowShield";
            this.chkShowShield.Size = new System.Drawing.Size(85, 17);
            this.chkShowShield.TabIndex = 1;
            this.chkShowShield.Text = "&Show Shield";
            this.chkShowShield.UseVisualStyleBackColor = true;
            this.chkShowShield.CheckedChanged += new System.EventHandler(this.chkShowShield_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowCueBanner);
            this.groupBox2.Controls.Add(this.cueBannerTextBox);
            this.groupBox2.Location = new System.Drawing.Point(194, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 113);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cue Banner TextBox";
            // 
            // chkShowCueBanner
            // 
            this.chkShowCueBanner.AutoSize = true;
            this.chkShowCueBanner.Checked = true;
            this.chkShowCueBanner.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCueBanner.Location = new System.Drawing.Point(15, 78);
            this.chkShowCueBanner.Name = "chkShowCueBanner";
            this.chkShowCueBanner.Size = new System.Drawing.Size(112, 17);
            this.chkShowCueBanner.TabIndex = 2;
            this.chkShowCueBanner.Text = "S&how Cue Banner";
            this.chkShowCueBanner.UseVisualStyleBackColor = true;
            this.chkShowCueBanner.CheckedChanged += new System.EventHandler(this.chkShowCueBanner_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.shieldLink2);
            this.groupBox3.Controls.Add(this.shieldLink1);
            this.groupBox3.Location = new System.Drawing.Point(12, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 98);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Shield Link";
            // 
            // shieldLink2
            // 
            this.shieldLink2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shieldLink2.Location = new System.Drawing.Point(234, 30);
            this.shieldLink2.Name = "shieldLink2";
            this.shieldLink2.ShowShield = true;
            this.shieldLink2.Size = new System.Drawing.Size(213, 47);
            this.shieldLink2.TabIndex = 1;
            this.shieldLink2.Text = "ShieldLink Example 2";
            this.shieldLink2.TextNote = "Additional Information";
            this.shieldLink2.UseVisualStyleBackColor = true;
            // 
            // shieldLink1
            // 
            this.shieldLink1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shieldLink1.Location = new System.Drawing.Point(15, 30);
            this.shieldLink1.Name = "shieldLink1";
            this.shieldLink1.Size = new System.Drawing.Size(213, 47);
            this.shieldLink1.TabIndex = 0;
            this.shieldLink1.Text = "ShieldLink Example 1";
            this.shieldLink1.TextNote = "Additional Information";
            this.shieldLink1.UseVisualStyleBackColor = true;
            // 
            // cueBannerTextBox
            // 
            this.cueBannerTextBox.CueBannerText = "Cue Banner Text";
            this.cueBannerTextBox.Location = new System.Drawing.Point(15, 38);
            this.cueBannerTextBox.Name = "cueBannerTextBox";
            this.cueBannerTextBox.Size = new System.Drawing.Size(250, 20);
            this.cueBannerTextBox.TabIndex = 0;
            // 
            // shieldButton
            // 
            this.shieldButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.shieldButton.Location = new System.Drawing.Point(21, 33);
            this.shieldButton.Name = "shieldButton";
            this.shieldButton.Size = new System.Drawing.Size(102, 28);
            this.shieldButton.TabIndex = 0;
            this.shieldButton.Text = "Shield Button";
            this.shieldButton.UseVisualStyleBackColor = true;
            this.shieldButton.Click += new System.EventHandler(this.shieldButton_Click);
            // 
            // btnTreeViewEx
            // 
            this.btnTreeViewEx.Location = new System.Drawing.Point(12, 261);
            this.btnTreeViewEx.Name = "btnTreeViewEx";
            this.btnTreeViewEx.Size = new System.Drawing.Size(175, 29);
            this.btnTreeViewEx.TabIndex = 4;
            this.btnTreeViewEx.Text = "&Open TreeViewEx Example";
            this.btnTreeViewEx.UseVisualStyleBackColor = true;
            this.btnTreeViewEx.Click += new System.EventHandler(this.btnTreeViewEx_Click);
            // 
            // frmExample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 311);
            this.Controls.Add(this.btnTreeViewEx);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExample";
            this.Text = "Vista Controls for .NET 2.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Vista.ShieldButton shieldButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkShowShield;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowCueBanner;
        private System.Windows.Forms.Vista.CueBannerTextBox cueBannerTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Vista.ShieldLink shieldLink2;
        private System.Windows.Forms.Vista.ShieldLink shieldLink1;
        private System.Windows.Forms.Button btnTreeViewEx;
    }
}

