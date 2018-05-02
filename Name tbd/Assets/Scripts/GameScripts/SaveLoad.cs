using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad {

    public static List<string> leaderboard;


    /* Save
     * @param List<string>
     * @return none
     * Takes in a list of strings with values to write to file
     */
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


    /* Load
     * @param none
     * @return List<string>
     * Reads a file, makes it a list of strings and returns that list
     */
    public static List<string> Load()
    {

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
