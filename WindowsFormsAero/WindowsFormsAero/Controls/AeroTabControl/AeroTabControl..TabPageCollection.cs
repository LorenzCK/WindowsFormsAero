using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WindowsFormsAero
{
    partial class AeroTabControl
    {
        public sealed class TabPageCollection : IList<AeroTabPage>, IList
        {
            private readonly AeroTabControl _owner;

            internal TabPageCollection(AeroTabControl owner)
            {
                _owner = owner;
            }

            #region IList<AeroTabPage> Members

            public int IndexOf(AeroTabPage item)
            {
                return _owner._pages.IndexOf(item);
            }

            public void Insert(int index, AeroTabPage item)
            {
                _owner.Controls.Add(item);
                _owner.Controls.SetChildIndex(item, index);
            }

            public void RemoveAt(int index)
            {
                _owner.Controls.RemoveAt(index);
            }

            public AeroTabPage this[int index]
            {
                get { return _owner._pages[index]; }
                set
                {
                    throw new NotSupportedException();
                }
            }

            #endregion

            #region ICollection<AeroTabPage> Members

            public void Add(AeroTabPage item)
            {
                _owner.Controls.Add(item);
            }

            public void Clear()
            {
                _owner.Controls.Clear();
            }

            public bool Contains(AeroTabPage item)
            {
                return _owner._pages.Contains(item);
            }

            public void CopyTo(AeroTabPage[] array, int arrayIndex)
            {
                _owner._pages.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return _owner._pages.Count; }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }

            public bool Remove(AeroTabPage item)
            {
                bool result = _owner.Controls.Contains(item);
                _owner.Controls.Remove(item);

                return result;
            }

            #endregion

            #region IEnumerable<AeroTabPage> Members

            public IEnumerator<AeroTabPage> GetEnumerator()
            {
                return _owner._pages.GetEnumerator();
            }

            #endregion

            #region IList Members

            int IList.Add(object value)
            {
                Add((AeroTabPage)(value));
                return Count - 1;
            }

            bool IList.Contains(object value)
            {
                return Contains((AeroTabPage)(value));
            }

            int IList.IndexOf(object value)
            {
                return IndexOf((AeroTabPage)(value));
            }

            void IList.Insert(int index, object value)
            {
                Insert(index, (AeroTabPage)(value));
            }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            void IList.Remove(object value)
            {
                Remove((AeroTabPage)(value));
            }

            void IList.RemoveAt(int index)
            {
                RemoveAt(index);
            }

            object IList.this[int index]
            {
                get { return this[index]; }
                set { this[index] = (AeroTabPage)(value); }
            }

            #endregion

            #region ICollection Members

            void ICollection.CopyTo(Array array, int index)
            {
                CopyTo((AeroTabPage[])(array), index);
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return this; }
            }

            #endregion
            
            #region IEnumerable Members

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }
    }
}
