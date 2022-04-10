using System;

namespace Isometrix;

public class LinkedList<T>
{
    public LinkedListNode<T>? First { get; private set; }
    public LinkedListNode<T>? Last => First is { } ? GetLast(First) : null;

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
        while (current is {Next: { } next})
            current = next;

        return current;
    }

    public void AddAfter(T existing, T insertedElement)
    {
        var current = First;
        var inserted = new LinkedListNode<T>(insertedElement);
        
        if (Last is {Data: not null} && Last.Data.Equals(existing))
        {
            Last.Next = inserted;
            return;
        }
        
        while (current is {Next: { } next})
        {
            if (current.Data is not null && current.Data.Equals(existing))
            {
                var oldNext = current.Next;
                current.Next = inserted;
                inserted.Next = oldNext;
                return;
            }
            current = next;
        }

        throw new InvalidOperationException($"Element {existing} not found in any node");
    }
}