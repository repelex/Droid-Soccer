using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Control_Scene : MonoBehaviour {
    //Switches back to main menu
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Update()
    {

    }
}
