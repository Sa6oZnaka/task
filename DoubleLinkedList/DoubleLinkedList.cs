namespace DoublyLinkedListTest;

public class DoubleLinkedList<T>
{
    private Node<T> _head;
    private Node<T> _tail;
    private int _count;

    public Node<T> First => _head;
    public Node<T> Last => _tail;
    public int Count => _count;

    public DoubleLinkedList(T item)
    {
        var node = new Node<T>(item);
        _head = _tail = node;
        _count = 1;
    }

    public DoubleLinkedList(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            AddLast(item);
        }
    }

    public Node<T> AddFirst(T data)
    {
        var newNode = new Node<T>(data);

        if (_count == 0)
        {
            _head = _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }

        _count++;
        return newNode;
    }

    public Node<T> AddLast(T data)
    {
        var newNode = new Node<T>(data);

        if (_count == 0)
        {
            _head = _tail = newNode;
        }
        else
        {
            newNode.Previous = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }

        _count++;
        return newNode;
    }

    public void RemoveFirst()
    {
        if (_count == 0)
            throw new InvalidOperationException("List is empty");

        if (_count == 1)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _head = _head.Next;
            _head.Previous = null;
        }

        _count--;
    }

    public void RemoveLast()
    {
        if (_count == 0)
            throw new InvalidOperationException("List is empty");

        if (_count == 1)
        {
            _head = null;
            _tail = null;
        }
        else
        {
            _tail = _tail.Previous;
            _tail.Next = null;
        }

        _count--;
    }

    public IEnumerable<T> GetData()
    {
        var current = _head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }
}