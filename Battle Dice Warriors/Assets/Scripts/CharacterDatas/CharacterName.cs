using System;
using System.Collections.Generic;

public static class CharacterName
{
    public static readonly List<string> Names = new(){
            "Tharok",
            "Kaelgor",
            "Draven",
            "Varkhan",
            "Morgrax",
            "Strygar",
            "Rulgar",
            "Jorvok",
            "Dromar",
            "Skarn",

            "Areson   ",
            "Thorvald",
            "Herakian",
            "Odryss   ",
            "Fenrik   ",
            "Tyranor   ",
            "Baldrik   ",
            "Zephalon   ",
            "Kratosh   ",
            "Heimdur   ",

            "Alarion",
            "Elandor",
            "Seradin",
            "Caedric",
            "Thalion",
            "Vaelen",
            "Elric",
            "Corwyn",
            "Galrin",
            "Aric",

            "Elthorn",
            "Braelith",
            "Dunmar",
            "Thandor",
            "Mirak",
            "Arthane",
            "Kyrion",
            "Fenlor",
            "Yaldren",
            "Zorin",

            "Garen",
            "Holt",
            "Bran",
            "Joric",
            "Rurik",
            "Doran",
            "Hagan",
            "Maddoc",
            "Wulric",
            "Thorne",
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

