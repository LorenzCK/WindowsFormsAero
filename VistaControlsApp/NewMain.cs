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
    public partial class NewMain : VistaControls.Dwm.Helpers.GlassForm {

		Timer _timer;
		Form _thumbnailedWindow = null;

        public NewMain() {
            InitializeComponent();

			_thumbnailedWindow = new ThumbnailedWindow();
			_thumbnailedWindow.Show(this);
			_thumbnailedWindow.Location = new Point(Location.X + Size.Width, Location.Y);

			thumbnailViewer1.SetThumbnail(_thumbnailedWindow, true);

			tabPage5.VisibleChanged += new EventHandler(NewMain_VisibleChanged);

			this.ResizeRedraw = true;
        }

		void NewMain_VisibleChanged(object sender, EventArgs e) {
			foreach (Control c in tabPage5.Controls)
				c.Visible = tabPage5.Visible;
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);

			//Initialize glass sheet
			GlassMargins = new VistaControls.Dwm.Margins(0, 0, 58, 28);

			//Init timer for animated footer
			_timer = new Timer();
			_timer.Interval = 4200;
			_timer.Enabled = true;
			_timer.Tick += new EventHandler(_timer_Tick);
		}

		protected override void OnClosing(CancelEventArgs e) {
			_thumbnailedWindow.Close();

			base.OnClosing(e);
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

		private void Search(object sender, EventArgs e) {
			string txt = searchTextBox1.Text;

			foreach (TabPage p in tabControl1.TabPages) {
				if (p.Text.IndexOf(txt, StringComparison.InvariantCultureIgnoreCase) >= 0) {
					tabControl1.SelectTab(p);
					return;
				}
			}

			searchTextBox1.Focus();
			//searchTextBox1.SetFocusWithoutSelection();
		}

		private void Search_cancelled(object sender, EventArgs e) {
			tabControl1.SelectTab(0);
		}

		private void td_info(object sender, EventArgs e) {
			TaskDialog.Show("Information", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.Information);
		}

		private void ts_warning(object sender, EventArgs e) {
			TaskDialog.Show("Warning", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.Warning);
		}

		private void td_error(object sender, EventArgs e) {
			TaskDialog.Show("Error", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.Stop);
		}

		private void td_shield(object sender, EventArgs e) {
			TaskDialog.Show("Shield", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.SecurityShield);
		}

		private void td_shielderror(object sender, EventArgs e) {
			TaskDialog.Show("Security error", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.SecurityError);
		}

		private void td_shieldsuccess(object sender, EventArgs e) {
			TaskDialog.Show("Security success", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.SecuritySuccess);
		}

		private void td_blueshield(object sender, EventArgs e) {
			TaskDialog.Show("Blue shield", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.SecurityShieldBlue);
		}

		private void td_grayshield(object sender, EventArgs e) {
			TaskDialog.Show("Gray shield", "Task Dialog", "Content of the task dialog.", TaskDialogButton.OK, TaskDialogIcon.SecurityShieldGray);
		}

		private void td_complex(object sender, EventArgs e) {
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

		private void td_progress(object sender, EventArgs e) {
			TaskDialog dlg = new TaskDialog("This dialog displays a progress bar", "Marquee Progress Bar", "The progress bar below is in 'marquee' mode, that is it will not show the exact percentage of the work done, but it will show that some work is being done.", TaskDialogButton.Close);
			dlg.SetMarqueeProgressBar(true, 30);

			dlg.Show(this);
		}

		private void button12_Click(object sender, EventArgs e) {
			thumbnailViewer1.Update();
		}

	}
}