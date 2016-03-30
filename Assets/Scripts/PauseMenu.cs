using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject menu;
    private bool isShowing = false;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isShowing = !isShowing;
            menu.SetActive(isShowing);
        }
    }
}
