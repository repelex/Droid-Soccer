using UnityEngine;
using System.Collections;

public class ColorRPC : MonoBehaviour {

    [PunRPC]
    public void ChangeColorToBlue()
    {
        GameObject controller = GameObject.Find("Control");
        gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 1, 1);
        controller.GetComponent<TeamManager>().bluePlayers++;
        
    }

    [PunRPC]
    public void ChangeColorToRed()
    {
        Debug.Log("red team");
        GameObject controller = GameObject.Find("Control");
        gameObject.GetComponent<Renderer>().material.color = new Vector4(1, 0, 0, 1);
        controller.GetComponent<TeamManager>().redPlayers++;
    }


}
