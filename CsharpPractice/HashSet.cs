using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomContainers
{
    public class CustomHashSet<T> : IEnumerable<T>
    {
        private LinkedList<T>[] _buckets;
        private int _size;
        private const int DefaultCapacity = 4;

        public CustomHashSet()
        {
            _buckets = new LinkedList<T>[DefaultCapacity];
        }
        public int Count => _size;
        
        private int GetBucketIndex(T item)
        {
            int hash = item.GetHashCode();
            int index = hash % _buckets.Length;
            return Math.Abs(index);
        }

        public bool Add(T item)
        {
            if (Contains(item))
            {
                return false;
            }
            if (_size >= _buckets.Length * 0.75)
            {
                Resize();
            }

            int bucketIndex = GetBucketIndex(item);
            if (_buckets[bucketIndex] == null)
            {
                _buckets[bucketIndex] = new LinkedList<T>();
            }
            _buckets[bucketIndex].AddLast(item);
            _size++;
            return true;
        }
        
        public bool Remove(T item)
        {
            int bucketIndex = GetBucketIndex(item);
            LinkedList<T> bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                LinkedListNode<T> node = bucket.Find(item);
                if (node != null)
                {
                    bucket.Remove(node);
                    _size--;
                    return true;
                }
            }
            return false;
        }

        public bool Contains(T item)
        {
            int bucketIndex = GetBucketIndex(item);
            LinkedList<T> bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                foreach (T element in bucket)
                {
                    if (element.Equals(item))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Resize()
        {
            int newCapacity = _buckets.Length * 2;
            LinkedList<T>[] newBuckets = new LinkedList<T>[newCapacity];

            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        int newBucketIndex = item.GetHashCode() % newCapacity;
                        if (newBuckets[newBucketIndex] == null)
                        {
                            newBuckets[newBucketIndex] = new LinkedList<T>();
                        }
                        newBuckets[newBucketIndex].AddLast(item);
                    }
                }
            }
            _buckets = newBuckets;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var item in bucket)
                    {
                        yield return item;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
