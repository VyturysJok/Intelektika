using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace IntelektikaProj
{
    class CompetitiveMushroomClassificator
    {
        private const decimal percentOfShroomsToUseForTesting = 0.3M; // 30%

        private List<MushroomNumerical> ShroomsForTesting;
        private List<MushroomNumerical> ShroomsForLearning;
        private double[][] Weights = new double[2][];
        private double[] Outputs = new double[2];
        private List<double[]> InputsLearn = new List<double[]>();
        private double[][] InputsTest;
        private double mu = 0.05;


        public void Run()
        {
            Console.WriteLine("\n\nBeginning neural network learning");
            Learning();
            Console.WriteLine("Finished learning\n");
            Console.WriteLine("Beginning test..");
            Testing();
        }


        public CompetitiveMushroomClassificator(List<Mushroom> mushrooms)
        {
            InitLearningAndTestingData(mushrooms);
        }

        private void Learning()
        {

            for (int i = 0; i < ShroomsForLearning.Count(); i++)
            {
                double[] temp = new double[23];
                temp[0] = ShroomsForLearning[i].CapShapeValue;
                temp[1] = ShroomsForLearning[i].CapSurfaceValue;
                temp[2] = ShroomsForLearning[i].CapColorValue;
                temp[3] = ShroomsForLearning[i].BruisesValue;
                temp[4] = ShroomsForLearning[i].OdorValue;
                temp[5] = ShroomsForLearning[i].GillAttachmentValue;
                temp[6] = ShroomsForLearning[i].GillSpacingValue;
                temp[7] = ShroomsForLearning[i].GillSizeValue;
                temp[8] = ShroomsForLearning[i].GillColorValue;
                temp[9] = ShroomsForLearning[i].StalkShapeValue;
                temp[10] = ShroomsForLearning[i].StalkRootValue;
                temp[11] = ShroomsForLearning[i].StalkSurfaceAboveRingValue;
                temp[12] = ShroomsForLearning[i].StalkSurfaceBelowRingValue;
                temp[13] = ShroomsForLearning[i].StalkColorAboveRingValue;
                temp[14] = ShroomsForLearning[i].StalkColorBelowRingValue;
                temp[15] = ShroomsForLearning[i].VeilTypeValue;
                temp[16] = ShroomsForLearning[i].VeilColorValue;
                temp[17] = ShroomsForLearning[i].RingNumberValue;
                temp[18] = ShroomsForLearning[i].RingTypeValue;
                temp[19] = ShroomsForLearning[i].SporePrintColorValue;
                temp[20] = ShroomsForLearning[i].PopulationValue;
                temp[21] = ShroomsForLearning[i].HabitatValue;
                temp[22] = ShroomsForLearning[i].IsEdible ? 1 : 0;
                InputsLearn.Add(temp);
            }
            var random = new Random(123);
            for (int i = 0; i < Outputs.Length; i++)
            {
                Weights[i] = new double[InputsLearn[i].Length-1];
                for (int j = 0; j < InputsLearn[i].Length-1; j++)
                {
                    Weights[i][j] = random.NextDouble();
                }
            }
            
            for (int iter = 0; iter < 1000; iter++)
            {
                var temp = InputsLearn.ToList();
                List<double[]> randomList = new List<double[]>();
                int randIndex;
                Random rnd = new Random(iter);
                while (temp.Count > 0)
                {
                    randIndex = rnd.Next(0, temp.Count);
                    randomList.Add(temp[randIndex]);
                    temp.RemoveAt(randIndex);
                }
                for (int i = 0; i < ShroomsForLearning.Count(); i++)
                {
                    for (int j = 0; j < Outputs.Length; j++)
                    {
                        Outputs[j] = 0;
                        for (int h = 0; h < InputsLearn[i].Length-1; h++)
                        {
                            Outputs[j] += Weights[j][h] * randomList[i][h];
                        }
                    }

                    if (Outputs[0] > Outputs[1])
                    {
                        for (int j = 0; j < InputsLearn[i].Length-1; j++)
                        {
                            Weights[0][j] = Weights[0][j] + mu * (randomList[i][j] - Weights[0][j]);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < InputsLearn[i].Length-1; j++)
                        {
                            Weights[1][j] = Weights[1][j] + mu * (randomList[i][j] - Weights[1][j]);
                        }
                    }
                }
                mu = mu / (1 + (1 * iter));
            }
        }

        private void Testing()
        {
            for (int i = 0; i < ShroomsForTesting.Count(); i++)
            {
                InputsTest[i] = new double[22];
                InputsTest[i][0] = ShroomsForTesting[i].CapShapeValue;
                InputsTest[i][1] = ShroomsForTesting[i].CapSurfaceValue;
                InputsTest[i][2] = ShroomsForTesting[i].CapColorValue;
                InputsTest[i][3] = ShroomsForTesting[i].BruisesValue;
                InputsTest[i][4] = ShroomsForTesting[i].OdorValue;
                InputsTest[i][5] = ShroomsForTesting[i].GillAttachmentValue;
                InputsTest[i][6] = ShroomsForTesting[i].GillSpacingValue;
                InputsTest[i][7] = ShroomsForTesting[i].GillSizeValue;
                InputsTest[i][8] = ShroomsForTesting[i].GillColorValue;
                InputsTest[i][9] = ShroomsForTesting[i].StalkShapeValue;
                InputsTest[i][10] = ShroomsForTesting[i].StalkRootValue;
                InputsTest[i][11] = ShroomsForTesting[i].StalkSurfaceAboveRingValue;
                InputsTest[i][12] = ShroomsForTesting[i].StalkSurfaceBelowRingValue;
                InputsTest[i][13] = ShroomsForTesting[i].StalkColorAboveRingValue;
                InputsTest[i][14] = ShroomsForTesting[i].StalkColorBelowRingValue;
                InputsTest[i][15] = ShroomsForTesting[i].VeilTypeValue;
                InputsTest[i][16] = ShroomsForTesting[i].VeilColorValue;
                InputsTest[i][17] = ShroomsForTesting[i].RingNumberValue;
                InputsTest[i][18] = ShroomsForTesting[i].RingTypeValue;
                InputsTest[i][19] = ShroomsForTesting[i].SporePrintColorValue;
                InputsTest[i][20] = ShroomsForTesting[i].PopulationValue;
                InputsTest[i][21] = ShroomsForTesting[i].HabitatValue;
            }

            int TruePositive = 0;
            int edibleCount = 0;
            int FalseNegative = 0;
            int poisonousCount = 0;
            for (int i = 0; i < ShroomsForTesting.Count(); i++)
            {
                for (int j = 0; j < Outputs.Length; j++)
                {
                    Outputs[j] = 0;
                    for (int h = 0; h < InputsTest[i].Length; h++)
                    {
                        Outputs[j] += Weights[j][h] * InputsTest[i][h];
                    }
                }

                if (Outputs[0] > Outputs[1]) // jei nevalgomas grybas
                {
                    TruePositive += !ShroomsForTesting[i].IsEdible ? 1 : 0;
                    edibleCount += ShroomsForTesting[i].IsEdible ? 1 : 0;
                    poisonousCount += ShroomsForTesting[i].IsEdible ? 0 : 1;
                }
                else // jei valgomas grybas
                {
                    FalseNegative += ShroomsForTesting[i].IsEdible ? 1 : 0;
                    poisonousCount += ShroomsForTesting[i].IsEdible ? 0 : 1;
                    edibleCount += ShroomsForTesting[i].IsEdible ? 1 : 0;
                }
            }
            Console.WriteLine("True positive accuracy: {0:f2}", (double)TruePositive / poisonousCount);
            Console.WriteLine("False negative accuracy: {0:f2}", (double)FalseNegative / edibleCount);
            Console.WriteLine("Total accuracy: {0:f2}", (((double)TruePositive / poisonousCount) + ((double)FalseNegative / edibleCount)) / 2);
        }

        private void InitLearningAndTestingData(List<Mushroom> mushrooms)
        {
            ShroomsForTesting = new List<MushroomNumerical>();
            ShroomsForLearning = new List<MushroomNumerical>();

            List<MushroomNumerical> temp = new List<MushroomNumerical>();
            for (int i = 0; i < mushrooms.Count; i++)
            {
                temp.Add(new MushroomNumerical(mushrooms[i]));
            }


            int shroomCountToUseForTesting = (int)(mushrooms.Count * percentOfShroomsToUseForTesting);
            temp = Normalize(temp);





            for (int i = 0; i < mushrooms.Count - shroomCountToUseForTesting; i++)
            {
                ShroomsForLearning.Add(temp[i]);
            }

            for (int i = mushrooms.Count - shroomCountToUseForTesting; i < mushrooms.Count; i++)
            {
                ShroomsForTesting.Add(temp[i]);
            }
            //InputsLearn = new double[ShroomsForLearning.Count()][];
            InputsTest = new double[ShroomsForTesting.Count()][];
        }

        private List<MushroomNumerical> Normalize(List<MushroomNumerical> mushroom)
        {
            List<MushroomNumerical> numerical = new List<MushroomNumerical>();
            MLContext mlContext = new MLContext();
            var data = mlContext.Data.LoadFromEnumerable(mushroom);
            var minMaxEstimator = mlContext.Transforms.NormalizeMinMax("CapShapeValue", fixZero: false);
            var minMaxTransformer = minMaxEstimator.Fit(data);
            var transformedData = minMaxTransformer.Transform(data);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("CapSurfaceValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("CapColorValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("BruisesValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("OdorValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("GillAttachmentValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("GillSpacingValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("GillSizeValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("GillColorValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkShapeValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkRootValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkSurfaceAboveRingValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkSurfaceBelowRingValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkColorAboveRingValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("StalkColorBelowRingValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("VeilTypeValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("VeilColorValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("RingNumberValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("RingTypeValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("SporePrintColorValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("PopulationValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);

            minMaxEstimator = mlContext.Transforms.NormalizeMinMax("HabitatValue", fixZero: false);
            minMaxTransformer = minMaxEstimator.Fit(transformedData);
            transformedData = minMaxTransformer.Transform(transformedData);
            var column1 = transformedData.GetColumn<double>("CapShapeValue").ToArray();
            var column2 = transformedData.GetColumn<double>("CapSurfaceValue").ToArray();
            var column3 = transformedData.GetColumn<double>("CapColorValue").ToArray();
            var column4 = transformedData.GetColumn<double>("BruisesValue").ToArray();
            var column5 = transformedData.GetColumn<double>("OdorValue").ToArray();
            var column6 = transformedData.GetColumn<double>("GillAttachmentValue").ToArray();
            var column7 = transformedData.GetColumn<double>("GillSpacingValue").ToArray();
            var column8 = transformedData.GetColumn<double>("GillSizeValue").ToArray();
            var column9 = transformedData.GetColumn<double>("GillColorValue").ToArray();
            var column10 = transformedData.GetColumn<double>("StalkShapeValue").ToArray();
            var column11 = transformedData.GetColumn<double>("StalkRootValue").ToArray();
            var column12 = transformedData.GetColumn<double>("StalkSurfaceAboveRingValue").ToArray();
            var column13 = transformedData.GetColumn<double>("StalkSurfaceBelowRingValue").ToArray();
            var column14 = transformedData.GetColumn<double>("StalkColorAboveRingValue").ToArray();
            var column15 = transformedData.GetColumn<double>("StalkColorBelowRingValue").ToArray();
            var column16 = transformedData.GetColumn<double>("VeilTypeValue").ToArray();
            var column17 = transformedData.GetColumn<double>("VeilColorValue").ToArray();
            var column18 = transformedData.GetColumn<double>("RingNumberValue").ToArray();
            var column19 = transformedData.GetColumn<double>("RingTypeValue").ToArray();
            var column20 = transformedData.GetColumn<double>("SporePrintColorValue").ToArray();
            var column21 = transformedData.GetColumn<double>("PopulationValue").ToArray();
            var column22 = transformedData.GetColumn<double>("HabitatValue").ToArray();
            for (int i = 0; i < column1.Count(); i++)
            {
               numerical.Add(new MushroomNumerical(mushroom[i].IsEdible, column1[i], column2[i], column3[i], column4[i], column5[i], column6[i], column7[i], column8[i], column9[i], column10[i], column11[i], column12[i], column13[i], column14[i], column15[i], column16[i], column17[i], column18[i], column19[i], column20[i], column21[i], column22[i]));
            }
            return numerical;
        }
    }
}
