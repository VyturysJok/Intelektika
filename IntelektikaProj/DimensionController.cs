using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelektikaProj
{
    public class DimensionController
    {
        private readonly Dictionary<Enum, decimal> PoisonousProbabilityTable;
        private readonly List<Mushroom> MushroomData;
        private double[] Avarages;
        private const int ParameterCount = 22;

        //Indexes of important dimensions after shrink
        public int[] ShrinkedDimensionsIndexes;

        public DimensionController(Dictionary<Enum, decimal> poisonousProbabilityTable, List<Mushroom> data)
        {
            PoisonousProbabilityTable = poisonousProbabilityTable;
            MushroomData = data;
        }

        public void Run(int numberOfFields)
        {
            Console.WriteLine("   Forming Pearsons matrix...");
            var correlationMatrix = GetCorrelationMatrix();
            Console.WriteLine("   Pearsons matrix formed.");

            Console.WriteLine("   Shrink dimensions...");
            ShrinkedDimensionsIndexes = ShrinkDimensionsAndGetSelected(correlationMatrix, numberOfFields);
            Console.WriteLine("   Dimensions shrinked.");
        }

        public int[] ShrinkDimensionsAndGetSelected(double[,] correlationMatrix, int n)
        {
            var result = new int[22];

            var matrixSumDictionary = GetMatrixSums(correlationMatrix);
            var matrixSummatrixSumDictionaryTopN = GetTopNParameters(n, matrixSumDictionary);

            result = matrixSummatrixSumDictionaryTopN.Keys.ToArray();
            return result;
        }

        public Dictionary<int, double> GetTopNParameters(int n, Dictionary<int, double> fullDictionary)
        {
            var result = new Dictionary<int, double>();

            for (int i = 0; i < n; i++)
            {
                var keyOfMaxValue = fullDictionary.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                result.Add(keyOfMaxValue, fullDictionary[keyOfMaxValue]);
                fullDictionary.Remove(keyOfMaxValue);
            }

            return result;
        }

        public Dictionary<int, double> GetMatrixSums(double[,] correlationMatrix)
        {
            //Convert correlation matrix to array with sums of each column exepct diagonal values
            var result = new Dictionary<int, double>();

            for (int x = 0; x < ParameterCount; x++)
            {
                for (int y = 0; y < ParameterCount; y++)
                {
                    if (x != y)
                    {
                        if (result.ContainsKey(x))
                        {
                            result[x] += correlationMatrix[x, y];
                        }
                        else
                        {
                            result.Add(x, correlationMatrix[x, y]);
                        }
                    }
                }
            }
            return result;
        }

        public double[,] GetCorrelationMatrix()
        {
            Avarages = new double[22];
            var result = new double[22,22];

            for (int i = 0; i < ParameterCount; i++)
            {
                GetParameterAvarages(i);
            }

            for (int x = 0; x < ParameterCount; x++)
            {
                for (int y = 0; y < ParameterCount; y++)
                {
                    result[x, y] = CalculateRxy(x, y);
                }
            }

            return result;
        }

        public double CalculateRxy(int x, int y)
        {
            double rXY = 0;
            double top = 0;
            double bottomX = 0;
            double bottomY = 0;

            foreach (var mushroom in MushroomData)
            {
                var xi = (double) PoisonousProbabilityTable[mushroom.getAttributes()[x]];
                var yi = (double) PoisonousProbabilityTable[mushroom.getAttributes()[y]];

                top += (xi - Avarages[x]) * (yi - Avarages[y]);
                bottomX += Math.Pow((xi - Avarages[x]), 2);
                bottomY += Math.Pow((yi - Avarages[y]), 2);
            }

            rXY = top / (Math.Sqrt(bottomX) * Math.Sqrt(bottomY));

            return rXY;
        }

        public void GetParameterAvarages(int index)
        {
            double sum = 0;

            foreach (var mushroom in MushroomData)
            {
                sum += (double) PoisonousProbabilityTable[mushroom.getAttributes()[index]];
            }

            Avarages[index] = sum / MushroomData.Count;
        }
    }
}
