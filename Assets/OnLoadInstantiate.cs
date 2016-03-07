using UnityEngine;
using System.Collections;

public class OnLoadInstantiate : MonoBehaviour {
    private GameObject player;
    public GameObject PlayerCamera;
    // Use this for initialization
    void Awake() {
	
	}
	
	// Update is called once per frame
	void Start () {
        Vector3 random = Random.insideUnitSphere;
        random.y = 0;
        random = random.normalized;
        Vector3 itempos = new Vector3(0,11,0) + 10.0f * random;
        player = PhotonNetwork.Instantiate("PlayerSphere", itempos, Quaternion.identity, 0);
        PhotonView photonView = player.GetPhotonView();
        CameraController camera = PlayerCamera.GetComponent<CameraController>();
        camera.enabled = true;
        camera.player = player;
        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.red)
        {
            photonView.RPC("ChangeColorToRed", PhotonTargets.AllBuffered);
        }
        else {
            photonView.RPC("ChangeColorToBlue", PhotonTargets.AllBuffered);
        }
    }



    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected " + player.name);
        PhotonNetwork.DestroyPlayerObjects(player);
    }


}
