using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private GameObject[] leaderboardText;
    private GameObject gameOverText;
    private List<string> vals;
    private List<string[]> stats;



    /* Awake
     * @param  none
     * @return none
     * Run when GameOver is started, finds text objects and delays the "Game Over" screen
     */
    public void Awake()
    {
        leaderboardText = GameObject.FindGameObjectsWithTag("Leaderboard");
        gameOverText = GameObject.Find("GameOverText");

        gameOverText.SetActive(true);
        foreach(GameObject text in leaderboardText)
        {
            text.SetActive(false);
        }
        Invoke("ShowLeaderboard", 3f);
    }
        


    /* ShowLeaderboard
     * @param none
     * @return none
     * Sets leaderboard text to show and hides "Game Over"
     */
    public void ShowLeaderboard()
    {
        gameOverText.SetActive(false);
        foreach(GameObject text in leaderboardText)
        {
            text.SetActive(true);
        }
        UpdateLeaderboard();
    }



    /* UpdateLeaderboard()
     * @param none
     * @return none
     * Loads the leaderboard from the last game and shows the highest scores
     */
    private void UpdateLeaderboard()
    {
        vals = SaveLoad.Load();
        foreach (string stat in vals) stats.Add(stat.Split(','));

        Text players = GameObject.Find("PlayerText").GetComponent<Text>();
        Text scores = GameObject.Find("ScoreText").GetComponent<Text>();

        for (int i = 0; i < stats.Count; i++)
        {
            players.text += stats[i][0] + "\n";
            scores.text += stats[i][1] + "\t" + stats[i][2] + "\n";
        }

        AddNewScore();
    }

    private void AddNewScore()
    {
        int newLevel = GameStats.Level;
        float newTime = GameStats.LevelDuration;
        int savedLevel;
        float savedTime;
        for(int i = 0; i < stats.Count; i ++)
        {

            savedLevel = int.Parse(stats[i][1]);
            savedTime = int.Parse(stats[i][2]);
            if (savedLevel <= newLevel)
            {
                string[] newPlayer = new string[3];
                newPlayer[0] = GetPlayerName();
                newPlayer[1] = newLevel.ToString();
                if (savedTime < newTime)
                {
                    newPlayer[2] = newTime.ToString();
                }
                else
                {
                    newPlayer[2] = newTime.ToString();
                }
                stats.Insert(i, newPlayer);
                break;
            }
        }

        while (stats.Count > 10)
        {
            stats.RemoveAt(stats.Count - 1);
        }
        
    }


    private string GetPlayerName()
    {
        string name = "";


        return name;
    }

	/**
	 * Loads MainMenu
	 * @Param None
	 * @Return None
	**/
	public void toMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}


    /* QuitGame()
     * @param none
     * @return none
     * Quits the game
     */
    public void QuitGame()
    {
        Application.Quit();
    }
		
}
