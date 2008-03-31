using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using VistaControls.TaskDialog;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace VistaControlsApp {
    public partial class Main : VistaControls.DWM.Helpers.GlassForm {

		Timer _timer;

        public Main() {
            InitializeComponent();

			this.ResizeRedraw = true;
        }

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);

			//Initialize glass sheet
			GlassMargins = new VistaControls.DWM.Margins(0, 0, 58, 28);

			//Init timer for animated footer
			_timer = new Timer();
			_timer.Interval = 4200;
			_timer.Enabled = true;
			_timer.Tick += new EventHandler(_timer_Tick);

		}

		int _count = 0;
		string[] _vFooterMsg = new string[] {
			"This is a status message...",
			"...implemented using the Glass Sheet effect...",
			"...and painted using the glow effect.",
			"It is implemented like a standard WinForms control...",
			"...therefore you can place the text label using the designer...",
			"...and you may change Text and properties in real-time,",
			"...achieving a Windows Media Player-like effect.",
			"Enjoy!"
		};

		void _timer_Tick(object sender, EventArgs e) {
			themedLabel2.Text = _vFooterMsg[(_count++ % _vFooterMsg.Length)];
		}

		#region Command link events

		private void commandLink1_Click(object sender, EventArgs e) {
            if (this.progressBar2.Value != this.progressBar2.Maximum) {
                this.progressBar1.Value += 10;
                this.progressBar2.Value += 10;
                this.progressBar3.Value += 10;
            }

        }

        private void commandLink2_Click(object sender, EventArgs e) {
            if (this.progressBar2.Value != this.progressBar2.Minimum) {
                this.progressBar1.Value -= 10;
                this.progressBar2.Value -= 10;
                this.progressBar3.Value -= 10;
            }
		}

		#endregion

		#region Task Dialog buttons

		private void tskDlg1Click(object sender, EventArgs e) {
            TaskDialog.Show("Security error", "Security error", "A security error has occurred.", TaskDialogButton.OK, TaskDialogIcon.SecurityError);
        }

        private void tskDlg2Click(object sender, EventArgs e) {
            TaskDialog.Show("Security success", "Security success", "Authentication successful.", TaskDialogButton.OK, TaskDialogIcon.SecuritySuccess);
        }

        private void tskDlg3Click(object sender, EventArgs e) {
            TaskDialog.Show("Stop", "Error", "An error has occurred.", TaskDialogButton.OK, TaskDialogIcon.Stop);
        }

        private void tskDlg4Click(object sender, EventArgs e) {
            TaskDialog.Show("Warning", "Warning", "I must warn you about something.", TaskDialogButton.OK, TaskDialogIcon.Warning);
        }

        private void tskDlg5Click(object sender, EventArgs e) {
            TaskDialog.Show("Info", "Information", "This really is important.", TaskDialogButton.OK, TaskDialogIcon.Information);
        }

        private void tskDlg6Click(object sender, EventArgs e) {
            TaskDialog.Show("UAC Shield", "UAC Shield", "You need Administrator privilegies.", TaskDialogButton.OK, TaskDialogIcon.SecurityShield);
        }

		private void tskDlgMarquee(object sender, EventArgs e) {
			TaskDialog dlg = new TaskDialog("This dialog displays a progress bar", "Marquee Progress Bar", "The progress bar below is in 'marquee' mode, that is it will not show the exact percentage of the work done, but it will show that some work is being done.", TaskDialogButton.Close);
			dlg.SetMarqueeProgressBar(true, 30);

			dlg.Show(this);
		}

        private void tskDlgComplex(object sender, EventArgs e) {
            TaskDialog dlg = new TaskDialog("This is the main instruction", "Complex Task Dialog");
            dlg.CommonIcon = TaskDialogIcon.SecurityShieldBlue;
            dlg.Content = "You may write long and informative messages, with <a href=\"http://www.google.com\">hyperlinks</a> and linebreaks.\nButtons can also be shaped as Command Link buttons instead of standard buttons. You may also use radio buttons or add a progress bar.";
            dlg.UseCommandLinks = true;
            dlg.EnableHyperlinks = true;
            dlg.CustomButtons = new CustomButton[] {
                new CustomButton(9, "Upload\nShows a fake upload task dialog."),
                new CustomButton(Result.Cancel, "Close")
            };
            dlg.RadioButtons = new CustomButton[] {
                new CustomButton(1, "First radio button"),
                new CustomButton(2, "Second radio button"),
                new CustomButton(3, "Third radio button")
            };
            dlg.ExpandedControlText = "Details";
            dlg.ExpandedInformation = "Place some \"expanded information\" here...";

            dlg.EnableRadioButton(3, false);

            //Evt registration
            dlg.ButtonClick += new EventHandler<ClickEventArgs>(dlg_ButtonClick);

            Results results = dlg.Show(this.Handle);
        }

        void dlg_ButtonClick(object sender, ClickEventArgs e) {
            if (e.ButtonID == 9) {
                e.PreventClosing = true;

                VistaControls.TaskDialog.TaskDialog newDlg = new TaskDialog("Uploading...", "Upload");
                newDlg.ShowProgressBar = true;
                newDlg.EnableCallbackTimer = true;
                newDlg.ProgressBarMaxRange = 90;
                newDlg.CustomButtons = new CustomButton[] {
                    new CustomButton(Result.Cancel, "Abort transfer")
                };
                newDlg.Footer = "Elapsed time: 0s.";
                newDlg.FooterCommonIcon = TaskDialogIcon.Information;

                VistaControls.TaskDialog.TaskDialog dlg = (VistaControls.TaskDialog.TaskDialog)sender;
                dlg.Navigate(newDlg);

                tickHandler = new EventHandler<TimerEventArgs>(dlg_Tick);
                dlg.Tick += tickHandler;
            }
        }

        EventHandler<TimerEventArgs> tickHandler;
        int cTicks = 0;
        void dlg_Tick(object sender, TimerEventArgs e) {
            VistaControls.TaskDialog.TaskDialog dlg = (VistaControls.TaskDialog.TaskDialog)sender;

            cTicks += (int)e.Ticks;
            dlg.Footer = "Elapsed time: " + cTicks / 1000 + "s.";

            if (dlg.ProgressBarState == VistaControls.ProgressBar.States.Normal) {
                dlg.ProgressBarPosition += (int)e.Ticks / 100;
                e.ResetCount = true;
            }

            if (dlg.ProgressBarPosition >= 90) {
                VistaControls.TaskDialog.TaskDialog newDlg = new TaskDialog("Upload complete.", "Upload", "Thank you!");
                newDlg.CustomButtons = new CustomButton[] {
                    new CustomButton(Result.Cancel, "Close")
                };
                dlg.Navigate(newDlg);

                dlg.Tick -= tickHandler;
            }
		}

		#endregion

		#region Split button

		private void Split_click(object sender, EventArgs e) {
			if (progressBar1.Value + 10 <= progressBar1.Maximum)
				progressBar1.Value += 10;
		}

		private void SplitMenu_increment(object sender, EventArgs e) {
			progressBar1.Value += 10;
		}

		private void SplitMenu_decrease(object sender, EventArgs e) {
			progressBar1.Value -= 10;
		}

		private void Split_opening(object sender, VistaControls.SplitButton.SplitMenuEventArgs e) {
			contextMenu1.MenuItems[0].Enabled = (progressBar1.Value < 100);
			contextMenu1.MenuItems[1].Enabled = (progressBar1.Value > 0);
		}

		#endregion

	}
}