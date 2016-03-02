using UnityEngine;
using System.Collections;

public class Credits_Scene : MonoBehaviour {
    public GameObject credits;
    // Use this for initialization
    void Start () {
        credits.transform.Translate(0, 1,0);
    }
	
	// Update is called once per frame
	void Update () {
        credits.transform.Translate(0, 1,0);
    }
}
