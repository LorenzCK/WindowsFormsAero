using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Runtime.InteropServices;

namespace WindowsFormsAero
{
    [ProvideProperty("HideWhenInactive", typeof(MenuStrip))]
    [System.ComponentModel.DesignerCategory("code")]
    [System.ComponentModel.Designer("WindowsFormsAero.Design.HiddenMenuStripDesigner, " + ThisAssembly.DesignAssemblyFullName)]
    public class HiddenMenuStrip : Component, IExtenderProvider
    {
        private List<MenuStrip> _strips;
        private Boolean _keepHidden;

        public HiddenMenuStrip()
        {
        }

        public HiddenMenuStrip(IContainer container)
        {
            container.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
            {
                if (!DesignMode)
                {
                    foreach (var item in _strips)
                    {
                        MenuStripMessageFilter.Current.RemoveMenu(item, _keepHidden);
                    }
                }
            }

            _strips = null;

            base.Dispose(disposing);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public bool KeepHiddenAfterDispose
        {
            get { return _keepHidden; }
            set { _keepHidden = value; } 
        }

        [DisplayName("HideWhenInactive")]
        public bool GetHideWhenInactive(MenuStrip menu)
        {
            if (_strips != null)
            {
                return _strips.Contains(menu);
            }

            return false;
        }

        [DisplayName("HideWhenInactive")]
        public void SetHideWhenInactive(MenuStrip menu, bool value)
        {
            if (GetHideWhenInactive(menu) != value)
            {
                if (value)
                {
                    if (_strips == null)
                    {
                        _strips = new List<MenuStrip>();
                    }

                    _strips.Add(menu);

                    if (!DesignMode)
                    {
                        MenuStripMessageFilter.Current.AddMenu(menu);
                    }
                }
                else
                {
                    _strips.Remove(menu);

                    if (!DesignMode)
                    {
                        MenuStripMessageFilter.Current.RemoveMenu(menu);
                    }
                }
            }
        }

        public bool CanExtend(object extendee)
        {
            return extendee is MenuStrip;
        }

        private sealed class MenuStripMessageFilter : IMessageFilter
        {
            private sealed class MenuStripEntry
            {
                private MenuStrip _menu;
                private Form _form;

                public MenuStripEntry(MenuStrip menu)
                {
                    _menu = menu;
                }

                public Form Form
                {
                    get
                    {
                        if (_form == null)
                        {
                            _form = _menu.FindForm();

                            if (_form != null)
                            {
                                MenuStripMessageFilter.Current.AddForm(_form);
                            }
                        }

                        return _form;
                    }
                }
                
                public Boolean OpenedOnKeyDown
                {
                    get;
                    set;
                }

                public Boolean Visible
                {
                    get { return _menu.Visible; }
                }

                public void Show()
                {
                    _menu.Show();
                }

                public void Hide()
                {
                    _menu.Hide();
                }

                public void Toggle()
                {
                    _menu.Visible = !_menu.Visible;
                }
            }

            private Boolean _enabled;
            private Dictionary<MenuStrip, MenuStripEntry> _menus = new Dictionary<MenuStrip, MenuStripEntry>();
            private Dictionary<Form, Int32> _menuCount = new Dictionary<Form, Int32>();
            
            private MenuStripMessageFilter()
            {
            }

            public void AddMenu(MenuStrip menu)
            {
                if (!_menus.ContainsKey(menu))
                {
                    _menus[menu] = new MenuStripEntry(menu);

                    menu.Visible = false;
                    menu.MenuDeactivate += OnMenuDeactivate;
                    
                    if (!_enabled)
                    {
                        _enabled = true;
                        Application.AddMessageFilter(this);
                    }
                }
            }

            public void RemoveMenu(MenuStrip menu)
            {
                RemoveMenu(menu, false);
            }

            public void RemoveMenu(MenuStrip menu, bool keepHidden)
            {
                if (_menus.ContainsKey(menu))
                {
                    RemoveForm(_menus[menu].Form);

                    menu.MenuDeactivate -= OnMenuDeactivate;

                    if (!keepHidden)
                    {
                        menu.Visible = true;
                    }

                    _menus.Remove(menu);

                    if (_menus.Count == 0)
                    {
                        Application.RemoveMessageFilter(this);
                        _enabled = false;
                    }
                }
            }

            private void AddForm(Form f)
            {
                if (!_menuCount.ContainsKey(f))
                {
                    _menuCount[f] = 1;
                    f.Deactivate += OnFormDeactivate;
                }
                else
                {
                    _menuCount[f]++;
                }
            }

            private void RemoveForm(Form f)
            {
                if (_menuCount.ContainsKey(f))
                {
                    _menuCount[f]--;

                    if (_menuCount[f] == 0)
                    {
                        f.Deactivate -= OnFormDeactivate;
                        _menuCount.Remove(f);
                    }
                }
            }

            private void OnFormDeactivate(object sender, EventArgs e)
            {
                var f = (Form)(sender);

                foreach (var entry in _menus.Values)
                {
                    if (entry.Form == f)
                    {
                        entry.Hide();
                    }
                }
            }

            private void OnMenuDeactivate(object sender, EventArgs e)
            {
                ((MenuStrip)(sender)).Hide();
            }

            private MenuStripEntry GetEntryFromHWnd(IntPtr hWnd)
            {
                foreach (var item in _menus.Values)
                {
                    if (item.Form.IsHandleCreated)
                    {
                        if (item.Form.Handle == hWnd)
                        {
                            return item;
                        }

                        if (NativeMethods.GetAncestor(hWnd, AncestorType.Root) == item.Form.Handle)
                        {
                            return item;
                        }
                    }
                }

                return null;
            }

            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                var entry = GetEntryFromHWnd(m.HWnd);

                if (entry != null)
                {
                    const Int32 MenuKeys = (Int32)(Keys.Menu | Keys.LMenu | Keys.RMenu);
                    
                    if (m.Msg == WindowMessages.WM_SYSKEYUP)
                    {
                        if (m.WParam.ToInt32() == (Int32)Keys.F10)
                        {
                            entry.Show();
                        }
                        else if (((m.WParam.ToInt32() & MenuKeys) != 0) && !entry.OpenedOnKeyDown)
                        {
                            entry.Toggle();
                        }

                        entry.OpenedOnKeyDown = false;
                    }
                    else if (m.Msg == WindowMessages.WM_SYSKEYDOWN)
                    {
                        const int AltDownBit = 0x20000000;

                        if (((m.LParam.ToInt32() & AltDownBit) != 0) 
                            //&& ((m.LParam.ToInt32() & MenuKeys) == 0)
                            )
                        {
                            entry.OpenedOnKeyDown = true;
                            entry.Show();

                            return false;
                        }

                        if (entry.Visible && m.WParam.ToInt32() == (Int32)(Keys.F10))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public static MenuStripMessageFilter Current
            {
                get { return _current; }
            }

            [ThreadStatic]
            private static readonly MenuStripMessageFilter _current = new MenuStripMessageFilter();
        }
    }
}
