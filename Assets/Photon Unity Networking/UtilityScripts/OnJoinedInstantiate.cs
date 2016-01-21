using UnityEngine;
using System.Collections;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector
    public GameObject PlayerCamera;
    

    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            PlayerCamera= GameObject.FindGameObjectWithTag("Camera");
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

                GameObject player = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
                CameraController camera = PlayerCamera.GetComponent<CameraController>();
                camera.enabled = true;
                camera.player = player;
                
            }
        }
    }
}
