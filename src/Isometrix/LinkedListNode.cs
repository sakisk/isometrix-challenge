namespace Isometrix;

public class LinkedListNode<T>
{
    public T Data { get; }
    public LinkedListNode<T>? Next { get; set; }

    public LinkedListNode(T data) => Data = data;
}