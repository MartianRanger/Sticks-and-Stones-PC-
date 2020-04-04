using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour //Overall game manager
{
    private GameObject player;
    public static GameManager instance = null; //Singleton pattern for entire game
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    public void GameOver()
    {
        SceneManager.LoadScene("You Lose", LoadSceneMode.Single);
    }

    public void playerWin()
    {
        SceneManager.LoadScene("You Win", LoadSceneMode.Single);
    }

}

