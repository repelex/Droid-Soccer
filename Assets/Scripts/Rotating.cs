using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {
	public int Speed=2;

    void Update ()
    {
		transform.Rotate(new Vector3 (0,1,0) * Time.deltaTime/Speed);
    }
}
