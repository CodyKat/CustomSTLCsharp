using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomContainers
{
    public class CustomNode<T>
    {
        public T Value { get; set; }
        public CustomNode<T> Next { get; set; }

        public CustomNode(T value)
        {
            Value = value;
            Next = null;
        }
    }

    public class CustomLinkedList<T> : IEnumerable<T>
    {
        private CustomNode<T> head;
        private CustomNode<T> tail;
        private int count;

        public CustomLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public int Count => count;

        public void AddLast(T value)
        {
            CustomNode<T> newNode = new CustomNode<T>(value);

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
            CustomNode<T> newNode = new CustomNode<T>(value);
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

            CustomNode<T> currentNode = head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
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

        public bool Contains(T value)
        {
            CustomNode<T> currentNode = head;
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
            CustomNode<T> currentNode = head;
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