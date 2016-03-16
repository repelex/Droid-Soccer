using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float timer = 500;
    Text txt;
    

    void Update()
    {
        timer -= Time.deltaTime;
        
        txt.text = string.Format("00:00", timer.ToString());

        if (timer <= 0)
        {
            timer = 0;
            SceneManager.LoadScene("Lobby");
        }
    }

    void Start()
    {
        txt = GetComponent<Text>();
        txt.text = "00.00";
    }
}

