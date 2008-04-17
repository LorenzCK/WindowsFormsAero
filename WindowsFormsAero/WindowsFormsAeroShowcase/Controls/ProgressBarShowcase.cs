using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero.Demo.Controls
{
    public partial class ProgressBarShowcase : UserControl
    {
        private readonly Button[] _buttons;

        private int _delta = 5;

        public ProgressBarShowcase()
        {
            InitializeComponent();

            _buttons = new Button[]
            {
                btnBackward, 
                btnFail,
                btnForward, 
                btnMarquee,
                btnPause,
            };
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            foreach (var button in _buttons)
            {
                button.Enabled = (button != sender);
            }

            if (sender == btnBackward)
            {
                _delta = -5;
                timer.Start();
            }

            if (sender == btnForward)
            {
                _delta = +5;
                timer.Start();
            }

            if (sender == btnMarquee)
            {
                progress.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                progress.Style = ProgressBarStyle.Continuous;
            }

            if (sender == btnFail)
            {
                progress.Status = AeroProgressBarStatus.Failed;
            }
            else if (sender == btnPause)
            {
                progress.Status = AeroProgressBarStatus.Paused;
            }
            else
            {
                progress.Status = AeroProgressBarStatus.Normal;
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if ((progress.Value + _delta) > progress.Maximum)
            {
                progress.Value = progress.Maximum;
                timer.Stop();
            }

            else if ((progress.Value + _delta) < progress.Minimum)
            {
                progress.Value = progress.Minimum;
                timer.Stop();
            }

            else
            {
                progress.Value += _delta;
            }
        }
    }
}
