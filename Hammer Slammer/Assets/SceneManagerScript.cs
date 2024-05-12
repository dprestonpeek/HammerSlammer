using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (player && player.transform.position.y < -4)
            YouDied();
    }

    public void PlayOnClick()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("EndScreen");
    }

    public void YouDied()
    {
        SceneManager.LoadScene("EndScreen", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
