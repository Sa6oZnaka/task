
namespace DoublyLinkedListTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = new DoubleLinkedList<int>(new List<int> { 1, 2, 3 });
            
            PrintList(list);

            list.AddFirst(0);
            PrintList(list);
            
            list.AddLast(4);
            PrintList(list);

            list.RemoveFirst();
            PrintList(list);

            list.RemoveLast();
            PrintList(list);

            Console.WriteLine($"\nCount: {list.Count}");
            Console.WriteLine($"First: {list.First?.Value}");
            Console.WriteLine($"Last: {list.Last?.Value}");

            Console.WriteLine("\n");
            Console.ReadKey();
        }

        static void PrintList(DoubleLinkedList<int> list)
        {
            foreach (var item in list.GetData())
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}


public class Node<T>
{
    public T Value;
    public Node<T> Next;
    public Node<T> Previous;

    public Node(T value)
    {
        Value = value;
    }
}

