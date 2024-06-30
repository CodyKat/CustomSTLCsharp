using System.Collections;

namespace CustomContainers
{
    class CustomVector<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _size;
        private const int DefaultCapacity = 4;

        public CustomVector()
        {
            _items = new T[DefaultCapacity];
        }

        public int Count => _size;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }
            _items[_size++] = item;
        }

        public bool Remove(T item)
        {
            int index = Array.IndexOf(_items, item, 0, _size);
            if (index >= 0)
            {
                _size--;
                if (index < _size)
                {
                    Array.Copy(_items, index + 1, _items, index, _size - index);
                }
                _items[_size] = default(T);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Array.Clear(_items, 0, _size);
            _size = 0;
        }

        public void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
                if (newCapacity < min) newCapacity = min;
                T[] newItems = new T[newCapacity];
                Array.Copy(_items, newItems, _size);
                _items = newItems;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
