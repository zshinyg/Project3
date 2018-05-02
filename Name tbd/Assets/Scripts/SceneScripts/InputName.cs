using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour {

	public void SubmitName()
    {
        GameVariables.PlayerName = GameObject.FindGameObjectWithTag("NameInput").GetComponent<InputField>().text;
        SceneManager.LoadScene("PlayerSelector");
    }
}
