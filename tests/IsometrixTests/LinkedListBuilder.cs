using Isometrix;

namespace IsometrixTests;

public class LinkedListBuilder<T>
{
    private T[]? _itemsFromStart;
    private T[]? _itemsFromLast;

    public LinkedListBuilder<T> WithElementsFromStart(params T[] items)
    {
        _itemsFromStart = items;

        return this;
    }

    public LinkedListBuilder<T> WithElementsFromLast(params T[] items)
    {
        _itemsFromLast = items;

        return this;
    }
    
    public LinkedList<T> Build()
    {
        var linkedList = new LinkedList<T>();

        if (_itemsFromStart is {Length: > 0})
        {
            foreach (var item in _itemsFromStart) 
                linkedList.AddFirst(item);
        }

        if (_itemsFromLast is {Length: > 0})
        {
            foreach (var item in _itemsFromLast) 
                linkedList.AddLast(item);
        }

        return linkedList;
    }
}