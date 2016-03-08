using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector
    public GameObject PlayerCamera;
    private int redPlayers = 0, bluePlayers = 0;
    private GameObject player;

    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            GameObject controller = GameObject.Find("Control");

            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPosition != null)
                {
                    spawnPos = this.SpawnPosition.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;
                

                player = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
                PhotonView photonView = player.GetPhotonView();
                CameraController camera = PlayerCamera.GetComponent<CameraController>();
                camera.enabled = true;
                camera.player = player;
                redPlayers = PunTeams.PlayersPerTeam[PunTeams.Team.red].Count;
                bluePlayers = PunTeams.PlayersPerTeam[PunTeams.Team.blue].Count;
                ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable();
                ht.Add("ready", false);
                ht.Add("all ready", false);
                PhotonNetwork.player.SetCustomProperties(ht);
                
                

                if (redPlayers > bluePlayers)
                {
                    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                    photonView.RPC("ChangeColorToBlue", PhotonTargets.AllBuffered);

                }
                else {
                    PhotonNetwork.player.SetTeam(PunTeams.Team.red);
                    photonView.RPC("ChangeColorToRed", PhotonTargets.AllBuffered);

                }


            }

        }
    }


}
