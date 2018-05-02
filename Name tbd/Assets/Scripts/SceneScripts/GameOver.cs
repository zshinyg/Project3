using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private GameObject[] leaderboardText;
    private GameObject gameOverText;
    private List<string> vals;
    List<string[]> stats;

    public void Awake()
    {
        GameObject music = GameObject.FindGameObjectWithTag("Music");
        Destroy(music);


        leaderboardText = GameObject.FindGameObjectsWithTag("Leaderboard");
        gameOverText = GameObject.Find("GameOverText");

        gameOverText.SetActive(true);
        foreach(GameObject text in leaderboardText)
        {
            text.SetActive(false);
        }
        Invoke("ShowLeaderboard", 3f);
    }
        



    public void ShowLeaderboard()
    {
        gameOverText.SetActive(false);
        foreach(GameObject text in leaderboardText)
        {
            text.SetActive(true);
        }
        UpdateLeaderboard();
        SetLeaderboardText();
    }


    private void UpdateLeaderboard()
    {
        vals = SaveLoad.Load();                                                 // vals is a List<string> where each string is name,level,duration
        stats = new List<string[]>();
        foreach (string stat in vals) stats.Add(stat.Split(','));               // each stats is [name,level,duration]

        for (int i = 0; i < stats.Count; i++)                                      // Iterate through the array to see if the new level can/should be added to the array
        {
            if (GameVariables.LevelReached >= int.Parse(stats[i][1]))
            {
                string[] tempPlayerString = new string[3];
                tempPlayerString[0] = GameVariables.PlayerName;
                tempPlayerString[1] = GameVariables.LevelReached.ToString();
                tempPlayerString[2] = GameVariables.LevelReached.ToString();

                if (GameVariables.LevelReached > int.Parse(stats[i][2]))
                    stats.Insert(i, tempPlayerString);
                else
                    stats.Insert(i + 1, tempPlayerString);

                break;
            }
        }

        while (stats.Count > 10)
            stats.RemoveAt(stats.Count - 1);
        
    }


    private void SetLeaderboardText()
    {
        foreach (string[] playerStat in stats)
        {
            GameObject.Find("PlayerText").GetComponent<Text>().text = playerStat[0] + "\n";
            GameObject.Find("ScoreText").GetComponent<Text>().text = playerStat[1] + "\t" + playerStat[2] + "\n";
        }
    }


    private void SaveLeaderboard()
    {
        while (vals.Count > 0)
            vals.RemoveAt(0);
        foreach (string[] toConvert in stats)
            vals.Add(toConvert[0]+","+toConvert[1]+","+toConvert[2]);
    }

	/**
	 * Loads MainMenu
	 * @Param None
	 * @Return None
	**/
	public void toMainMenu(){
        SaveLeaderboard();
		SceneManager.LoadScene ("MainMenu");
	}

    public void QuitGame()
    {
        Application.Quit();
    }
		
}
