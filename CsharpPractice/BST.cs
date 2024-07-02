using System.Collections;

namespace CustomContainers
{
    public class CustomTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public CustomTreeNode<T>? Left { get; set; }
        public CustomTreeNode<T>? Right { get; set; }

        public CustomTreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private CustomTreeNode<T>? root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Add(T value)
        {
            root = AddRecursive(root, value);
        }

        private CustomTreeNode<T> AddRecursive(CustomTreeNode<T>? node, T value)
        {
            if (node == null)
            {
                return new CustomTreeNode<T>(value);
            }

            int comparison = value.CompareTo(node.Value);
            if (comparison < 0)
            {
                node.Left = AddRecursive(node.Left, value);
            }
            else if (comparison > 0)
            {
                node.Right = AddRecursive(node.Right, value);
            }

            return node;
        }

        public bool Remove(T value)
        {
            bool removed;
            root = RemoveRecursive(root, value, out removed);
            return removed;
        }

        private CustomTreeNode<T>? RemoveRecursive(CustomTreeNode<T>? node, T value, out bool removed)
        {
            if (node == null)
            {
                removed = false;
                return null;
            }
            int comparison = value.CompareTo(node.Value);
            if (comparison < 0)
            {
                node.Left = RemoveRecursive(node.Left, value, out removed);
            }
            else if (comparison > 0)
            {
                node.Right = RemoveRecursive(node.Right, value, out removed);
            }
            else
            {
                removed = true;
                if (node.Left == null)
                {
                    return node.Right;
                }
                if (node.Right == null)
                {
                    return node.Left;
                }

                CustomTreeNode<T> successor = FindMin(node.Right);
                node.Value = successor.Value;
                node.Right= RemoveRecursive(node.Right, successor.Value, out _);
            }
            return node;
        }

        private CustomTreeNode<T> FindMin(CustomTreeNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public bool Contains(T value)
        {
            return ContainsRecursive(root, value);
        }

        private bool ContainsRecursive(CustomTreeNode<T>? node, T value)
        {
            if (node == null)
            {
                return false;
            }

            int comparison = value.CompareTo(node.Value);

            if (comparison < 0)
            {
                return ContainsRecursive(node.Left, value);
            }
            else if (comparison > 0)
            {
                return ContainsRecursive(node.Right, value);
            }
            else
            {
                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal(root).GetEnumerator();
        }

        private IEnumerable<T> InOrderTraversal(CustomTreeNode<T>? node)
        {
            if (node != null)
            {
                foreach (var item in InOrderTraversal(node.Left))
                {
                    yield return item;
                }

                yield return node.Value;

                foreach (var item in InOrderTraversal(node.Right))
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
