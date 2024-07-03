using System.Collections;

namespace CustomContainers
{
    public class CustomLinkedListNode<T>
    {
        public T Value { get; set; }
        public CustomLinkedListNode<T> Next { get; set; }
        public CustomLinkedListNode<T> Prev { get; set; }

        public CustomLinkedListNode(T value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private CustomLinkedListNode<T>? head;
        private CustomLinkedListNode<T>? tail;
        private int count;
        public CustomLinkedListNode<T>? First => head;
        public CustomLinkedListNode<T>? Last => tail;

        public CustomLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int Count => count;

        public void AddLast(T value)
        {
            CustomLinkedListNode<T> newNode = new CustomLinkedListNode<T>(value);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            count++;
        }

        public void AddFirst(T value)
        {
            CustomLinkedListNode<T> newNode = new CustomLinkedListNode<T>(value);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Prev = newNode;
                head = newNode;
            }
            count++;
        }

        public bool Remove(T value)
        {
            if (head == null)
            {
                return false;
            }
            if (head.Value.Equals(value))
            {
                RemoveFirst();
            }
            else if (tail.Value.Equals(value))
            {
                RemoveLast();
            }

            CustomLinkedListNode<T> currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    currentNode.Prev.Next = currentNode.Next;
                    currentNode.Next.Prev = currentNode.Prev.Prev;
                    currentNode.Next = null;
                    currentNode.Prev = null;
                    count--;
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public bool Remove(CustomLinkedListNode<T> value)
        {
            if (head == null)
            {
                return false;
            }
            if (head.Value.Equals(value.Value))
            {
                RemoveFirst();
            }
            else if (tail.Value.Equals(value.Value))
            {
                RemoveLast();
            }

            CustomLinkedListNode<T> currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value.Value))
                {
                    currentNode.Prev.Next = currentNode.Next;
                    currentNode.Next.Prev = currentNode.Prev.Prev;
                    currentNode.Next = null;
                    currentNode.Prev = null;
                    count--;
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public bool RemoveFirst()
        {
            if (head == null)
            {
                return false;
            }
            head = head.Next;
            if (head != null)
            {
                head.Prev = null;
            }
            else
            {
                tail = null;
            }
            count--;
            return true;
        }

        public bool RemoveLast()
        {
            if (tail == null)
            {
                return false;
            }
            tail = tail.Prev;
            if (tail != null)
            {
                tail.Next = null;
            }
            else
            {
                head = null;
            }
            count--;
            return true;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public CustomLinkedListNode<T>? Find(T value)
        {
            CustomLinkedListNode<T> currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return currentNode;
                }
                currentNode = currentNode.Next;
            }
            return null;
        }

        public CustomLinkedListNode<T>? FindLast(T value)
        {
            CustomLinkedListNode<T> currentNode = tail;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return currentNode;
                }
                currentNode = currentNode.Prev;
            }
            return null;
        }

        public bool Contains(T value)
        {
            CustomLinkedListNode<T> currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            CustomLinkedListNode<T> currentNode = head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}