using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomContainers
{
    public class CustomQueue<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();
        public int Count => _list.Count;

        public void Enqueue(T item)
        {
            _list.AddLast(item);
        }
        
        public T Dequeue()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            T value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        public T peek()
        {
            if (_list.Count == 0)
            {
                throw new InvalidOperationException("Queue is Empty");
            }
            return _list.First.Value;
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void Clear()
        {
            _list.Clear();
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
