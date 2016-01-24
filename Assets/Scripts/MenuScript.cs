using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {


     //Switches to the MiniGame scene
	public void PlayGame()
    {
          SceneManager.LoadScene("MiniGame");  
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
