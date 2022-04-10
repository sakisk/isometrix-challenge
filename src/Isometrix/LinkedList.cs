using System;

namespace Isometrix;

public class LinkedList<T>
{
    public LinkedListNode<T>? First { get; private set; }
    public LinkedListNode<T>? Last => First is { } ? GetLast(First) : null;

    public void AddFirst(T element)
    {
        var oldFirst = First;
        var newFirstNode = new LinkedListNode<T>(element)
        {
            Next = oldFirst
        };

       if (oldFirst is { })
           oldFirst.Previous = newFirstNode;
       
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
        last.Previous = currentLast;
    }

    public void AddAfter(T existing, T insertedElement)
    {
        var inserted = new LinkedListNode<T>(insertedElement);
        var node = FindNode(existing);
        var oldNext = node.Next;

        node.Next = inserted;
        inserted.Next = oldNext;
        inserted.Previous = node;
    }

    public void AddBefore(T existing, T insertedElement)
    {
        var inserted = new LinkedListNode<T>(insertedElement);
        var node = FindNode(existing);
        var oldPrevious = node.Previous;

        if (oldPrevious is null)
        {
            AddFirst(insertedElement);
            return;
        }

        node.Previous = inserted;
        inserted.Next = node;
        inserted.Previous = oldPrevious;
        oldPrevious.Next = inserted;
    }

    private static LinkedListNode<T> GetLast(LinkedListNode<T> start)
    {
        var current = start;
        while (current is {Next: { } next})
            current = next;

        return current;
    }

    private LinkedListNode<T> FindNode(T existing)
    {
        var current = First;
        while (current is {Next: { } next})
        {
            if (current.Data is not null && current.Data.Equals(existing))
                return current;

            current = next;
        }

        if (current is {Data: { } data} && data.Equals(existing))
            return current;

        throw new InvalidOperationException($"Element {existing} not found in any node");
    }

    public void RemoveFirst()
    {
        if (First is {Next: { } next})
        {
            First = next;
            next.Previous = null;
            return;
        }

        First = null;
    }

    public void RemoveLast()
    {
        if (First is {Next: { } next })
        {
            var last = GetLast(next);
            last.Previous!.Next = null;
            return;
        }
        
        First = null;
    }

    public void RemoveElement(T element)
    {
        var node = FindNode(element);

        if (node.Previous is null)
        {
            RemoveFirst();
            return;
        }

        if (node.Next is null)
        {
            RemoveLast();
            return;
        }
        
    }
}