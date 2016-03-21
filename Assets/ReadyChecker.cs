using UnityEngine;
using System.Collections;

public class ReadyChecker : MonoBehaviour {

    public bool readyStatus = false;
    private static bool allReady = false;
    public char[] names = new char[] {  };
    public Rect TextPos = new Rect(1, 80, 150, 300);
    int numberofplayers = 0;
    int counterforarray = 0;
    // Update is called once per frame
    public void Update () {
       /* if (numberofplayers != PhotonNetwork.countOfPlayers)
        {
            while (numberofplayers != PhotonNetwork.countOfPlayers)
            {
                names[counterforarray] = (PhotonNetwork.playerName[counterforarray]);


                counterforarray += 1;
            }
        }*/
        numberofplayers = PhotonNetwork.countOfPlayers;

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
                    
                    GUILayout.BeginArea(TextPos);

                    
                    
                    GUILayout.Label(string.Format("",names));
                    // GUILayout.Label(string.Format("test:{0:0.000)}",(timetostart)));
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
