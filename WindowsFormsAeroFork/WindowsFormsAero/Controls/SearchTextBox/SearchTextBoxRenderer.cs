using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsAero
{
    internal sealed class SearchTextBoxRenderer : ToolStripAeroRenderer
    {
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            var strip = e.ToolStrip as SearchTextBoxStrip;

            if ((strip != null) && (VistaOSFeature.IsRunningAeroTheme))
            {
                var element = VisualStyleElement.TextBox.TextEdit.Normal;

                if (!strip.Enabled)
                {
                    element = VisualStyleElement.TextBox.TextEdit.Disabled;
                }
                else if (strip.TextBoxFocused)
                {
                    element = VisualStyleElement.TextBox.TextEdit.Focused;
                }

                var renderer = new VisualStyleRenderer(element);
                renderer.DrawBackground(e.Graphics, e.AffectedBounds);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }

        protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
        {

            base.OnRenderSplitButtonBackground(e);
        }
    }
}
