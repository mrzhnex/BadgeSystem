using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BadgeSystem
{
    class Global
    {
        //main
        public static bool Active = true;

        //static value
        public static System.Random rand = new System.Random();
        public static readonly int idRangeOne = 9500;
        public static readonly int idRangeTwo = 9700;
        public static readonly string voidSymbol = " ";

        //fixed static data
        public static List<string> randomName = new List<string>();
        public static List<string> fixedIdAndName = new List<string>();
        public static readonly string fileNameFixed = "FixedNames.txt";
        public static readonly string fileNameRandom = "RandomNames.txt";
        public static readonly string fileNameColor = "ActionColor.txt";

        //temp data in game
        public static List<string> surnameInGame = new List<string>();

        public static string SetSurName()
        {
            string name = randomName[rand.Next(0, randomName.Count)];
            int id = rand.Next(idRangeOne, idRangeTwo);
            while (surnameInGame.Any(x => x.Contains(id.ToString())))
            {
                id = rand.Next(idRangeOne, idRangeTwo);
            }
            while (surnameInGame.Any(x => x.Contains(name)))
            {
                name = randomName[rand.Next(0, randomName.Count)];
            }
            surnameInGame.Add("[" + id.ToString() + "/" + name + "]");
            return "[" + id.ToString() + "/" + name + "]";
        }

        public static string GetDataFolder()
        {
            return Path.Combine("/etc/scpsl/Plugin");
        }

        public static string color = "army_green";

        public static string bleedout1 = "*Слегка истекает кровью*";
        public static string bleedout2 = "*Истекает кровью*";
        public static string bleedout3 = "*Хлещет кровью*";
        public static string bleedout4 = "*Умирает от кровотечения*";


        public static string pocketkills = "*Гниет*";

        public static string bodyholder = "*Несет тело*";
    }
}