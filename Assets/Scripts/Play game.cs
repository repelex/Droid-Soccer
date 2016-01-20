using UnityEngine;
using System.Collections;

public class Playgame : MonoBehaviour {

	public void PlayGame()
    {
        Application.loadedLevel();/*Needs to add a int value from play game however unsure how we want to set player setup in game play
        atm it showed just switch to game scene from main menu scene*/
    }
    public void QuitGame()
    {
        Application.Quit();

    }
}
