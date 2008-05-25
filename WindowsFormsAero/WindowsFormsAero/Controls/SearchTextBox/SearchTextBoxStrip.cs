using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    internal sealed class SearchTextBoxStrip : ToolStrip
    {
        private AeroTextBox _textBox;

        private ToolStripControlHost _textBoxHost;
        private ToolStripSplitButton _searchButton;

        public SearchTextBoxStrip()
        {
            _textBox = new SearchTextBoxTextBox();
            
            _textBoxHost = new ToolStripControlHost(_textBox)
            {   
                Dock = DockStyle.Fill,
            };
            
            _searchButton = new ToolStripSplitButton(Resources.Images.SearchStart)
            {
                Dock = DockStyle.Fill,
            };
            
            SuspendLayout();

            Dock = DockStyle.Fill;
            GripStyle = ToolStripGripStyle.Hidden;
            LayoutStyle = ToolStripLayoutStyle.Table;
            Renderer = new SearchTextBoxRenderer();

            Items.Add(_textBoxHost);
            Items.Add(_searchButton);

            TableLayoutSettings.RowCount = 1; 
            TableLayoutSettings.ColumnCount = 2;
            TableLayoutSettings.ColumnStyles.Clear();
            TableLayoutSettings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            TableLayoutSettings.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

            TableLayoutSettings.SetRow(_textBoxHost, 0);
            TableLayoutSettings.SetRow(_searchButton, 0);
            TableLayoutSettings.SetColumn(_textBoxHost, 0);
            TableLayoutSettings.SetColumn(_searchButton, 1);

            ResumeLayout(true);
        }

        internal bool TextBoxFocused
        {
            get { return Focused || _textBox.Focused; }
        }

        private TableLayoutSettings TableLayoutSettings
        {
            get { return (TableLayoutSettings)(LayoutSettings); }
        }
    }
}
