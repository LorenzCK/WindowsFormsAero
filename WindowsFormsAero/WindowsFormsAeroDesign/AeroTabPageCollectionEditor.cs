using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;

namespace WindowsFormsAero.Design
{
    internal sealed class AeroTabPageCollectionEditor : CollectionEditor
    {
        public AeroTabPageCollectionEditor()
            : base(typeof(AeroTabControl.TabPageCollection))
        {

        }

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(AeroTabPage) };
        }

        protected override void DestroyInstance(object instance)
        {
            base.DestroyInstance(instance);
        }

        protected override object SetItems(object editValue, object[] value)
        {
            var control = Context.Instance as AeroTabControl;

            if (control != null)
            {
                Context.OnComponentChanging();

                var index = control.SelectedTabIndex;

                control.SuspendLayout();
                control.TabPages.Clear();

                foreach (AeroTabPage page in value)
                {
                    control.TabPages.Add(page);
                }

                if ((index >= control.TabPages.Count) ||
                    ((index == -1) && (control.TabPages.Count > 0)))
                {
                    index = control.TabPages.Count - 1;
                }

                control.SelectedTabIndex = index;
                control.ResumeLayout();

                Context.OnComponentChanged();
            }

            return editValue;
        }
    }
}