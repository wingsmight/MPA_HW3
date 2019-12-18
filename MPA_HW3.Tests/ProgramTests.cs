using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPA_HW3.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GenerateGraph_NonPositiveSize_Exception()
        {
            //arrange
            string expected = "NonPositiveGrpahSize";

            //act
            Exception exception = Assert.ThrowsException<Exception>(() => Program.GenerateGraph(-1));
            string actual = exception.Message;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GenerateGraph_PositiveSize_Generated()
        {
            //arrange
            int expected = Program.GenerateGraph(5).GetLength(0);

            //act
            int actual = 5;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NextPermutation_Size3_Done()
        {
            //arrange
            int[] sequence = { 1, 2, 3};

            int[,] expected =
            {
                {1, 2, 3 },
                {1, 3, 2 },
                {2, 1, 3 },
                {2, 3, 1 },
                {3, 1, 2 },
                {3, 2, 1 }
            };

            //act
            int[,] actual = new int[6, 3];// 6 = 3 * 2 * 1
            int i = 0;
            for (int j = 0; j < sequence.Length; j++)
            {
                actual[i, j] = sequence[j];
            }
            i++;
            while (Program.NextPermutation(sequence))
            { 
                for (int j = 0; j < sequence.Length; j++)
                {
                    actual[i, j] = sequence[j];
                }
                i++;
            }

            //assert
            for (i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }

        [TestMethod]
        public void NextPermutation_Empty_FalseReturn()
        {
            //arrange
            int[] sequence = { };

            bool expected = false;

            //act
            bool actual = Program.NextPermutation(sequence);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindFullWayByBruteForce_EmptyOr1Size_Exception()
        {
            //arrange
            int[,] graph = { };
            int startIndex = 0;//no matter
            string expected = "NonPositiveGrpahSize";

            //act
            int actualLenght;
            Exception exception = Assert.ThrowsException<Exception>(() => Program.FindFullWayByBruteForce(graph, startIndex, out actualLenght));
            string actual = exception.Message;

            //assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void FindFullWayByBruteForce_3SizeGraph_Found()
        {
            //arrange
            int[,] graph =
            {
                {-1, 22, 32 },
                {11, -1, 12 },
                {20, 10, -1 }
            };

            int startIndex = 0;
            List<int> expected = new List<int>();
            expected.Add(startIndex);
            expected.Add(1);
            expected.Add(2);

            int expectedLenght = 34;//22 + 12

            //act
            int actualLenght;
            List<int> actual = Program.FindFullWayByBruteForce(graph, startIndex, out actualLenght);

            //assert
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLenght, actualLenght);
        }

        [TestMethod]
        public void FindFullWayByBruteForce_4SizeGraph_Found()
        {
            //arrange
            int[,] graph =
            {
                {-1, 5, 11, 9},
                {10, -1, 8, 7 },
                {7, 14, -1, 8 },
                {12, 6, 15, -1 }
            };

            int startIndex = 0;
            List<int> expected = new List<int>();
            expected.Add(startIndex);
            expected.Add(1);
            expected.Add(2);
            expected.Add(3);

            int expectedLenght = 21;//5 + 8 + 8

            //act
            int actualLenght;
            List<int> actual = Program.FindFullWayByBruteForce(graph, startIndex, out actualLenght);

            //assert
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLenght, actualLenght);
        }

        [TestMethod]
        public void FindFullWayByGA_EmptyOr1Size_Exception()
        {
            //arrange
            int[,] graph = { };
            int startIndex = 0;//no matter
            string expected = "NonPositiveGrpahSize";

            //act
            int actualLenght;
            Exception exception = Assert.ThrowsException<Exception>(() => Program.FindFullWayByGA(graph, startIndex, out actualLenght));
            string actual = exception.Message;

            //assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void FindFullWayByGA_3SizeGraph_Found()
        {
            //arrange
            int[,] graph =
            {
                {-1, 22, 32 },
                {11, -1, 12 },
                {20, 10, -1 }
            };

            int startIndex = 0;
            List<int> expected = new List<int>();
            expected.Add(startIndex);
            expected.Add(1);
            expected.Add(2);

            int expectedLenght = 34;//22 + 12

            //act
            int actualLenght;
            List<int> actual = Program.FindFullWayByGA(graph, startIndex, out actualLenght);

            //assert
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLenght, actualLenght);
        }

        [TestMethod]
        public void FindFullWayByGA_4SizeGraph_Found()
        {
            //arrange
            int[,] graph =
            {
                {-1, 5, 11, 9},
                {10, -1, 8, 7 },
                {7, 14, -1, 8 },
                {12, 6, 15, -1 }
            };

            int startIndex = 0;
            List<int> expected = new List<int>();
            expected.Add(startIndex);
            expected.Add(1);
            expected.Add(3);
            expected.Add(2);

            int expectedLenght = 27;//5 + 7 + 15

            //act
            int actualLenght;
            List<int> actual = Program.FindFullWayByGA(graph, startIndex, out actualLenght);

            //assert
            CollectionAssert.AreEqual(expected, actual);
            Assert.AreEqual(expectedLenght, actualLenght);
        }
    }
}
