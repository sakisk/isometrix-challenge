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
    
    public void AddLast(T element)
    {
        var last = new LinkedListNode<T>(element);
        
        if (First is null)
        {
            First = last;
            return;
        }

        var current = First;
        while (current is {Next: {} next}) 
            current = next;
        
        current.Next = last;
    }
}