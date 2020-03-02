using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace BaggageSys
{
    // The buffer class is essentially a blocking collection, except it is minimal and only works with queue
    class Buffer<T>
    {
        private Queue<T> items { get; }
        public int MaxItems { get; }
        private EventWaitHandle isSpace { get; } = new EventWaitHandle(false, EventResetMode.AutoReset);
        private EventWaitHandle containsItem { get; } = new EventWaitHandle(false, EventResetMode.AutoReset);
        public Buffer(int maxItems)
        {
            items = new Queue<T>();
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
        // Blocks the thread until there is space for another item
        public void TryPutItem(T item)
        {
            Monitor.Enter(this);
            while (IsFull())
            {
                Monitor.Exit(this);
                containsItem.Set();
                isSpace.WaitOne();
                Monitor.Enter(this);
            }
            containsItem.Set();
            items.Enqueue(item);
            Monitor.Exit(this);
        }
        // Blocks the thread until there is an item in the buffer
        public T TryGetItem()
        {
            Monitor.Enter(this);
            while (IsEmpty())
            {
                Monitor.Exit(this);
                isSpace.Set();
                containsItem.WaitOne();
                Monitor.Enter(this);
            }
            T item = items.Dequeue();
            isSpace.Set();
            Monitor.Exit(this);
            return item;
        }
    }
}
