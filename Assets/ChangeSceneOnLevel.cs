using UnityEngine;
using System.Collections;

public class ChangeSceneOnLevel : MonoBehaviour {
    private bool allReady = false;
	// Update is called once per frame
	void Update () {
        
        if (GameObject.FindGameObjectsWithTag("Player").Length -1 > 0)
        {
            Debug.Log(GameObject.FindGameObjectsWithTag("Player").Length);
            allReady = true;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length -1; i++)
            {
                Debug.Log(i);
                if (GameObject.FindGameObjectsWithTag("Player")[i].GetComponent<ReadyChecker>().readyStatus == false)
                {
                    allReady = false;
                }
            }
            if (allReady == true)
            {
                PhotonNetwork.automaticallySyncScene = true;
                PhotonNetwork.LoadLevel("MiniGame");
            }
        }
	}
}
