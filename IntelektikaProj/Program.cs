using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace IntelektikaProj
{
    class Program
    {
        const decimal defaultProbabilityMain = 0.4M;
        const decimal neutralProbabilityMain = 0.5M;
        const decimal isSpamBoundaryMain = 0.6M;

        private static readonly HttpClient _client = new HttpClient();
        private const string BaseApiUrl = "https://www.kaggle.com/api/v1/";
        private const string CompetitionName = "uciml/mushroom-classification";

        private static Dictionary<Enum, int> PoisonousCount = new Dictionary<Enum, int>();
        private static Dictionary<Enum, int> EdibleCount = new Dictionary<Enum, int>();
        private static Dictionary<Enum, int> Counts = new Dictionary<Enum, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Kaggle Api C# example.");
            var auth = new { Username = string.Empty, Key = string.Empty };

            Console.WriteLine("Creating output folder ...");
            if (Directory.Exists(CompetitionName))
            {
                Directory.Delete(CompetitionName, true);
            }

            Directory.CreateDirectory(CompetitionName);

            Console.WriteLine("Loading api auth information ...");
            using (var reader = new StreamReader("kaggle.json"))
            {
                var json = reader.ReadToEnd();
                auth = JsonConvert.DeserializeAnonymousType(json, auth);
            }
            Console.WriteLine($"username: {auth.Username}, password: {auth.Key}", auth);
            var authToken = Convert.ToBase64String(
                System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format($"{auth.Username}:{auth.Key}", auth)
                ));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var data = ParseData();

            foreach (var shroom in data)
            {
                var enums = shroom.getEnums();

                foreach (var shroomClassifier in enums)
                {
                    if (shroom.IsEdible)
                    {
                        if (EdibleCount.ContainsKey(shroomClassifier))
                        {
                            EdibleCount[shroomClassifier] = EdibleCount[shroomClassifier] + 1;
                        }
                        else
                        {
                            EdibleCount[shroomClassifier] = 1;
                        }
                    }

                    else
                    {
                        if (PoisonousCount.ContainsKey(shroomClassifier))
                        {
                            PoisonousCount[shroomClassifier] = PoisonousCount[shroomClassifier] + 1;
                        }
                        else
                        {
                            PoisonousCount[shroomClassifier] = 1;
                        }
                    }

                    if (Counts.ContainsKey(shroomClassifier))
                    {
                        Counts[shroomClassifier] = Counts[shroomClassifier] + 1;
                    }
                    else
                    {
                        Counts[shroomClassifier] = 1;
                    }
                }
            }

            var a = PoisonousCount;
            var b = EdibleCount;
            var bbz = getWordSpamProbabilityTable(PoisonousCount, EdibleCount, Counts);
        }

        static Dictionary<Enum, decimal> getWordSpamProbabilityTable(Dictionary<Enum, int> poisonousClassifierCounts, Dictionary<Enum, int> edibleClassifierCounts, Dictionary<Enum, int> allClassifierCounts)
        {
            Dictionary<Enum, decimal> poisonousProbabilityTable = new Dictionary<Enum, decimal>();

            foreach (var classifier in allClassifierCounts.Keys)
            {
                int poisonousRepeatCount = poisonousClassifierCounts.ContainsKey(classifier) ? poisonousClassifierCounts[classifier] : 0;
                int edibleRepeatCount = edibleClassifierCounts.ContainsKey(classifier) ? edibleClassifierCounts[classifier] : 0;
                int allRepeatCount = allClassifierCounts.ContainsKey(classifier) ? allClassifierCounts[classifier] : 0;
                if (allRepeatCount == 0) {
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
            return poisonousProbabilityTable;
        }


        static List<Mushroom> ParseData()
        {
            List<Mushroom> result = new List<Mushroom>();
            var getTask = _client.GetStringAsync(BaseApiUrl + "datasets/download/uciml/mushroom-classification/mushrooms.csv");
            var json = getTask.Result;

            string[] lines = json.Split('\n').Skip(1).ToArray();

            foreach (var line in lines)
            {
                result.Add(new Mushroom(line));
            }

            return result;
        }

        static List<string> ListCompetitionFiles()
        {
            var getTask = _client.GetStringAsync(BaseApiUrl + "datasets/list/" + CompetitionName);
            var json = getTask.Result;
            var list = new List<string>();
            dynamic files = JsonConvert.DeserializeObject<dynamic[]>(json);

            foreach (dynamic file in files)
            {
                // fields, case senstive
                // ref
                // description
                // name
                // totalBytes
                // url - this is not the api download url, do not use this
                // creationDate
                Console.WriteLine($"{file.name}", file);
                list.Add(file.name.ToString());
            }

            return list;
        }

        static void DownloadCompetitionFiles(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                Console.WriteLine($"downloading {file} ...", file);
                var filename = CompetitionName + "/" + file;
                var getTask = _client.GetStreamAsync(BaseApiUrl + "competitions/data/download/" + filename);
                using (var stream = getTask.Result)
                using (var output = new FileStream(filename, FileMode.CreateNew))
                {
                    stream.CopyTo(output);
                }
            }
        }
    }
}
