using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoad {

    public static List<string> leaderboard;


    /* Save
     * @param List<string> 
     * @return none
     * Takes in a list of strings and saves it to file
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


    /* Load()
     * @param none
     * @return List<string>
     * Reads the scoreboard list and returns a list of the scoreboard
     */
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
