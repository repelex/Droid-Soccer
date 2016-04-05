using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadyChecker : MonoBehaviour {

    public bool readyStatus = false;
    private static bool allReady = false;
    public char[] names = new char[] {  };
    public Rect TextPos = new Rect(1, 80, 150, 300);
     
     //int numberofplayers = 0;
     //int counterforarray = 0;
     // Update is called once per frame


     public void Start() {
          //PhotonNetwork.JoinLobby();
     }
    public void Update () {

        //numberofplayers = PhotonNetwork.countOfPlayers;

        if (Input.GetKeyUp(KeyCode.F2))
        {
            readyStatus = !readyStatus;
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
            ht.Add("ready", readyStatus);

            initializeVars();
            Debug.Log("status is now " + readyStatus);
            if (PhotonNetwork.playerList.Length > 0)
            {
                PhotonNetwork.player.SetCustomProperties(ht);
                allReady = true;
                foreach (PhotonPlayer player in PhotonNetwork.playerList)
                {
                    
                    GameObject.Find("Player Status").GetComponent<Text>().text = "player status: "+player.customProperties["ready"];
                                  
                    Debug.Log(player.ID + " " + player.customProperties["ready"] + " out of " + PhotonNetwork.playerList.Length);  
                    if ((bool)player.customProperties["ready"] == false)
                    {
                        allReady = false;
                    }
                }
                if (allReady)
                {
                         //validateVars();
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

     //Intialize timer and scores
     void initializeVars()
     {
          PlayerPrefs.SetInt("BlueScore", 0);
          PlayerPrefs.SetInt("RedScore", 0);
          PlayerPrefs.SetInt("Time", 300);

          if (PhotonNetwork.isMasterClient)
          {
               ExitGames.Client.Photon.Hashtable h = new ExitGames.Client.Photon.Hashtable();
               h.Add("BlueScore", 0);
               h.Add("RedScore", 0);
               h.Add("Time", 300);
               PhotonNetwork.player.SetCustomProperties(h);
          }
     }
    
}
