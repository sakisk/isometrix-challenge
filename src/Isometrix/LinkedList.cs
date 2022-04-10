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
        var currentLast = GetLast(current);
        
        currentLast.Next = last;
    }

    private static LinkedListNode<T> GetLast(LinkedListNode<T> start)
    {
        var current = start;
        while (current is {Next: {} next}) 
            current = next;

        return current;
    }
}