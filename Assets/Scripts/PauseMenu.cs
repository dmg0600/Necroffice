using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;

    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void OnClose()
    {
        Close();
    }

    public void OnQuit()
    {
        Close();
        Application.LoadLevel("TitleScreen");
    }
}
