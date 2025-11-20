namespace CollapseAndRefill;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Cascading Slot Machine Tests ---");

        #region Test 1: Major Cascade (Matches -> Removal -> Gravity -> Refill)

        List<string> input1 =
        [
            "7 7 7",
            "7 7 7",
            "7 7 99",
            "10 20 30",
            "11 21 31",
            "12 22 32"
        ];

        byte[][] expected1 =
        [
            [12, 22, 31],
            [11, 21, 30],
            [10, 20, 99]
        ];

        RunTestCase("Test 1 (Cascade & Gravity)", 3, 3, input1, expected1);

        #endregion

        #region Test 2: No Matches (Identity)

        List<string> input2 =
        [
            "1 2 3",
            "4 5 6",
            "7 8 9"
        ];

        byte[][] expected2 =
        [
            [1, 2, 3],
            [4, 5, 6],
            [7, 8, 9]
        ];

        RunTestCase("Test 2 (No Matches)", 3, 3, input2, expected2);

        #endregion
    }

    private static void RunTestCase(string testName, int n, int m, List<string> input, byte[][] expected)
    {
        Console.WriteLine($"\nRunning {testName}...");
        try
        {
            List<string> inputCopy = [.. input];
            byte[][] actual = CascadingSlotMachine.Spin(n, m, inputCopy);

            if (AreGridsEqual(expected, actual))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("PASS");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAIL");
                Console.ResetColor();

                Console.WriteLine("\nExpected Grid:");
                PrintGrid(expected);

                Console.WriteLine("\nActual Grid:");
                PrintGrid(actual);
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"CRASH: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();
        }
    }

    private static bool AreGridsEqual(byte[][] expected, byte[][] actual)
    {
        if (actual is null || actual.Length != expected.Length)
        {
            return false;
        }

        for (int i = 0; i < expected.Length; i++)
        {
            if (actual[i] is null || actual[i].Length != expected[i].Length)
            {
                return false;
            }

            for (int j = 0; j < expected[i].Length; j++)
            {
                if (expected[i][j] != actual[i][j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void PrintGrid(byte[][] grid)
    {
        if (grid is null)
        {
            Console.WriteLine("null");
            return;
        }

        foreach (byte[] row in grid)
        {
            if (row is null)
            {
                Console.WriteLine("[ null ]");
            }
            else
            {
                Console.WriteLine($"[ {string.Join(", ", row),-10} ]");
            }
        }
    }
}