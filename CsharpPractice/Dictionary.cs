using System.Collections;

namespace CustomContainers
{
    public class CustomDictionary<Tkey, Tvalue> : IEnumerable<KeyValuePair<Tkey, Tvalue>>
    {
        private const int DefaultCapacity = 4;
        private CustomLinkedList<KeyValuePair<Tkey, Tvalue>>[] _buckets;
        private int _size;

        public CustomDictionary()
        {
            _buckets = new CustomLinkedList<KeyValuePair<Tkey, Tvalue>>[DefaultCapacity];
        }

        public int Count => _size;

        private int GetBucketIndex(Tkey key)
        {
            int index = key.GetHashCode() % _buckets.Length;
            return Math.Abs(index);
        }

        public void Add(Tkey key, Tvalue value)
        {
            if (ContainsKey(key))
            {
                throw new ArgumentException("same key already exists");
            }
            if (_size >= _buckets.Length * 0.75)
            {
                Resize();
            }

            int bucketIndex = GetBucketIndex(key);
            if (_buckets[bucketIndex] == null)
            {
                _buckets[bucketIndex] = new CustomLinkedList<KeyValuePair<Tkey, Tvalue>>();
            }
            _buckets[bucketIndex].AddLast(new KeyValuePair<Tkey, Tvalue>(key, value));
            _size++;
        }

        public bool Remove(Tkey key)
        {
            int bucketIndex = GetBucketIndex(key);
            CustomLinkedList<KeyValuePair<Tkey, Tvalue>> bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                var node = bucket.First;
                while (node != null)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        bucket.Remove(node);
                        _size--;
                        return true;
                    }
                    node = node.Next;
                }
            }
            return false;
        }

        public bool ContainsKey(Tkey key)
        {
            int bucketIndex = GetBucketIndex(key);
            CustomLinkedList<KeyValuePair<Tkey, Tvalue>> bucket = _buckets[bucketIndex];
            foreach (var node in bucket)
            {
                if (node.Key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool TryGetValue(Tkey key, out Tvalue value)
        {
            int bucketIndex = GetBucketIndex(key);
            CustomLinkedList<KeyValuePair<Tkey, Tvalue>> bucket = _buckets[bucketIndex];
            if (bucket != null)
            {
                foreach (var node in bucket)
                {
                    if (node.Key.Equals(key))
                    {
                        value = node.Value;
                        return true;
                    }
                }
            }
            value = default(Tvalue);
            return false;
        }

        private void Resize()
        {
            int newCapacity = _buckets.Length * 2;
            CustomLinkedList<KeyValuePair<Tkey, Tvalue>>[] newBuckets 
                = new CustomLinkedList<KeyValuePair<Tkey, Tvalue>>[newCapacity];

            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var node in bucket)
                    {
                        int newBucketIndex = node.Key.GetHashCode() % newCapacity;
                        newBucketIndex = Math.Abs(newBucketIndex);
                        if (newBuckets[newBucketIndex] == null)
                        {
                            newBuckets[newBucketIndex] = new CustomLinkedList<KeyValuePair<Tkey, Tvalue>>();
                        }
                        newBuckets[newBucketIndex].AddLast(node);
                    }
                }
            }
            _buckets = newBuckets;
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var node in bucket)
                    {
                        yield return node;
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
