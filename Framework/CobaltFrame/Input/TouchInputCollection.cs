using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltFrame.Input
{
    public class TouchInputCollection:ICollection<TouchLocation>
    {
        public List<TouchLocation> Items { get; private set; }

        public TouchInputCollection()
        {
            Items = new List<TouchLocation>();
        }

        public TouchInputCollection(TouchInputCollection collection)
        {
            Items = new List<TouchLocation>();
            foreach (var location in collection)
            {
                Items.Add(location);
            }
        }

        public bool IsTouch
        {
            get
            {
                if (Items.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Add(TouchLocation item)
        {
            if (!Items.Contains(item))
                Items.Add(item);
        }

        public void Clear()
        {

            Items.Clear();
        }

        public bool Contains(TouchLocation item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(TouchLocation[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TouchLocation item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<TouchLocation> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
