using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    private bool isShowing = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("escape"))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }

    //Quit Game
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
