//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)]
    public class TabStripButton : TabStripButtonBase
    {
        private Boolean _isBusy;
        private Boolean _isClosable = true;
        
        public TabStripButton()
        {
            Initialize();
        }

        public TabStripButton(string text)
            : base(text)
        {
            Initialize();
        }
        
        public TabStripButton(Image image)
            : base(image)
        {
            Initialize();
        }
        
        public TabStripButton(string text, Image image)
            : base(text, image)
        {
            Initialize();
        }
        
        public TabStripButton(string text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
            Initialize();
        }

        public TabStripButton(string text, Image image, EventHandler onClick, string name)
            : base(text, image, onClick, name)
        {
            Initialize();
        }

        private void Initialize()
        {
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            TextImageRelation = TextImageRelation.ImageBeforeText;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextAlign = ContentAlignment.MiddleLeft;
            CheckOnClick = false;
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool Checked
        {
            get { return base.Checked; }
            set { base.Checked = value; }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool CheckOnClick
        {
            get { return false; }
            set { }
        }

        [Browsable(false)]
        [DefaultValue(CheckState.Unchecked)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new CheckState CheckState
        {
            get { return base.CheckState; }
            set { base.CheckState = value; }
        }

        [DefaultValue(ContentAlignment.MiddleLeft)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
            set { base.ImageAlign = value; }
        }

        [DefaultValue(false)]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    InvalidateInternalLayout();

                    var ownerTabStrip = (Owner as TabStrip);

                    if (ownerTabStrip != null)
                    {
                        if (value)
                        {
                            ++ownerTabStrip.BusyTabCount;
                        }
                        else
                        {
                            --ownerTabStrip.BusyTabCount;
                        }

                        Invalidate();
                    }
                }
            }
        }

        [DefaultValue(true)]
        public bool IsClosable
        {
            get { return _isClosable; }
            set 
            {
                if (_isClosable != value)
                {
                    _isClosable = value;

                    if (Owner != null)
                    {
                        Owner.Invalidate();
                    }
                }
            }
        }

        [DefaultValue(ContentAlignment.MiddleLeft)]
        public override ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
            set { base.TextAlign = value; }
        }

        [DefaultValue(TextImageRelation.ImageBeforeText)]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { base.TextImageRelation = value; }
        }

        protected override ToolStripItemDisplayStyle DefaultDisplayStyle
        {
            get { return ToolStripItemDisplayStyle.ImageAndText; }
        }

        protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
        {
            var oldTabStrip = (oldParent as TabStrip);
            var newTabStrip = (newParent as TabStrip);

            if (_isBusy)
            {
                if (oldTabStrip != null)
                {
                    --oldTabStrip.BusyTabCount;
                }

                if (newTabStrip != null)
                {
                    ++newTabStrip.BusyTabCount;
                }
            }

            base.OnParentChanged(oldParent, newParent);
        }

        internal override sealed bool IsBusyInternal
        {
            get { return IsBusy; }
        }

        internal override bool IsClosableInternal
        {
            get 
            {
                if (Owner.CloseButtonVisibility == CloseButtonVisibility.Never)
                {
                    return false;
                }

                if (Owner.CloseButtonVisibility == CloseButtonVisibility.ExceptSingleTab)
                {
                    int count = 0;

                    foreach (var item in Owner.ItemsOfType<TabStripButton>())
                    {
                        ++count;
                    }

                    if (count < 2)
                    {
                        return false;
                    }
                }

                return Checked && IsClosable;
            }
        }
    }
}
