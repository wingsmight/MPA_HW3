using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_HW3
{
    class Program
    {
        static void FindFullWayByBruteForce(int[,] graph, bool[] v, int currPos, int n, int count, int cost, ref int ans)
        {
            if (count == n && graph[currPos, 0] != 0)
            {
                ans = Math.Min(ans, cost);
                //ans = Math.Min(ans, cost + graph[currPos, 0]);
                return;
            }

            for (int i = 0; i < n; i++)
            {
                if (!v[i] && graph[currPos, i] != 0)
                {
                    v[i] = true;
                    FindFullWayByBruteForce(graph, v, i, n, count + 1, cost + graph[currPos, i], ref ans);

                    v[i] = false;
                }
            }
        }

        static void FindWay(ref List<List<int>> graph, out int startNode, out int finishNode)
        {
            //2
            graph[0].Add(-1);
            for (int i = 1; i < graph.Count; i++)
            {
                int minOfRow = FindMinOfRow(graph, i);

                graph[i].Add(minOfRow);
            }

            //3
            for (int i = 1; i < graph.Count; i++)
            {
                for (int j = 1; j < graph[i].Count - 1; j++)
                {
                    if (graph[i][j] != -1)
                    {
                        graph[i][j] -= graph[i][graph[i].Count - 1];
                    }
                }
            }

            //4
            List<int> newRow = new List<int>();
            newRow.Add(-1);
            for (int i = 1; i < graph.Count; i++)
            {
                int minOfcolumn = FindMinOfColumn(graph, i);

                newRow.Add(minOfcolumn);
            }
            newRow.Add(-1);
            graph.Add(newRow);

            //5
            for (int i = 1; i < graph.Count - 1; i++)
            {
                for (int j = 1; j < graph[i].Count - 1; j++)
                {
                    if (graph[i][j] != -1)
                    {
                        graph[i][j] -= graph[graph.Count - 1][j];
                    }
                }
            }

            //6
            graph.RemoveAt(graph.Count - 1);
            for (int i = 0; i < graph.Count; i++)
            {
                graph[i].RemoveAt(graph[i].Count - 1);
            }

            int fake0Max = 0;
            int columnMax = 0;
            int rowMax = 0;
            List<List<int>> onlyFake0list = CreateZero2DList(graph.Count, graph[graph.Count - 1].Count);
            for (int i = 1; i < graph.Count; i++)
            {
                for (int j = 1; j < graph[i].Count; j++)
                {
                    if (graph[i][j] == 0)
                    {
                        graph[i][j] = -1;
                        onlyFake0list[i][j] = FindMinOfColumn(graph, j) + FindMinOfRow(graph, i);
                        if(onlyFake0list[i][j] > fake0Max)
                        {
                            fake0Max = onlyFake0list[i][j];
                        }
                        graph[i][j] = 0;
                    }
                }
            }

            for (columnMax = 0; columnMax < onlyFake0list.Count; columnMax++)
            {
                rowMax = onlyFake0list[columnMax].IndexOf(fake0Max);
                if (rowMax != -1)
                {
                    break;
                }
            }

            startNode = graph[columnMax][0];
            finishNode = graph[0][rowMax];

            Console.WriteLine(startNode + " -> " + finishNode);

            graph[rowMax][columnMax] = -1;

            //Delete rowMax and columnMax
            for (int i = 0; i < graph.Count; i++)
            {
                graph[i].RemoveAt(rowMax);
            }
            graph.RemoveAt(columnMax);
        }

        static List<int> FindFullWayByMBB(List<List<int>> graph)
        {
            List<int> way = new List<int>();

            for (int i = graph.Count; i > 3; i--)
            {
                //FindWay(ref graph, out int startNode, out int finishNode, way.Count == 0, startIndex);
                FindWay(ref graph, out int startNode, out int finishNode);

                way.Add(startNode);
                way.Add(finishNode);
            }

            if(graph[0][1] != way.First())
            {
                way.Add(way.Last());
                Console.Write(way.Last() + " -> ");
                way.Add(graph[0][1]);
                Console.WriteLine(way.Last());
            }
            else
            {
                way.Add(way.Last());
                Console.Write(way.Last() + " -> ");
                way.Add(graph[0][2]);
                Console.WriteLine(way.Last());
            }

            return way;
        }

        private static int FindMinOfRow(List<List<int>> list, int rowIndex)
        {
            List<int> row = list[rowIndex];
            int min = int.MaxValue;
            for (int i = 1; i < list.Count; i++)
            {
                if(row[i] != -1 && row[i] < min)
                {
                    min = row[i];
                }
            }

            list = null;
            return min;
        }

        private static int FindMaxOfRow(List<List<int>> list, int rowIndex)
        {
            List<int> row = list[rowIndex];
            int max = 0;
            for (int i = 1; i < row.Count; i++) 
            {
                if (row[i] != -1 && row[i] > max)
                {
                    max = row[i];
                }
            }

            return max;
        }

        private static int FindMinOfColumn(List<List<int>> list, int indexColumn)
        {
            int min = int.MaxValue;
            for (int i = 1; i < list.Count; i++)
            {
                if(list[i][indexColumn] != -1 && list[i][indexColumn] < min)
                {
                    min = list[i][indexColumn];
                }
            }

            return min;
        }

        static int[,] GenerateGraph(int size)
        {
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

        static void Main(string[] args)
        {
            // n is the number of nodes i.e. V 

            //int[,] graph1 = {
            //    { -1, 10, 15, 20 },
            //    { 10, -1, 35, 25 },
            //    { 15, 35, -1, 30 },
            //    { 20, 25, 30, -1 }
            //};

            int[,] graph2 = {
                { -1, 5, 11, 9 },
                { 10, -1, 8, 7 },
                { 7, 14, -1, 8 },
                { 12, 6, 15, -1 }
            };

            Console.WriteLine("Enter graph size:");
            int graphSize = int.Parse(Console.ReadLine());
            int[,] graph = GenerateGraph(graphSize);

            //Brute force
            int minWay = int.MaxValue;
            for (int i = 0; i < graphSize; i++)
            {
                bool[] v = new bool[graphSize];
                for (int j = 0; j < v.Length; j++)
                {
                    v[j] = false;
                }

                v[i] = true;
                int ans = int.MaxValue;

                FindFullWayByBruteForce(graph, v, i, graphSize, 1, 0, ref ans);

                minWay = Math.Min(minWay, ans);
            }

            Console.WriteLine($"Min way length by brute force: {minWay}");


            //MBB
            List<int> way = FindFullWayByMBB(ArrayToList(graph));

            int wayLength = 0;
            for (int i = 0; i < way.Count - 1; i += 2)
            {
                wayLength += graph[way[i],way[i + 1]];
            }
            Console.WriteLine($"Min way length by method of branches and borders: {wayLength}");

            Console.ReadKey();
        }

        static List<List<int>> ArrayToList(int[,] array)
        {
            List<List<int>> newList = new List<List<int>>();

            newList.Add(new List<int>());
            for (int j = -1; j < array.GetLength(1); j++)
            {
                newList[0].Add(j);
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                newList.Add(new List<int>());
                newList[i + 1].Add(i);
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    newList[i + 1].Add(array[i, j]);
                }
            }

            return newList;
        }

        private static List<List<int>> CreateZero2DList(int columnLenght, int rowLenght)
        {
            List<List<int>> newList = new List<List<int>>();

            newList.Add(new List<int>());
            for (int j = -1; j < rowLenght; j++)
            {
                newList[0].Add(j);
            }

            for (int i = 0; i < columnLenght + 1; i++)
            {
                newList.Add(new List<int>());
                newList[i + 1].Add(i);
                for (int j = 0; j < rowLenght; j++)
                {
                    newList[i + 1].Add(0);
                }
            }

            return newList;
        }
    }
}
