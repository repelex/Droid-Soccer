using UnityEngine;
using System.Collections;

public class ColorRPC : MonoBehaviour {

    [PunRPC]
    public void ChangeColorToBlue()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(0, 0, 1, 1);
        
    }

    [PunRPC]
    public void ChangeColorToRed()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(1, 0, 0, 1);

    }


}
