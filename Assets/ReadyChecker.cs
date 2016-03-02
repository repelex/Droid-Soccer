using UnityEngine;
using System.Collections;

public class ReadyChecker : MonoBehaviour {

    public bool readyStatus = false;

    public ReadyChecker() { }

	// Update is called once per frame
	public void Update () {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            readyStatus = !readyStatus;
            Debug.Log("status is now " + readyStatus);
        }
	}

    public bool isReady() {
        return readyStatus;
    }
}
