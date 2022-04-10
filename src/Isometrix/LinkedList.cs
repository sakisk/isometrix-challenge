namespace Isometrix;

public class LinkedList<T>
{
    public LinkedListNode<T>? First { get; private set; }

    public void AddFirst(T newNode)
    {
        var newFirstNode = new LinkedListNode<T>(newNode)
        {
            Next = First
        };

        First = newFirstNode;
    }
}