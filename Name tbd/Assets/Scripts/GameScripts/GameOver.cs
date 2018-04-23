using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    private GameObject[] leaderboardText;
    private GameObject gameOverText;
    private List<string> vals;

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
        



    public void ShowLeaderboard()
    {
        gameOverText.SetActive(false);
        foreach(GameObject text in leaderboardText)
        {
            text.SetActive(true);
        }
        UpdateLeaderboard();
    }


    private void UpdateLeaderboard()
    {
        vals = SaveLoad.Load();
        List<string[]> stats = new List<string[]>();
        foreach (string stat in vals) stats.Add(stat.Split(','));
        
    }

	/**
	 * Loads MainMenu
	 * @Param None
	 * @Return None
	**/
	public void toMainMenu(){
		SceneManager.LoadScene ("MainMenu");
	}

    public void QuitGame()
    {
        Application.Quit();
    }
		
}
