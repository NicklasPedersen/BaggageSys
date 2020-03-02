using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace BaggageSys
{
    class Buffer<T>
    {
        private Queue<T> items { get; }
        public int MaxItems { get; }
        private EventWaitHandle isSpace { get; } = new EventWaitHandle(false, EventResetMode.AutoReset);
        private EventWaitHandle containsItem { get; } = new EventWaitHandle(false, EventResetMode.AutoReset);
        private object _lock = new object();
        public Buffer(int maxItems)
        {
            items = new Queue<T>(maxItems);
            MaxItems = maxItems;
        }
        public bool IsFull()
        {
            return items.Count >= MaxItems;
        }
        public bool IsEmpty()
        {
            return items.Count <= 0;
        }
        public void TryPutItem(T item)
        {
            Monitor.Enter(_lock);
            while (IsFull())
            {
                Monitor.Exit(_lock);
                containsItem.Set();
                isSpace.WaitOne();
                Monitor.Enter(_lock);
            }
            containsItem.Set();
            items.Enqueue(item);
            Monitor.Exit(_lock);
        }
        public T TryGetItem()
        {
            Monitor.Enter(_lock);
            while (IsEmpty())
            {
                Monitor.Exit(_lock);
                isSpace.Set();
                containsItem.WaitOne();
                Monitor.Enter(_lock);
            }
            T item = items.Dequeue();
            isSpace.Set();
            Monitor.Exit(_lock);
            return item;
        }
    }
}
