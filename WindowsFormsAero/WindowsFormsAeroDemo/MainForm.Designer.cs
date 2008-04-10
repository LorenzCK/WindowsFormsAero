namespace WindowsFormsAero.Demo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chkTabIsBusy = new System.Windows.Forms.CheckBox();
            this.cbkTabStripBackground = new System.Windows.Forms.CheckBox();
            this.chkDebugRenderer = new System.Windows.Forms.CheckBox();
            this.tabPageSwitcher1 = new WindowsFormsAero.TabPageSwitcher();
            this.tabStripPage5 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage1 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage2 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage6 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage4 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage3 = new WindowsFormsAero.TabStripPage();
            this.tabStripPage7 = new WindowsFormsAero.TabStripPage();
            this.tabStrip1 = new WindowsFormsAero.TabStrip();
            this.tab0 = new WindowsFormsAero.TabStripButton();
            this.tab1 = new WindowsFormsAero.TabStripButton();
            this.tab2 = new WindowsFormsAero.TabStripButton();
            this.tab3 = new WindowsFormsAero.TabStripButton();
            this.tab4 = new WindowsFormsAero.TabStripButton();
            this.tab5 = new WindowsFormsAero.TabStripButton();
            this.tab6 = new WindowsFormsAero.TabStripButton();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPageSwitcher1.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkTabIsBusy
            // 
            this.chkTabIsBusy.AutoSize = true;
            this.chkTabIsBusy.Location = new System.Drawing.Point(91, 248);
            this.chkTabIsBusy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkTabIsBusy.Name = "chkTabIsBusy";
            this.chkTabIsBusy.Size = new System.Drawing.Size(142, 24);
            this.chkTabIsBusy.TabIndex = 2;
            this.chkTabIsBusy.Text = "Current Tab Busy";
            this.chkTabIsBusy.UseVisualStyleBackColor = true;
            this.chkTabIsBusy.CheckedChanged += new System.EventHandler(this.chkTabIsBusy_CheckedChanged);
            // 
            // cbkTabStripBackground
            // 
            this.cbkTabStripBackground.AutoSize = true;
            this.cbkTabStripBackground.Location = new System.Drawing.Point(91, 315);
            this.cbkTabStripBackground.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbkTabStripBackground.Name = "cbkTabStripBackground";
            this.cbkTabStripBackground.Size = new System.Drawing.Size(170, 24);
            this.cbkTabStripBackground.TabIndex = 0;
            this.cbkTabStripBackground.Text = "TabStrip Background";
            this.cbkTabStripBackground.UseVisualStyleBackColor = true;
            this.cbkTabStripBackground.CheckedChanged += new System.EventHandler(this.cbkTabStripBackground_CheckedChanged);
            // 
            // chkDebugRenderer
            // 
            this.chkDebugRenderer.AutoSize = true;
            this.chkDebugRenderer.Checked = true;
            this.chkDebugRenderer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebugRenderer.Location = new System.Drawing.Point(91, 281);
            this.chkDebugRenderer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDebugRenderer.Name = "chkDebugRenderer";
            this.chkDebugRenderer.Size = new System.Drawing.Size(136, 24);
            this.chkDebugRenderer.TabIndex = 3;
            this.chkDebugRenderer.Text = "Debug renderer";
            this.chkDebugRenderer.UseVisualStyleBackColor = true;
            this.chkDebugRenderer.CheckedChanged += new System.EventHandler(this.chkDebugRenderer_CheckedChanged);
            // 
            // tabPageSwitcher1
            // 
            this.tabPageSwitcher1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage5);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage1);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage2);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage6);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage4);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage3);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage7);
            this.tabPageSwitcher1.Location = new System.Drawing.Point(86, 102);
            this.tabPageSwitcher1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageSwitcher1.Name = "tabPageSwitcher1";
            this.tabPageSwitcher1.SelectedTabStripPage = null;
            this.tabPageSwitcher1.Size = new System.Drawing.Size(381, 141);
            this.tabPageSwitcher1.TabIndex = 1;
            this.tabPageSwitcher1.TabStrip = this.tabStrip1;
            this.tabPageSwitcher1.Text = "tabPageSwitcher1";
            // 
            // tabStripPage5
            // 
            this.tabStripPage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage5.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage5.Name = "tabStripPage5";
            this.tabStripPage5.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage5.TabIndex = 4;
            // 
            // tabStripPage1
            // 
            this.tabStripPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage1.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage1.Name = "tabStripPage1";
            this.tabStripPage1.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage1.TabIndex = 0;
            // 
            // tabStripPage2
            // 
            this.tabStripPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage2.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage2.Name = "tabStripPage2";
            this.tabStripPage2.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage2.TabIndex = 1;
            // 
            // tabStripPage6
            // 
            this.tabStripPage6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage6.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage6.Name = "tabStripPage6";
            this.tabStripPage6.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage6.TabIndex = 5;
            // 
            // tabStripPage4
            // 
            this.tabStripPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage4.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage4.Name = "tabStripPage4";
            this.tabStripPage4.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage4.TabIndex = 3;
            // 
            // tabStripPage3
            // 
            this.tabStripPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage3.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage3.Name = "tabStripPage3";
            this.tabStripPage3.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage3.TabIndex = 2;
            // 
            // tabStripPage7
            // 
            this.tabStripPage7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage7.Location = new System.Drawing.Point(2, 2);
            this.tabStripPage7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabStripPage7.Name = "tabStripPage7";
            this.tabStripPage7.Size = new System.Drawing.Size(377, 137);
            this.tabStripPage7.TabIndex = 6;
            // 
            // tabStrip1
            // 
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tab0,
            this.tab1,
            this.tab2,
            this.tab3,
            this.tab4,
            this.tab5,
            this.tab6});
            this.tabStrip1.Location = new System.Drawing.Point(0, 0);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 2, 5);
            this.tabStrip1.SelectedTab = this.tab2;
            this.tabStrip1.Size = new System.Drawing.Size(554, 39);
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // tab0
            // 
            this.tab0.Image = ((System.Drawing.Image)(resources.GetObject("tab0.Image")));
            this.tab0.Name = "tab0";
            this.tab0.Size = new System.Drawing.Size(78, 32);
            this.tab0.TabStripPage = this.tabStripPage1;
            this.tab0.Text = "tab0 - huuhahaha";
            // 
            // tab1
            // 
            this.tab1.Image = ((System.Drawing.Image)(resources.GetObject("tab1.Image")));
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(78, 32);
            this.tab1.TabStripPage = this.tabStripPage2;
            this.tab1.Text = "tab1 - tralala";
            // 
            // tab2
            // 
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(78, 32);
            this.tab2.Text = "tab2 nbvbhjgh";
            // 
            // tab3
            // 
            this.tab3.Image = ((System.Drawing.Image)(resources.GetObject("tab3.Image")));
            this.tab3.Name = "tab3";
            this.tab3.Size = new System.Drawing.Size(78, 32);
            this.tab3.TabStripPage = this.tabStripPage3;
            this.tab3.Text = "tab3 fdsafsaf";
            this.tab3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // tab4
            // 
            this.tab4.Image = ((System.Drawing.Image)(resources.GetObject("tab4.Image")));
            this.tab4.Name = "tab4";
            this.tab4.Size = new System.Drawing.Size(78, 32);
            this.tab4.TabStripPage = this.tabStripPage4;
            this.tab4.Text = "tab4 fsadfsadf";
            // 
            // tab5
            // 
            this.tab5.Image = ((System.Drawing.Image)(resources.GetObject("tab5.Image")));
            this.tab5.Name = "tab5";
            this.tab5.Size = new System.Drawing.Size(78, 32);
            this.tab5.TabStripPage = this.tabStripPage5;
            this.tab5.Text = "tab5 fsdfsaf";
            // 
            // tab6
            // 
            this.tab6.Image = ((System.Drawing.Image)(resources.GetObject("tab6.Image")));
            this.tab6.Name = "tab6";
            this.tab6.Size = new System.Drawing.Size(78, 32);
            this.tab6.TabStripPage = this.tabStripPage6;
            this.tab6.Text = "tab6 utyfjg";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(309, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Toggle Right-to-Left";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 385);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkDebugRenderer);
            this.Controls.Add(this.cbkTabStripBackground);
            this.Controls.Add(this.chkTabIsBusy);
            this.Controls.Add(this.tabPageSwitcher1);
            this.Controls.Add(this.tabStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.tabPageSwitcher1.ResumeLayout(false);
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WindowsFormsAero.TabStrip tabStrip1;
        private WindowsFormsAero.TabStripButton tab0;
        private WindowsFormsAero.TabStripPage tabStripPage1;
        private WindowsFormsAero.TabStripButton tab1;
        private WindowsFormsAero.TabStripPage tabStripPage2;
        private WindowsFormsAero.TabStripButton tab3;
        private WindowsFormsAero.TabStripPage tabStripPage3;
        private WindowsFormsAero.TabStripButton tab4;
        private WindowsFormsAero.TabStripPage tabStripPage4;
        private WindowsFormsAero.TabPageSwitcher tabPageSwitcher1;
        private WindowsFormsAero.TabStripButton tab5;
        private WindowsFormsAero.TabStripPage tabStripPage5;
        private WindowsFormsAero.TabStripButton tab6;
        private WindowsFormsAero.TabStripPage tabStripPage6;
        private WindowsFormsAero.TabStripPage tabStripPage7;
        private WindowsFormsAero.TabStripButton tab2;
        private System.Windows.Forms.CheckBox cbkTabStripBackground;
        private System.Windows.Forms.CheckBox chkTabIsBusy;
        private System.Windows.Forms.CheckBox chkDebugRenderer;
        private System.Windows.Forms.Button button1;



    }
}