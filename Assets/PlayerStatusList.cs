using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(PhotonView))]

public class PlayerStatusList : Photon.MonoBehaviour {



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        

        GameObject.Find("Player Status").GetComponent<Text>().text = "";
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {
            if (player.ID != -1) {

                string status = "Not Ready";
                if (player.customProperties["ready"].Equals(true))
                {
                    status = "Ready";
                }

                GameObject.Find("Player Status").GetComponent<Text>().text += "player " + player.ID + " status: " + status + "\n";


            }
        }

    }
}
