namespace WindowsFormsAero.Demo.Controls
{
    partial class ProgressBarShowcase
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            this.btnForward = new WindowsFormsAero.AeroButton();
            this.btnMarquee = new WindowsFormsAero.AeroButton();
            this.btnFail = new WindowsFormsAero.AeroButton();
            this.btnPause = new WindowsFormsAero.AeroButton();
            this.btnBackward = new WindowsFormsAero.AeroButton();
            this.progress = new WindowsFormsAero.AeroProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.btnForward);
            groupBox1.Controls.Add(this.btnMarquee);
            groupBox1.Controls.Add(this.btnFail);
            groupBox1.Controls.Add(this.btnPause);
            groupBox1.Controls.Add(this.btnBackward);
            groupBox1.Controls.Add(this.progress);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            groupBox1.Location = new System.Drawing.Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(446, 144);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Progress Bar";
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(277, 51);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(95, 23);
            this.btnForward.TabIndex = 2;
            this.btnForward.Text = "Fo&rward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnMarquee
            // 
            this.btnMarquee.Location = new System.Drawing.Point(176, 109);
            this.btnMarquee.Name = "btnMarquee";
            this.btnMarquee.Size = new System.Drawing.Size(95, 23);
            this.btnMarquee.TabIndex = 2;
            this.btnMarquee.Text = "&Marquee";
            this.btnMarquee.UseVisualStyleBackColor = true;
            this.btnMarquee.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnFail
            // 
            this.btnFail.Location = new System.Drawing.Point(176, 80);
            this.btnFail.Name = "btnFail";
            this.btnFail.Size = new System.Drawing.Size(95, 23);
            this.btnFail.TabIndex = 2;
            this.btnFail.Text = "&Fail";
            this.btnFail.UseVisualStyleBackColor = true;
            this.btnFail.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(176, 51);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(95, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "&Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnBackward
            // 
            this.btnBackward.Location = new System.Drawing.Point(75, 51);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(95, 23);
            this.btnBackward.TabIndex = 2;
            this.btnBackward.Text = "&Backward";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(6, 21);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(434, 23);
            this.progress.TabIndex = 0;
            this.progress.Value = 33;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // ProgressBarShowcase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(groupBox1);
            this.Name = "ProgressBarShowcase";
            this.Size = new System.Drawing.Size(446, 144);
            groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroButton btnForward;
        private AeroButton btnMarquee;
        private AeroButton btnFail;
        private AeroButton btnPause;
        private AeroButton btnBackward;
        private AeroProgressBar progress;
        private System.Windows.Forms.Timer timer;
    }
}
