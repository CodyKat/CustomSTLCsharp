using System.Collections;

namespace CustomContainers
{
    public class CustomLinkedListNode<T>
    {
        public T Value { get; set; }
        public CustomLinkedListNode<T> Next { get; set; }

        public CustomLinkedListNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private CustomLinkedListNode<T> head;
        private CustomLinkedListNode<T> tail;
        private int count;
        public CustomLinkedListNode<T> First => head;

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
                head = head.Next;
                count--;
                if (head == null)
                {
                    tail = null;
                }
                return true;
            }

            CustomLinkedListNode<T> currentNode = head;
            while (currentNode.Next != null)
            {
                if (currentNode.Next.Value.Equals(value))
                {
                    currentNode.Next = currentNode.Next.Next;
                    count--;
                    if (currentNode.Next == null)
                    {
                        tail = currentNode;
                    }
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
                head = head.Next;
                count--;
                if (head == null)
                {
                    tail = null;
                }
                return true;
            }

            CustomLinkedListNode<T> currentNode = head;
            while (currentNode.Next != null)
            {
                if (currentNode.Next.Value.Equals(value.Value))
                {
                    currentNode.Next = currentNode.Next.Next;
                    count--;
                    if (currentNode.Next == null)
                    {
                        tail = currentNode;
                    }
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
            if (head == null)
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
            CustomLinkedListNode<T> currentNode = head;
            if (currentNode.Next == null)
            {
                RemoveFirst();
                return true;
            }
            while (currentNode.Next.Next != null)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = null;
            tail = currentNode;
            count--;
            return true;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public CustomLinkedListNode<T> Find(T value)
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