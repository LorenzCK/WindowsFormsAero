using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace WindowsFormsAero.Design
{
    public sealed class AeroTabControlSelectedPageEditor : ObjectSelectorEditor
    {
        protected override void FillTreeWithData(Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
        {
            base.FillTreeWithData(selector, context, provider);

            var tabControl = context.Instance as AeroTabControl;

            if (tabControl != null)
            {
                foreach (var page in tabControl.TabPages)
                {
                    var node = new SelectorNode(page.Name, page);

                    selector.Nodes.Add(node);

                    if (tabControl.SelectedTab == page)
                    {
                        selector.SelectedNode = node;
                    }
                }
            }
        }
    }
}
