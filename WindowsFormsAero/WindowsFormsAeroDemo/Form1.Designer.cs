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
            this.aeroTabPage2 = new WindowsFormsAero.AeroTabPage();
            this.aeroTabControl1.SuspendLayout();
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
            this.aeroTabPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aeroTabPage1.Location = new System.Drawing.Point(2, 40);
            this.aeroTabPage1.Name = "aeroTabPage1";
            this.aeroTabPage1.Size = new System.Drawing.Size(600, 311);
            this.aeroTabPage1.TabIndex = 1;
            this.aeroTabPage1.Text = "aeroTabPage1";
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
            this.ResumeLayout(false);

        }

        #endregion

        private AeroTabControl aeroTabControl1;
        private AeroTabPage aeroTabPage1;
        private AeroTabPage aeroTabPage2;

    }
}