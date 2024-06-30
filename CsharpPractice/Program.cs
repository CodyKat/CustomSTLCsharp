using System.Collections;

namespace CustomContainers
{
    public class CustomList<T> : IEnumerable<T>
    {
        private T[] _itmes;
        private int _size;
        private const int DefaultCapacity = 4;

        public CustomList()
        {
            _itmes = new T[DefaultCapacity];
        }

        public int Count => _size;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _itmes[index];
            }
            set
            {
                if (index < 0 || index >= _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _itmes[index] = value;
            }
        }

        public void Add(T item)
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}