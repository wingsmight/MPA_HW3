using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MPA_HW3
{
    public class Program
    {
        //static void FindFullWayByBruteForceRec(int[,] graph, bool[] visited, int currPos, int n, int count, int cost, ref int ans)
        //{
        //    if (count == n && graph[currPos, 0] != 0)
        //    {
        //        ans = Math.Min(ans, cost);

        //        return;
        //    }

        //    for (int i = 0; i < n; i++)
        //    {
        //        if (!visited[i] && graph[currPos, i] != 0)
        //        {
        //            visited[i] = true;
        //            FindFullWayByBruteForceRec(graph, visited, i, n, count + 1, cost + graph[currPos, i], ref ans);

        //            visited[i] = false;
        //        }
        //    }
        //}
        public static List<int> FindFullWayByBruteForce(int[,] graph, int startIndex, out int length)//поиск кратчайщего путем полного перебора
        {
            if(graph.Length <= 1)
            {
                throw new Exception("NonPositiveGrpahSize");
            }

            length = 0;
            List<int> minWay = new List<int>();
            int[] way = new int[graph.GetLength(0) - 1];
            //way[0] = startIndex;
            int k = 0;
            if (startIndex == 0)
            {
                k = 1;
            }
            for (; k < graph.GetLength(0); k++)
            {
                if(k < startIndex)
                {
                    way[k] = k;
                }
                else if(k > startIndex)
                {
                    way[k - 1] = k;
                }
            }

            int minCost = int.MaxValue;
            int cost = 0;
            bool isEnd = false;

            do
            {
                cost = graph[startIndex, way[0]];
                for (int i = 0; i < way.Length - 1; i++)
                {
                    cost += graph[way[i], way[i + 1]];
                }

                if(cost < minCost)
                {
                    minCost = cost;

                    minWay = new List<int>();
                    minWay.Add(startIndex);
                    for (int i = 0; i < way.Length; i++)
                    {
                        minWay.Add(way[i]);
                    }
                    
                }

                isEnd = !NextPermutation(way);
                Console.WriteLine("[{0}, {1}]", startIndex, string.Join(", ", way));
            }
            while (!isEnd);

            length = minCost;
            return minWay;
        }

        public static bool NextPermutation(int[] sequence)//следующая последовательность перестановки
        {
            int i = sequence.Length;
            do
            {
                if (i < 2)
                {
                    return false;
                }
                i--;
            } while (sequence[i - 1] >= sequence[i]);

            int j = sequence.Length;
            while (i < j && sequence[i - 1] >= sequence[--j]);
            SwapItems(sequence, i - 1, j);

            j = sequence.Length;
            while (i < --j)
            {
                SwapItems(sequence, i++, j);
            }

            return true;

            void SwapItems(int[] array, int index_0, int index_1)
            {
                var item = array[index_0];
                array[index_0] = array[index_1];
                array[index_1] = item;
            }
        }

        public static List<int> FindFullWayByGA(int[,] graph, int startIndex, out int length)//поиск кратчайщего жадным алгоритмом
        {
            if (graph.Length <= 1)
            {
                throw new Exception("NonPositiveGrpahSize");
            }

            length = 0;
            List<int> way = new List<int>();
            way.Add(startIndex);

            int searchIndex = startIndex;
            while (way.Count != graph.GetLength(0))
            {
                int minCost = int.MaxValue;
                int minIndex = -1;
                for (int i = 0; i < graph.GetLength(1); i++)
                {
                    if (graph[searchIndex, i] < minCost && graph[searchIndex, i] != -1 && way.IndexOf(i) == -1)
                    {
                        minIndex = i;
                        minCost = graph[searchIndex, i];
                    }
                }
                way.Add(minIndex);
                length += minCost;

                searchIndex = minIndex;
            }

            return way;
        }

        public static int[,] GenerateGraph(int size)//генерация случайной сети городов
        {
            if(size <= 0)
            {
                throw new Exception("NonPositiveGrpahSize");
            }

            int[,] graph = new int[size, size];
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if(i == j)
                    {
                        graph[i, j] = -1;
                    }
                    else
                    {
                        graph[i, j] = random.Next(1, 40);
                    }
                }
            }

            return graph;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter start index:");
            int startIndex = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter graph size:");
            int graphSize = int.Parse(Console.ReadLine());
            int[,] graph = GenerateGraph(graphSize);

            //Brute force
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int lengthBF;
            List<int> way = FindFullWayByBruteForce(graph, startIndex, out lengthBF);

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

            Console.WriteLine($"Min way length by brute force: {lengthBF}");
            Console.WriteLine("[{0}]", string.Join(", ", way));



            //MBB
            sw = new Stopwatch();
            sw.Start();

            int lengthGA;
            way = FindFullWayByGA(graph, startIndex, out lengthGA);

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

            Console.WriteLine($"Min way length by gready method: {lengthGA}");
            Console.WriteLine("[{0}]", string.Join(", ", way));

            Console.ReadKey();
        }
    }
}
