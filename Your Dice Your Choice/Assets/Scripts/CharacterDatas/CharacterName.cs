using System;
using System.Collections.Generic;

namespace Assets.Scripts.CharacterDatas
{
    public static class CharacterName
    {
        public static readonly List<string> Names = new(){
            "Ironmaw",
            "Stonefist",
            "Bulwark",
            "Gravethorn",
            "Cragshield",
            "Steelhide",
            "Brassclad",
            "Rocksoul",
            "Forgeback",
            "Dreadplate",

            "Oathguard   ",
            "Thornhelm",
            "Aegiron",
            "Valcrux   ",
            "Garranox   ",
            "Shielden   ",
            "Vanguardus   ",
            "Korran   ",
            "Hearthwall   ",
            "Barrold   ",
        };

        /// <summary>
        /// Gets the random name in the list.
        /// </summary>
        /// <returns></returns>
        public static string GetName()
        {
            int rndIndex = new Random().Next(0, CharacterGenerator.Instance.CharacterNames.Count);
            string tempName = CharacterGenerator.Instance.CharacterNames[rndIndex];
            CharacterGenerator.Instance.CharacterNames.Remove(tempName);

            return tempName;
        }
    }
}
