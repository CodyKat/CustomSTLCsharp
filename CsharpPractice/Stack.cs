using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomContainers
{
    class CustomStack<T> : IEnumerable<T>
    {
        private CustomList<T> _list = new CustomList<T>();

        public int Count => _list.Count;

        public void Push(T item)
        {
            _list.Add(item);
        }

        public T Pop()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            T item = _list[_list.Count - 1];
            _list.Remove(item);
            return item;
        }

        public T Peek()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty");
            }
            return _list[_list.Count - 1];
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
