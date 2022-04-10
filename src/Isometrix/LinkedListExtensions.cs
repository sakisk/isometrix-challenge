using System;

namespace Isometrix;

public static class LinkedListExtensions
{
    public static void PrintList<T>(this LinkedList<T> list, Action<T> print)
    {
        foreach (var data in list.GetData()) 
            print(data);
    }
}