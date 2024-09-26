using System;
using System.Collections.Generic;
using System.Threading;

namespace BoundedBuffer
{
    public class BoundedBuffer<T>
    {
        private readonly Queue<T> _buffer;
        private readonly SemaphoreSlim _empty;
        private readonly SemaphoreSlim _full;
        private readonly object _lockObj = new object();
        private readonly int _capacity;

        public BoundedBuffer(int capacity)
        {
            _capacity = capacity;
            _buffer = new Queue<T>(capacity);
            _empty = new SemaphoreSlim(0, capacity);
            _full = new SemaphoreSlim(capacity, capacity);
        }

        public void Insert(T item)
        {
            _full.Wait();
            lock (_lockObj)
            {
                _buffer.Enqueue(item);
            }
            _empty.Release();
        }

        public T Take()
        {
            _empty.Wait();
            T item;
            lock (_lockObj)
            {
                item = _buffer.Dequeue();
            }
            _full.Release();
            return item;
        }
    }
}

