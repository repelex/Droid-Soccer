using UnityEngine;
using System.Collections;

public class ReadyChecker : MonoBehaviour {

    public bool readyStatus = false;
    private static bool allReady = false;
    
	// Update is called once per frame
	public void Update () {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            readyStatus = !readyStatus;
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
            ht.Add("ready", readyStatus);
            
            
            Debug.Log("status is now " + readyStatus);
            if (PhotonNetwork.playerList.Length > 0)
            {
                PhotonNetwork.player.SetCustomProperties(ht);
                allReady = true;
                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                {
                    Debug.Log(player.ID + " " + player.customProperties["ready"] + " out of " + PhotonNetwork.playerList.Length);  
                    if ((bool)player.customProperties["ready"] == false)
                    {
                        allReady = false;
                    }
                }
                if (allReady)
                {
                    PhotonView photonView = GameObject.Find("Control").GetComponent<PhotonView>();
                    photonView.RPC("changeLevel", PhotonTargets.AllBuffered);
                }
            }
        }
        
	}

    [PunRPC]
    public void changeLevel(){
        PhotonNetwork.LoadLevel("MiniGame");
    }

}
