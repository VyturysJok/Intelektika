using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;


namespace IntelektikaProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var mushrooms = MushroomManager.Instance.GetMushrooms();

            var bayesProgram = new BayesMushroomClassificator(mushrooms);
            bayesProgram.Run();

            var compProgram = new CompetitiveMushroomClassificator(mushrooms);
            compProgram.Run();
        }
    }
}
