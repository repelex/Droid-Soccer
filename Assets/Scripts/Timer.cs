using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timer = 500;
    Text txt;

    void Update()
    {
        timer -= Time.deltaTime;
        txt.text = timer.ToString("00:00");
        if (timer <= 0)
        {
            timer = 0;
        }
    }

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "0.00";
    }
}

