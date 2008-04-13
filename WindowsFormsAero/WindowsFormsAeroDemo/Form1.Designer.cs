namespace WindowsFormsAero.Demo
{
    partial class Form1
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
            this.aeroTabControl1 = new WindowsFormsAero.AeroTabControl();
            this.aeroTabPage1 = new WindowsFormsAero.AeroTabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.aeroTabPage2 = new WindowsFormsAero.AeroTabPage();
            this.aeroTabControl1.SuspendLayout();
            this.aeroTabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // aeroTabControl1
            // 
            this.aeroTabControl1.Controls.Add(this.aeroTabPage1);
            this.aeroTabControl1.Controls.Add(this.aeroTabPage2);
            this.aeroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aeroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.aeroTabControl1.Name = "aeroTabControl1";
            this.aeroTabControl1.SelectedTab = this.aeroTabPage1;
            this.aeroTabControl1.Size = new System.Drawing.Size(604, 353);
            this.aeroTabControl1.TabIndex = 0;
            this.aeroTabControl1.Text = "aeroTabControl1";
            // 
            // aeroTabPage1
            // 
            this.aeroTabPage1.Controls.Add(this.label1);
            this.aeroTabPage1.Controls.Add(this.button1);
            this.aeroTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aeroTabPage1.Location = new System.Drawing.Point(2, 40);
            this.aeroTabPage1.Name = "aeroTabPage1";
            this.aeroTabPage1.Size = new System.Drawing.Size(600, 311);
            this.aeroTabPage1.TabIndex = 1;
            this.aeroTabPage1.Text = "aeroTabPage1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 178);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // aeroTabPage2
            // 
            this.aeroTabPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aeroTabPage2.Location = new System.Drawing.Point(2, 40);
            this.aeroTabPage2.Name = "aeroTabPage2";
            this.aeroTabPage2.Size = new System.Drawing.Size(600, 311);
            this.aeroTabPage2.TabIndex = 2;
            this.aeroTabPage2.Text = "aeroTabPage2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 353);
            this.Controls.Add(this.aeroTabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.aeroTabControl1.ResumeLayout(false);
            this.aeroTabControl1.PerformLayout();
            this.aeroTabPage1.ResumeLayout(false);
            this.aeroTabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroTabControl aeroTabControl1;
        private AeroTabPage aeroTabPage1;
        private AeroTabPage aeroTabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;

    }
}