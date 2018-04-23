using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad {

    public static List<string> leaderboard;

    public static void Save(List<string> stats)
    {
        try
        {
            leaderboard = stats;
            StreamWriter file = new StreamWriter(Application.persistentDataPath + "/leaderboard.txt");
            foreach (string stat in leaderboard) file.WriteLine(stat);
            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

    }

    public static List<string> Load()
    {
        leaderboard.Clear();
        try
        {
            StreamReader file = new StreamReader(Application.persistentDataPath + "/leaderboard.txt");
            string line = file.ReadLine();
            while (line != null)
            {
                leaderboard.Add(line);
                line = file.ReadLine();
            }
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }
        return leaderboard;
    }
}
