using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {


     //Switches to the MiniGame scene
	public void PlayGame()
    {
          SceneManager.LoadScene("MiniGame");  
    }
    //Quit Game 
    public void QuitGame()
    {
        Application.Quit();

    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
 }
