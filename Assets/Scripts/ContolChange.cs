using UnityEngine;
using System.Collections;

public class ContolChange : MonoBehaviour
{
    KeyCode UP = KeyCode.W;
    KeyCode DOWN = KeyCode.S;
    KeyCode RIGHT = KeyCode.D;
    KeyCode LEFT = KeyCode.A;

    public void CUp()
    {
        Debug.Log("Click Up");
        
    }
    public void CDown()
    {
        Debug.Log("Click Down");
    }
    public void CRight()
    {
        Debug.Log("Click Right");
    }
    public void CLeft()
    {
        Debug.Log("Click Left");
    }
    public void Test()
    {
        Debug.Log("UP: " + UP.ToString());
        Debug.Log("DOWN: " + DOWN.ToString());
        Debug.Log("LEFT: " + LEFT.ToString());
        Debug.Log("RIGHT: " + RIGHT.ToString());

    }

}
