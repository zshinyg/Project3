using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameVariables {

    private static string characterName;
    private static int levelReached;
    private static double levelDuration;
    private static string playerName;

    public static string CharacterName
    {
        get
        {
            return characterName;
        }
        set
        {
            characterName = value;
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
            levelReached = value;
        }
    }

    public static double LevelDuration
    {
        get
        {
            return levelDuration;
        }
        set
        {
            levelDuration = value;
        }
    }


    public static string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            playerName = value;
        }
    }

}
