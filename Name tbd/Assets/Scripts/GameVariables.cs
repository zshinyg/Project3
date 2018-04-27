using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables {

    private static string characterName;
    private static int levelReached;
    private static float levelDuration;

    public static string CharacterName
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = CharacterName;
        }
    }

    public static int LevelReached
    {
        get
        {
            return levelReached;
        }
        set
        {
            levelReached = LevelReached;
        }
    }

    public static float LevelDuration
    {
        get
        {
            return levelDuration;
        }
        set
        {
            levelDuration = LevelDuration;
        }
    }

}
