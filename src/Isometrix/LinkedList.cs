namespace Isometrix;

public class LinkedList<T>
{
    public LinkedListNode<T>? First { get; private set; }

    public void AddFirst(T first)
    {
        First = new LinkedListNode<T>(first);
    }
}