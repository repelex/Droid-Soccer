using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public GameObject player;
    public bool experimental = false;

    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate ()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            experimental = !experimental;
        }
        if (!experimental)
        {
            transform.position = player.transform.position + offset;
        }
        else {
            
            transform.position = player.transform.position;
            Vector3 playerballvec = new Vector3(target.transform.position.x - player.transform.position.x, target.transform.position.y - player.transform.position.y, target.transform.position.z - player.transform.position.z);
            playerballvec.Normalize();
            playerballvec.Scale(new Vector3(3, 3, 3));
            transform.position = new Vector3(player.transform.position.x - playerballvec.x,
                                            player.transform.position.y - playerballvec.y + 3,
                                            player.transform.position.z - playerballvec.z);
            transform.LookAt(target.transform);
        }
    }

}
