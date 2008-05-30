using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.ComponentModel.ProvideProperty("FixAlignmentOnTabletPC", typeof(ToolStrip))]
    public class MenuStripTabletFixProvider : Component, IExtenderProvider
    {
        private List<ToolStrip> _strips;

        public MenuStripTabletFixProvider()
        {
        }

        public MenuStripTabletFixProvider(IContainer container)
        {
            container.Add(this);
        }

        [DefaultValue(false)]
        public bool GetFixAlignmentOnTabletPC(ToolStrip strip)
        {
            if (_strips == null)
            {
                return false;
            }

            return _strips.Contains(strip);
        }

        public void SetFixAlignmentOnTabletPC(ToolStrip strip, Boolean value)
        {
            if (GetFixAlignmentOnTabletPC(strip) != value)
            {
                if (value)
                {
                    if (!DesignMode)
                    {
                        Attach(strip);
                    }

                    if (_strips == null)
                    {
                        _strips = new List<ToolStrip>();
                    }

                    _strips.Add(strip);
                }
                else
                {
                    if (!DesignMode)
                    {
                        Detach(strip);
                    }

                    if (_strips != null)
                    {
                        _strips.Remove(strip);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !DesignMode && (_strips != null))
            {
                foreach (var item in _strips)
                {
                    Detach(item);
                }
            }

            _strips = null;

            base.Dispose(disposing);
        }

        private void Attach(ToolStrip strip)
        {
            var dropDown = (strip as ToolStripDropDown);

            if (dropDown != null)
            {
                dropDown.Opening += OnDropDownOpening;
            }

            AttachAll(strip.Items);

            strip.ItemAdded += OnItemAdded;
            strip.ItemRemoved -= OnItemRemoved;
        }

        private void AttachAll(ToolStripItemCollection items)
        {
            foreach (var item in items)
            {
                var dropDownItem = item as ToolStripDropDownItem;

                if (dropDownItem != null)
                {
                    Attach(dropDownItem.DropDown);
                }
            }
        }

        private void Detach(ToolStrip strip)
        {
            var dropDown = (strip as ToolStripDropDown);

            if (dropDown != null)
            {
                dropDown.Opening -= OnDropDownOpening;
            }

            strip.ItemAdded -= OnItemAdded;
            strip.ItemRemoved -= OnItemRemoved;

            DetachAll(strip.Items);
        }

        private void DetachAll(ToolStripItemCollection items)
        {
            foreach (var item in items)
            {
                var dropDownItem = item as ToolStripDropDownItem;

                if (dropDownItem != null)
                {
                    Detach(dropDownItem.DropDown);
                }
            }
        }

        private void OnItemAdded(object sender, ToolStripItemEventArgs e)
        {
            var item = (e.Item as ToolStripDropDownItem);

            if (item != null)
            {
                Attach(item.DropDown);
            }
        }

        private void OnItemRemoved(object sender, ToolStripItemEventArgs e)
        {
            var item = (e.Item as ToolStripDropDownItem);

            if (item != null)
            {
                Detach(item.DropDown);
            }
        }

        private void OnDropDownOpening(object sender, EventArgs e)
        {
            var dropDown = (sender as ToolStripDropDown);

            if (dropDown != null)
            {
                var item = dropDown.OwnerItem;
                var direction = ToolStripDropDownDirection.Default;

                if (SystemInformation.RightAlignedMenus)
                {
                    if ((item != null) && (item.Owner is MenuStrip))
                    {
                        direction = ToolStripDropDownDirection.BelowLeft;
                    }
                    else
                    {
                        direction = ToolStripDropDownDirection.Left;
                    }
                }

                var dropDownItem = (item as ToolStripDropDownItem);

                if (dropDownItem != null)
                {
                    dropDownItem.DropDownDirection = direction;
                }
            }
        }

        public bool CanExtend(object extendee)
        {
            return extendee is ToolStrip;
        }
    }
}
