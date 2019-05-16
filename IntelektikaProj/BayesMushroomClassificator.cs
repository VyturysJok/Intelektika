using System;
using System.Collections.Generic;

namespace IntelektikaProj
{
    class BayesMushroomClassificator
    {
        // Bayes algorithm testing properties
        private const decimal defaultProbabilityMain = 0.4M;
        private const decimal neutralProbabilityMain = 0.5M;
        private const decimal isPoisonousBoundaryMain = 0.5M;
        private const int nOfAttributesToCheckMain = 5;
        private const decimal percentOfShroomsToUseForTesting = 0.3M; // 30%

        private Dictionary<Enum, int> MushroomAttributeRepeatCountsPoisonous;
        private Dictionary<Enum, int> MushroomAttributeRepeatCountsEdible;
        private Dictionary<Enum, int> MushroomAttributeRepeatCountsAll;

        private List<Mushroom> ShroomsForTesting;

        private Dictionary<Enum, decimal> poisonousProbabilityTable;

        public BayesMushroomClassificator(List<Mushroom> mushrooms)
        {
            InitLearningAndTestingData(mushrooms);
        }

        public void Run()
        {
            Console.WriteLine("Initing poisonous probability table from learning data...");
            initPoisonousProbabilityTable();
            Console.WriteLine("Finished initing poisonous probability table...\n");
            Console.WriteLine("Beginning Bayes algorithm test..");
            var truePositivesAndFalseNegatives = runBayesAlgorithmTest();
            Console.WriteLine("Bayes algorithm finished\nResults:");
            Console.WriteLine(
                "True positive accuracy: {0:f2}\nFalse negative accuracy: {1:f2}\nTotal accuracy {2:f2}",
                truePositivesAndFalseNegatives.Item1,
                truePositivesAndFalseNegatives.Item2,
                (truePositivesAndFalseNegatives.Item1 + truePositivesAndFalseNegatives.Item2) / 2);

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

        private void InitLearningAndTestingData(List<Mushroom> mushrooms)
        {
            MushroomAttributeRepeatCountsPoisonous = new Dictionary<Enum, int>();
            MushroomAttributeRepeatCountsEdible = new Dictionary<Enum, int>();
            MushroomAttributeRepeatCountsAll = new Dictionary<Enum, int>();
            ShroomsForTesting = new List<Mushroom>();

            int shroomCountToUseForTesting = (int) (mushrooms.Count * percentOfShroomsToUseForTesting);

            for (int i = 0; i < mushrooms.Count - shroomCountToUseForTesting; i++)
            {
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

            for (int i = mushrooms.Count - shroomCountToUseForTesting; i < mushrooms.Count; i++)
            {
                ShroomsForTesting.Add(mushrooms[i]);
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
    }
}
