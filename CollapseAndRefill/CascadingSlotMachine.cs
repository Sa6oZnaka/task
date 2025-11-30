
namespace CollapseAndRefill
{
    public static class CascadingSlotMachine
    {
        public static byte[][] Spin(int N, int M, List<string> symbols)
        {
            var grid = InitGrid(N, M, symbols);
            var supply = InitSupply(M, symbols, N);
            
            // cascades
            for (int i = 0; i < 10; i ++)
            {
                var matches = FindMatches(grid);

                if (matches.Count == 0)
                    break;

                RemoveMatches(grid, matches);
                ApplyGravity(grid);

                if (!Refill(grid, supply))
                    break;
            }

            return grid;
        }
        
        static byte[][] InitGrid(int N, int M, List<string> symbols)
        {
            var grid = new byte[N][];

            for (int i = 0; i < N; i++)
                grid[i] = new byte[M];

            for (int i = 0; i < N; i++)
            {
                var parts = symbols[i].Split(' ');
                for (int j = 0; j < M; j++)
                {
                    grid[i][j] = byte.Parse(parts[j]);
                }
            }

            return grid;
        }

        static Queue<byte>[] InitSupply(int M, List<string> symbols, int startIndex)
        {
            var supply = new Queue<byte>[M];

            for (int i = 0; i < M; i++)
                supply[i] = new Queue<byte>();

            for (int i = startIndex; i < symbols.Count; i++)
            {
                var parts = symbols[i].Split(' ');
                for (int j = 0; j < M; j++)
                {
                    supply[j].Enqueue(byte.Parse(parts[j]));
                }
            }

            return supply;
        }

        static HashSet<byte> FindMatches(byte[][] grid)
        {
            var counts = new Dictionary<byte, int>();

            foreach (var row in grid)
            {
                foreach (var value in row)
                {
                    if (value == 255) continue;

                    if (!counts.ContainsKey(value))
                        counts[value] = 0;

                    counts[value]++;
                }
            }

            return counts
                .Where(kv => kv.Value >= 8)
                .Select(kv => kv.Key)
                .ToHashSet();
        }

        static void RemoveMatches(byte[][] grid, HashSet<byte> matches)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (matches.Contains(grid[i][j]))
                        grid[i][j] = 255; // empty
                }
            }
        }

        static void ApplyGravity(byte[][] grid)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;

            for (int i = 0; i < cols; i++)
            {
                var columnValues = new List<byte>();

                for (int j = rows - 1; j >= 0; j--)
                {
                    if (grid[j][i] != 255)
                        columnValues.Add(grid[j][i]);
                }

                int index = rows - 1;

                foreach (var val in columnValues)
                {
                    grid[index--][i] = val;
                }

                for (var row = index; row >= 0; row--)
                {
                    grid[row][i] = 255;
                }
            }
        }

        static bool Refill(byte[][] grid, Queue<byte>[] supply)
        {
            var rows = grid.Length;
            var cols = grid[0].Length;

            for (int i = 0; i < cols; i++)
            {
                for (int j = rows - 1; j >= 0; j--)
                {
                    if (grid[j][i] == 255)
                    {
                        if (supply[i].Count == 0)
                            return false;

                        grid[j][i] = supply[i].Dequeue();
                    }
                }
            }

            return true;
        }
    }
}
