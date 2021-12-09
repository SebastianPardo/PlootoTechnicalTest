using System.Collections;
using System.Collections.Generic;

namespace Services.Data
{
    public class MyTree<T> : IEnumerable<MyTree<T>>
    {
        private readonly Dictionary<string, MyTree<T>> children = new Dictionary<string, MyTree<T>>();

        public readonly string id;
        private T value;
        public MyTree<T> Parent { get; private set; }

        public MyTree(string id, T value)
        {
            this.id = id;
            this.value = value;
        }

        public T GetValue()
        {
            return value;
        }

        public MyTree<T> GetChild(string id)
        {
            return children[id];
        }

        public Dictionary<string, MyTree<T>> GetChildren()
        {
            return children;
        }

        public MyTree<T> Add(MyTree<T> item)
        {
            if (item.Parent != null)
            {
                item.Parent.children.Remove(item.id);
            }

            item.Parent = this;
            children.Add(item.id, item);
            return item;
        }

        public IEnumerator<MyTree<T>> GetEnumerator()
        {
            return children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return children.Count; }
        }

        public string Path()
        {
            string path = value.ToString();
            var child = this;
            var myParent = Parent;
            while(myParent != null)
            {
                path = myParent.value + "." + path;
                child = myParent;
                myParent = child.Parent;
            }
            return path;
        }
    }
}
