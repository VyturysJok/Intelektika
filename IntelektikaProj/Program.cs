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
            var bayesProgram = new BayesMushroomClassificator(MushroomManager.Instance.GetMushrooms());
            //bayesProgram.Run();
            bayesProgram.crossValidation();
        }
    }
}
