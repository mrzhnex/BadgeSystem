﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BadgeSystem
{
    public static class Global
    {
        public static bool Active = true;
        public static System.Random rand = new System.Random();
        public static readonly int idRangeOne = 9500;
        public static readonly int idRangeTwo = 9700;
        public static readonly string voidSymbol = " ";

        public static List<string> randomName = new List<string>();
        public static List<string> fixedIdAndName = new List<string>();
        public static readonly string fileNameFixed = "FixedNames.txt";
        public static readonly string fileNameRandom = "RandomNames.txt";
        public static readonly string fileNameColor = "ActionColor.txt";

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

        public static string pocketkills = "*Гниет*";

        public static string bodyholder = "*Несет тело*";
    }
}