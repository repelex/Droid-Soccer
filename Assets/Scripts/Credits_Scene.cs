using UnityEngine;
using System.Collections;

public class Credits_Scene : MonoBehaviour {
    public GameObject credits;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 20;
    }
	
	// Update is called once per frame
	void Update () {
        
        Application.targetFrameRate = 20;
        credits.transform.Translate(0, 1,0);
       
        if (credits.transform.position.y >= (Screen.height*2 + 100)){
            
            credits.transform.Translate(0, -Screen.height*2 - 100, 0);
        }
    }
}
