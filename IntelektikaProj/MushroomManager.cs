using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;

namespace IntelektikaProj
{
    class MushroomManager
    {
        public static MushroomManager Instance = new MushroomManager();
        private readonly HttpClient _client = new System.Net.Http.HttpClient();
        private const string BaseApiUrl = "https://www.kaggle.com/api/v1/";
        private const string CompetitionName = "uciml/mushroom-classification";
        private const string DatasetURL = "datasets/download/uciml/mushroom-classification/mushrooms.csv";
        private List<Mushroom> MushroomCache;

        private void Authorize()
        {
            if (_client.DefaultRequestHeaders.Authorization != null)
            {
                // already authorized
                return;
            }

            var auth = new { Username = string.Empty, Key = string.Empty };
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
        }
        public List<Mushroom> GetMushrooms()
        {
            if (MushroomCache != null)
            {
                return new List<Mushroom>(MushroomCache);
            }

            Console.WriteLine("Loading mushrooms from database...");
            Authorize();
            List<Mushroom> result = new List<Mushroom>();
            var getTask = _client.GetStringAsync(BaseApiUrl + DatasetURL);
            var json = getTask.Result;

            string[] lines = json.Split('\n').Skip(1).ToArray();

            foreach (var line in lines)
            {
                result.Add(new Mushroom(line));
            }

            MushroomCache = new List<Mushroom>(result);
            Console.WriteLine("Mushrooms finished loading\n");
            return result;
        }
    }
}
