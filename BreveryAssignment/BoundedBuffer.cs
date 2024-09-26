using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Concurrent;
using System.Threading;

public class BoundedBuffer<T>
{
    private readonly SemaphoreSlim _empty;
    private readonly SemaphoreSlim _full;
    private readonly ConcurrentQueue<T> _queue;

    public BoundedBuffer(int capacity)
    {
        _empty = new SemaphoreSlim(capacity);
        _full = new SemaphoreSlim(0);
        _queue = new ConcurrentQueue<T>();
    }

    public void Add(T item)
    {
        _empty.Wait();
        _queue.Enqueue(item);
        _full.Release();
    }

    public T Remove()
    {
        _full.Wait();
        _queue.TryDequeue(out T item);
        _empty.Release();
        return item;
    }
}

