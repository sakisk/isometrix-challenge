using System;

namespace Isometrix;

public class LinkedList<T>
{
    public LinkedListNode<T>? First { get; private set; }

    public void AddFirst(T element)
    {
        var newFirstNode = new LinkedListNode<T>(element)
        {
            Next = First
        };

        First = newFirstNode;
    }
}