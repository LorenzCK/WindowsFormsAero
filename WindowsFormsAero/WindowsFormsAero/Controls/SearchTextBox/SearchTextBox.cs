using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.Layout;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class SearchTextBox : Control
    {
        private SearchTextBoxStrip _strip;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            _strip = new SearchTextBoxStrip();            
            Controls.Add(_strip);
        }
    }
}
