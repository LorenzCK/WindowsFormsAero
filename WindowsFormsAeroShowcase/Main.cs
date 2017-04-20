using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero.TaskDialog;

namespace WindowsFormsAeroShowcase {

    public partial class Main : WindowsFormsAero.AeroForm {

        private Form _thumbnailedWindow = null;

        public Main() {
            InitializeComponent();

            _thumbnailedWindow = new ThumbnailedWindow();
            _thumbnailedWindow.Show(this);
            _thumbnailedWindow.Location = new Point(Location.X + Size.Width, Location.Y);

            thumbnailViewer1.SetThumbnail(_thumbnailedWindow, true);

            ResizeRedraw = true;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            GlassMargins = new Padding(0, themedLabel1.Height, 0, themedLabel2.Height);
        }

        protected override void OnClosing(CancelEventArgs e) {
            _thumbnailedWindow.Close();

            base.OnClosing(e);
        }

        private void td_info(object sender, EventArgs e) {
            TaskDialog.Show("Information", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.Information);
        }

        private void ts_warning(object sender, EventArgs e) {
            TaskDialog.Show("Warning", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.Warning);
        }

        private void td_error(object sender, EventArgs e) {
            TaskDialog.Show("Error", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.Stop);
        }

        private void td_shield(object sender, EventArgs e) {
            TaskDialog.Show("Shield", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.SecurityShield);
        }

        private void td_shielderror(object sender, EventArgs e) {
            TaskDialog.Show("Security error", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.SecurityError);
        }

        private void td_shieldsuccess(object sender, EventArgs e) {
            TaskDialog.Show("Security success", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.SecuritySuccess);
        }

        private void td_blueshield(object sender, EventArgs e) {
            TaskDialog.Show("Blue shield", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.SecurityShieldBlue);
        }

        private void td_grayshield(object sender, EventArgs e) {
            TaskDialog.Show("Gray shield", "Task Dialog", "Content of the task dialog.", CommonButton.OK, CommonIcon.SecurityShieldGray);
        }

        private void td_complex(object sender, EventArgs e) {
            TaskDialog dlg = new TaskDialog("This is the main instruction", "Complex Task Dialog");
            dlg.CommonIcon = CommonIcon.SecurityShieldBlue;
            dlg.Content = "You may write long and informative messages, with <a href=\"http://www.google.com\">hyperlinks</a> and linebreaks.\nButtons can also be shaped as Command Link buttons instead of standard buttons. You may also use radio buttons or add a progress bar.";
            dlg.UseCommandLinks = true;
            dlg.EnableHyperlinks = true;
            dlg.CustomButtons = new CustomButton[] {
                new CustomButton(9, "Upload\nShows a fake upload task dialog."),
                new CustomButton(CommonButtonResult.Cancel, "Close")
            };
            dlg.RadioButtons = new CustomButton[] {
                new CustomButton(1, "First radio button"),
                new CustomButton(2, "Second radio button"),
                new CustomButton(3, "Third radio button")
            };
            dlg.ExpandedControlText = "Details";
            dlg.ExpandedInformation = "Place some \"expanded information\" here...";

            dlg.EnableRadioButton(3, false);

            dlg.ButtonClick += new EventHandler<ClickEventArgs>(dlg_ButtonClick);

            TaskDialogResult results = dlg.Show(this.Handle);
        }

        private void dlg_ButtonClick(object sender, ClickEventArgs e) {
            if (e.ButtonID == 9) {
                e.PreventClosing = true;

                WindowsFormsAero.TaskDialog.TaskDialog newDlg = new TaskDialog("Uploading...", "Upload");
                newDlg.ShowProgressBar = true;
                newDlg.EnableCallbackTimer = true;
                newDlg.ProgressBarMaxRange = 90;
                newDlg.CustomButtons = new CustomButton[] {
                    new CustomButton(CommonButtonResult.Cancel, "Abort transfer")
                };
                newDlg.Footer = "Elapsed time: 0s.";
                newDlg.FooterCommonIcon = CommonIcon.Information;

                WindowsFormsAero.TaskDialog.TaskDialog dlg = (WindowsFormsAero.TaskDialog.TaskDialog)sender;
                dlg.Navigate(newDlg);

                tickHandler = new EventHandler<TimerEventArgs>(dlg_Tick);
                dlg.Tick += tickHandler;
            }
        }

        private EventHandler<TimerEventArgs> tickHandler;
        private int cTicks = 0;
        private void dlg_Tick(object sender, TimerEventArgs e) {
            WindowsFormsAero.TaskDialog.TaskDialog dlg = (WindowsFormsAero.TaskDialog.TaskDialog)sender;

            cTicks += (int)e.Ticks;
            dlg.Footer = "Elapsed time: " + cTicks / 1000 + "s.";

            if (dlg.ProgressBarState == WindowsFormsAero.ProgressBarState.Normal) {
                dlg.ProgressBarPosition += (int)e.Ticks / 100;
                e.ResetCount = true;
            }

            if (dlg.ProgressBarPosition >= 90) {
                WindowsFormsAero.TaskDialog.TaskDialog newDlg = new TaskDialog("Upload complete.", "Upload", "Thank you!");
                newDlg.CustomButtons = new CustomButton[] {
                    new CustomButton(CommonButtonResult.Cancel, "Close")
                };
                dlg.Navigate(newDlg);

                dlg.Tick -= tickHandler;
            }
        }

        private void td_progress(object sender, EventArgs e) {
            TaskDialog dlg = new TaskDialog("This dialog displays a progress bar", "Marquee Progress Bar", "The progress bar below is in 'marquee' mode, that is it will not show the exact percentage of the work done, but it will show that some work is being done.", CommonButton.Close);
            dlg.SetMarqueeProgressBar(true, 30);

            dlg.Show(this);
        }

        private void commandLink3_Click(object sender, EventArgs e) {
            ControlPanel cp = new ControlPanel();
            cp.ShowDialog();
            cp.Dispose();
        }

        private void commandLink1_Click(object sender, EventArgs e) {

        }

        private void commandLink4_Click(object sender, EventArgs e) {
            HorizontalPanelExample hp = new HorizontalPanelExample();
            hp.ShowDialog();
            hp.Dispose();
        }

    }

}
