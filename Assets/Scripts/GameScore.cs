using UnityEngine;
using System.Collections;

public class GameScore : MonoBehaviour {
    public GameObject Player_Scores;
    private bool isShowing = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("Tab")){
            isShowing = !isShowing;
            Player_Scores.SetActive(isShowing);
        }
    }
}
