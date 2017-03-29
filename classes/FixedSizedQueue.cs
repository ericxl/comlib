using System.Collections.Generic;

public class FixedSizedQueue<T> : Queue<T>
{
    public FixedSizedQueue(int size)
    {
        Size = size;
    }
    public int Size { get; private set; }
    public new void Enqueue(T obj)
    {
        base.Enqueue(obj);
        lock (this)
        {
            while (base.Count > Size)
            {
                base.Dequeue();
            } ;
        }
    }
}
