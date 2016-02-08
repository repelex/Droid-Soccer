using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {


    //MiniGame
	public void PlayGame()
    {
          SceneManager.LoadScene("MiniGame");  
    }

    //Controls Menu
    public void Controls()
    {
        SceneManager.LoadScene("ControlMenu");
    }

    //Credits Menu
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    //Quit Game 
    public void QuitGame()
    {
        Application.Quit();
    }
 }
