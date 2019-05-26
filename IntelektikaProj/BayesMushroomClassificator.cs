using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelektikaProj
{
    class BayesMushroomClassificator
    {
        // Bayes algorithm testing properties
        private const decimal defaultProbabilityMain = 0.4M;
        private const decimal neutralProbabilityMain = 0.5M;
        private const decimal isPoisonousBoundaryMain = 0.5M;
        private const int nOfAttributesToCheckMain = 5;
        private const int crossValidationN = 15;
        private const decimal percentOfShroomsToUseForTesting = 0.3M; // 30%

        private List<Mushroom> mushrooms;
        private Dictionary<Enum, int> MushroomAttributeRepeatCountsPoisonous;
        private Dictionary<Enum, int> MushroomAttributeRepeatCountsEdible;
        private Dictionary<Enum, int> MushroomAttributeRepeatCountsAll;

        private Dictionary<Enum, int> AllData_MushroomAttributeRepeatCountsPoisonous;
        private Dictionary<Enum, int> AllData_MushroomAttributeRepeatCountsEdible;
        private Dictionary<Enum, int> AllData_MushroomAttributeRepeatCountsAll;

        private List<Mushroom> ShroomsForTesting;
        private List<Mushroom> AllShroomData;

        private Dictionary<Enum, decimal> poisonousProbabilityTable;
        private Dictionary<Enum, decimal> poisonousProbabilityTableAll;

        private int[] selectedDimensionsIndexes;

        public BayesMushroomClassificator(List<Mushroom> mushrooms)
        {
            this.mushrooms = mushrooms;
        }

        public void Run()
        {
            InitLearningAndTestingData();
            Console.WriteLine("Initing poisonous probability table from learning data...");
            initPoisonousProbabilityTable();
            Console.WriteLine("Finished initing poisonous probability table...\n");

            Console.WriteLine("Initing poisonous probability table from all data...");
            initPoisonousProbabilityTableForAll();
            Console.WriteLine("Finished initing poisonous probability table...\n");
            
            Console.WriteLine("Initing pearsons correlation matrix...");
            var dimensionController = new DimensionController(poisonousProbabilityTable, AllShroomData);
            dimensionController.Run(5);
            selectedDimensionsIndexes = dimensionController.ShrinkedDimensionsIndexes;
            Console.WriteLine("Finnished initing pearsons correleation matrix..\n");
            Console.WriteLine(GetSelectedDimensionsString() + "\n");

            Console.WriteLine("Beginning Bayes algorithm test..");
            var truePositivesAndFalseNegatives = runBayesAlgorithmTest();
            Console.WriteLine("Bayes algorithm finished\nResults:");

            Console.WriteLine(
                "True positive accuracy: {0:f2}\nFalse negative accuracy: {1:f2}\nTotal accuracy {2:f2}",
                truePositivesAndFalseNegatives.Item1,
                truePositivesAndFalseNegatives.Item2,
                (truePositivesAndFalseNegatives.Item1 + truePositivesAndFalseNegatives.Item2) / 2);

        }

        public void crossValidation()
        {
            for (int x = 0; x < crossValidationN; x++)
            {
                InitLearningAndTestingData(x, crossValidationN);
                initPoisonousProbabilityTable();
                var truePositivesAndFalseNegatives = runBayesAlgorithmTest();
                Console.WriteLine("Bayes algorithm finished for cross validation iteration number: {0}\nResults:", x + 1);
                Console.WriteLine(
                    "True positive accuracy: {0:f2}\nFalse negative accuracy: {1:f2}\nTotal accuracy {2:f2}",
                    truePositivesAndFalseNegatives.Item1,
                    truePositivesAndFalseNegatives.Item2,
                    (truePositivesAndFalseNegatives.Item1 + truePositivesAndFalseNegatives.Item2) / 2);
            }
        }


        // returns true positive percentage and false negative percentage
        private Tuple<decimal, decimal> runBayesAlgorithmTest()
        {
            int poisonousTruePositiveCount = 0;
            int poisonousFalseNegativeCount = 0;
            int poisonousCount = 0;
            int edibleCount = 0;
            foreach (var shroom in ShroomsForTesting)
            {
                var poisonousProbability = getPoisonousProbability(shroom);
                if (shroom.IsEdible)
                {
                    edibleCount++;
                    poisonousFalseNegativeCount += poisonousProbability < isPoisonousBoundaryMain ? 1 : 0;
                }
                else
                {
                    poisonousCount++;
                    poisonousTruePositiveCount += poisonousProbability >= isPoisonousBoundaryMain ? 1 : 0;
                }
            }
            return new Tuple<decimal, decimal>((decimal)poisonousTruePositiveCount / poisonousCount, (decimal)poisonousFalseNegativeCount / edibleCount);
        }

        private decimal getPoisonousProbability(Mushroom shroom)
        {
            // Using enum, decimal Tuple just for debugging, decimal List would suffice
            List<Tuple<Enum, decimal>> probabilities = new List<Tuple<Enum, decimal>>();
            foreach (var attribute in shroom.getAttributes())
            {
                decimal Ppoisonous = poisonousProbabilityTable.ContainsKey(attribute) ? poisonousProbabilityTable[attribute] : defaultProbabilityMain;
                probabilities.Add(new Tuple<Enum, decimal>(attribute, Ppoisonous));
            }
            probabilities.Sort((a, b) => Math.Abs(b.Item2 - neutralProbabilityMain).CompareTo(Math.Abs(a.Item2 - neutralProbabilityMain)));
            decimal probabilityProduct = 1.0M;
            decimal probabilityInverseProduct = 1.0M;

            for (int i = 0; i < nOfAttributesToCheckMain; i++)
            {
                probabilityProduct *= probabilities[i].Item2;
                probabilityInverseProduct *= (1 - probabilities[i].Item2);
            }
            decimal probabilityThatShroomIsPoisonous = probabilityProduct / (probabilityProduct + probabilityInverseProduct);
            return probabilityThatShroomIsPoisonous;
        }

        private void InitAllMushroomData(List<Mushroom> mushrooms)
        {
            AllData_MushroomAttributeRepeatCountsPoisonous = new Dictionary<Enum, int>();
            AllData_MushroomAttributeRepeatCountsEdible = new Dictionary<Enum, int>();
            AllData_MushroomAttributeRepeatCountsAll = new Dictionary<Enum, int>();
            AllShroomData = new List<Mushroom>();

            int shroomCountToUseForTesting = (int)(mushrooms.Count * percentOfShroomsToUseForTesting);

            for (int i = 0; i < mushrooms.Count; i++)
            {
                var enums = mushrooms[i].getAttributes();
                AllShroomData.Add(mushrooms[i]);

                foreach (var shroomClassifier in enums)
                {
                    if (mushrooms[i].IsEdible)
                    {
                        if (AllData_MushroomAttributeRepeatCountsEdible.ContainsKey(shroomClassifier))
                        {
                            AllData_MushroomAttributeRepeatCountsEdible[shroomClassifier] = AllData_MushroomAttributeRepeatCountsEdible[shroomClassifier] + 1;
                        }
                        else
                        {
                            AllData_MushroomAttributeRepeatCountsEdible[shroomClassifier] = 1;
                        }
                    }

                    else
                    {
                        if (AllData_MushroomAttributeRepeatCountsPoisonous.ContainsKey(shroomClassifier))
                        {
                            AllData_MushroomAttributeRepeatCountsPoisonous[shroomClassifier] = AllData_MushroomAttributeRepeatCountsPoisonous[shroomClassifier] + 1;
                        }
                        else
                        {
                            AllData_MushroomAttributeRepeatCountsPoisonous[shroomClassifier] = 1;
                        }
                    }

                    if (AllData_MushroomAttributeRepeatCountsAll.ContainsKey(shroomClassifier))
                    {
                        AllData_MushroomAttributeRepeatCountsAll[shroomClassifier] = AllData_MushroomAttributeRepeatCountsAll[shroomClassifier] + 1;
                    }
                    else
                    {
                        AllData_MushroomAttributeRepeatCountsAll[shroomClassifier] = 1;
                    }
                }
            }
        }

        private void InitLearningAndTestingData(List<Mushroom> mushrooms)
        private void InitLearningAndTestingData(int crossValidationI = 0, int crossValidationN = 0)
        {
            MushroomAttributeRepeatCountsPoisonous = new Dictionary<Enum, int>();
            MushroomAttributeRepeatCountsEdible = new Dictionary<Enum, int>();
            MushroomAttributeRepeatCountsAll = new Dictionary<Enum, int>();
            ShroomsForTesting = new List<Mushroom>();

            int shroomCountForTesting;
            int shroomRemainder = 0;
            if (crossValidationN != 0)
            {
                shroomCountForTesting = mushrooms.Count - (int)(mushrooms.Count * (1.0M - (1.0M / crossValidationN)));
                shroomRemainder = mushrooms.Count % crossValidationN;
            }
            else
            {
                shroomCountForTesting = (int)(mushrooms.Count * percentOfShroomsToUseForTesting);
            }

            for (int i = 0; i < mushrooms.Count - (crossValidationN == 0 ? shroomCountForTesting : 0); i++)
            {
               if (crossValidationN != 0 && i >= crossValidationI * shroomCountForTesting && i <= crossValidationI * shroomCountForTesting + shroomCountForTesting)
                    continue;
               
                var enums = mushrooms[i].getAttributes();

                foreach (var shroomClassifier in enums)
                {
                    if (mushrooms[i].IsEdible)
                    {
                        if (MushroomAttributeRepeatCountsEdible.ContainsKey(shroomClassifier))
                        {
                            MushroomAttributeRepeatCountsEdible[shroomClassifier] = MushroomAttributeRepeatCountsEdible[shroomClassifier] + 1;
                        }
                        else
                        {
                            MushroomAttributeRepeatCountsEdible[shroomClassifier] = 1;
                        }
                    }

                    else
                    {
                        if (MushroomAttributeRepeatCountsPoisonous.ContainsKey(shroomClassifier))
                        {
                            MushroomAttributeRepeatCountsPoisonous[shroomClassifier] = MushroomAttributeRepeatCountsPoisonous[shroomClassifier] + 1;
                        }
                        else
                        {
                            MushroomAttributeRepeatCountsPoisonous[shroomClassifier] = 1;
                        }
                    }

                    if (MushroomAttributeRepeatCountsAll.ContainsKey(shroomClassifier))
                    {
                        MushroomAttributeRepeatCountsAll[shroomClassifier] = MushroomAttributeRepeatCountsAll[shroomClassifier] + 1;
                    }
                    else
                    {
                        MushroomAttributeRepeatCountsAll[shroomClassifier] = 1;
                    }
                }
            }
            if (crossValidationN == 0)
            {
                for (int i = mushrooms.Count - shroomCountForTesting; i < mushrooms.Count; i++)
                {
                    ShroomsForTesting.Add(mushrooms[i]);
                }
            }
            else
            {
                for (int i = crossValidationI * shroomCountForTesting; i < crossValidationI * shroomCountForTesting + shroomCountForTesting - shroomRemainder; i++)
                {
                    ShroomsForTesting.Add(mushrooms[i]);
                }
            }
        }

        private void initPoisonousProbabilityTable()
        {
            poisonousProbabilityTable = new Dictionary<Enum, decimal>();

            foreach (var classifier in MushroomAttributeRepeatCountsAll.Keys)
            {
                int poisonousRepeatCount = MushroomAttributeRepeatCountsPoisonous.ContainsKey(classifier) ? MushroomAttributeRepeatCountsPoisonous[classifier] : 0;
                int edibleRepeatCount = MushroomAttributeRepeatCountsEdible.ContainsKey(classifier) ? MushroomAttributeRepeatCountsEdible[classifier] : 0;
                int allRepeatCount = MushroomAttributeRepeatCountsAll.ContainsKey(classifier) ? MushroomAttributeRepeatCountsAll[classifier] : 0;
                if (allRepeatCount == 0)
                {
                    continue;
                }
                decimal Pclassifierpoisonous = (decimal)poisonousRepeatCount / allRepeatCount;
                decimal Pclassifieredible = (decimal)edibleRepeatCount / allRepeatCount;
                decimal PclassifierWord = defaultProbabilityMain;
                if (Pclassifieredible > 0.0M && Pclassifierpoisonous == 0.0M)
                {
                    PclassifierWord = 0.01M;
                }
                else if (Pclassifierpoisonous > 0.0M && Pclassifieredible == 0.0M)
                {
                    PclassifierWord = 0.99M;
                }
                else if (Pclassifierpoisonous > 0 && Pclassifieredible > 0)
                {
                    PclassifierWord = Pclassifierpoisonous / (Pclassifierpoisonous + Pclassifieredible);
                }
                else
                {
                    PclassifierWord = 0.01M;
                }

                poisonousProbabilityTable.Add(classifier, PclassifierWord);
            }
        }

        private void initPoisonousProbabilityTableForAll()
        {
            poisonousProbabilityTableAll = new Dictionary<Enum, decimal>();

            foreach (var classifier in AllData_MushroomAttributeRepeatCountsAll.Keys)
            {
                int poisonousRepeatCount = AllData_MushroomAttributeRepeatCountsPoisonous.ContainsKey(classifier) ? AllData_MushroomAttributeRepeatCountsPoisonous[classifier] : 0;
                int edibleRepeatCount = AllData_MushroomAttributeRepeatCountsEdible.ContainsKey(classifier) ? AllData_MushroomAttributeRepeatCountsEdible[classifier] : 0;
                int allRepeatCount = AllData_MushroomAttributeRepeatCountsAll.ContainsKey(classifier) ? AllData_MushroomAttributeRepeatCountsAll[classifier] : 0;
                if (allRepeatCount == 0)
                {
                    continue;
                }
                decimal Pclassifierpoisonous = (decimal)poisonousRepeatCount / allRepeatCount;
                decimal Pclassifieredible = (decimal)edibleRepeatCount / allRepeatCount;
                decimal PclassifierWord = defaultProbabilityMain;
                if (Pclassifieredible > 0.0M && Pclassifierpoisonous == 0.0M)
                {
                    PclassifierWord = 0.01M;
                }
                else if (Pclassifierpoisonous > 0.0M && Pclassifieredible == 0.0M)
                {
                    PclassifierWord = 0.99M;
                }
                else if (Pclassifierpoisonous > 0 && Pclassifieredible > 0)
                {
                    PclassifierWord = Pclassifierpoisonous / (Pclassifierpoisonous + Pclassifieredible);
                }
                else
                {
                    PclassifierWord = 0.01M;
                }

                poisonousProbabilityTableAll.Add(classifier, PclassifierWord);
            }
        }

        private string GetSelectedDimensionsString()
        {
            string result = "Selected dimensions: ";
            result += selectedDimensionsIndexes.Contains(0) ? " cap-shape," : "";
            result += selectedDimensionsIndexes.Contains(1) ? " cap-surface," : "";
            result += selectedDimensionsIndexes.Contains(2) ? " cap-color," : "";
            result += selectedDimensionsIndexes.Contains(3) ? " bruises," : "";
            result += selectedDimensionsIndexes.Contains(4) ? " odor," : "";
            result += selectedDimensionsIndexes.Contains(5) ? " gill-attachment," : "";
            result += selectedDimensionsIndexes.Contains(6) ? " gill-spacing," : "";
            result += selectedDimensionsIndexes.Contains(7) ? " gill-size," : "";
            result += selectedDimensionsIndexes.Contains(8) ? " gill-color," : "";
            result += selectedDimensionsIndexes.Contains(9) ? " stalk-shape," : "";
            result += selectedDimensionsIndexes.Contains(10) ? " stalk-root," : "";
            result += selectedDimensionsIndexes.Contains(11) ? " stalk-surface-above-ring," : "";
            result += selectedDimensionsIndexes.Contains(12) ? " stalk-surface-below-ring," : "";
            result += selectedDimensionsIndexes.Contains(13) ? " stalk-color-above-ring," : "";
            result += selectedDimensionsIndexes.Contains(14) ? " stalk-color-below-ring," : "";
            result += selectedDimensionsIndexes.Contains(15) ? " veil-type," : "";
            result += selectedDimensionsIndexes.Contains(16) ? " veil-color," : "";
            result += selectedDimensionsIndexes.Contains(17) ? " ring-number," : "";
            result += selectedDimensionsIndexes.Contains(18) ? " ring-type," : "";
            result += selectedDimensionsIndexes.Contains(19) ? " spore-print-color," : "";
            result += selectedDimensionsIndexes.Contains(20) ? " population," : "";
            result += selectedDimensionsIndexes.Contains(21) ? " habitat," : "";
            return result;
        }
    }
}