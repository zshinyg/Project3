using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelector : MonoBehaviour {

	public void SelectGingy()
    {
        GameVariables.CharacterName = "Gingy";
        Invoke("StartGame", .5f);
    }

    public void SelectShadowGingy()
    {
        GameVariables.CharacterName = "ShadowGingy";
        Invoke("StartGame", .5f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
}
