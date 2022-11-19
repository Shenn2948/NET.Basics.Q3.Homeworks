using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using Tasks.DoNotChange;

namespace Tasks
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public Node<T> Next { get; set; }

        public Node<T> Previous { get; set; }
    }

    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public Node<T> Head { get; set; }

        public int Length { get; private set; }

        public void Add(T item)
        {
            var node = new Node<T>(item);
            Node<T> last = Head;
            Length++;

            if (Head == null)
            {
                Head = node;
                return;
            }

            while (last.Next != null)
            {
                last = last.Next;
            }

            last.Next = node;
            node.Previous = last;
        }

        public void AddAt(int index, T item)
        {
            if (index > Length || index < 0)
            {
                throw new IndexOutOfRangeException("index should not be less than 0, greater or equal than length");
            }

            if (index == Length)
            {
                Add(item);
                return;
            }

            Node<T> prevNode = GetNodeAt(index);

            var newNode = new Node<T>(item) { Next = prevNode.Next, Previous = prevNode.Previous };

            if (index == Length - 1)
            {
                Head.Next = newNode;
            }

            if (index == 0)
            {
                Head = newNode;
            }
        }

        public T ElementAt(int index)
        {
            return GetNodeAt(index).Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        public void Remove(T item)
        {
            Node<T> current = Head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    break;
                }

                current = current.Next;
            }

            RemoveByNode(current);
        }

        public T RemoveAt(int index)
        {
            var node = GetNodeAt(index);
            RemoveByNode(node);

            return node.Value;
        }

        private void RemoveByNode(Node<T> node)
        {
            if (node == null) return;

            var prev = node.Previous;
            var next = node.Next;

            if (prev != null) prev.Next = node.Next;
            if (next != null) next.Previous = node.Previous;

            Length--;
        }

        private Node<T> GetNodeAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException("index should not be less than 0, greater or equal than length");
            }

            Node<T> current = Head;
            int count = 0;

            while (count != Length - 1)
            {
                if (count == index)
                {
                    break;
                }

                count++;
                current = current.Next;
            }

            return current;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly DoublyLinkedList<T> _doublyLinkedList;
            private int _index;
            private T _currentElement;

            public Enumerator(DoublyLinkedList<T> doublyLinkedList)
            {
                _doublyLinkedList = doublyLinkedList;
                _index = -1;
                _currentElement = default;
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }

                    return _currentElement;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }

                    return _currentElement;
                }
            }

            public void Dispose()
            {
                _index = -2;
                _currentElement = default;
            }

            public bool MoveNext()
            {
                if (_index == -2)
                {
                    return false;
                }

                _index++;

                if (_index == _doublyLinkedList.Length)
                {
                    _index = -2;
                    _currentElement = default;
                    return false;
                }

                _currentElement = _doublyLinkedList.ElementAt(_index);

                return true;
            }

            void IEnumerator.Reset()
            {
                _index = -1;
                _currentElement = default;
            }
        }
    }
}