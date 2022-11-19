using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private readonly DoublyLinkedList<T> _list = new DoublyLinkedList<T>();

        private bool IsEmpty => _list.Length == 0;

        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("nothing to Dequeue");
            }

            return _list.RemoveAt(0);
        }

        public void Enqueue(T item)
        {
            _list.Add(item);
        }

        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("nothing to Pop");
            }

            return _list.RemoveAt(_list.Length - 1);
        }

        public void Push(T item)
        {
            _list.AddAt(0, item);
        }
    }
}