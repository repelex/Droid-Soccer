using UnityEngine;
using System.Collections;

public class ChangeSceneOnLevel : MonoBehaviour {
    private bool allReady = false;
	// Update is called once per frame
	void Update () {
        /*
        if (PhotonNetwork.playerList.Length > 0)
        {
            allReady = true;
            foreach (PhotonPlayer player in PhotonNetwork.playerList)
            {
                if ((bool)player.customProperties["ready"] == false)
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
        */
	}
}
