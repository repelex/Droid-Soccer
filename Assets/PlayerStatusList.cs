using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusList : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject.Find("Player Status").GetComponent<Text>().text = "";
        foreach (PhotonPlayer player in PhotonNetwork.playerList)
        {

            Debug.Log(PhotonNetwork.playerList.Length);
            GameObject.Find("Player Status").GetComponent<Text>().text += player.name+" status: " + player.customProperties["ready"] + "\n";
        }

    }
}
