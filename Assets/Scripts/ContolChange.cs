using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ContolChange : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
