using UnityEngine;
using System.Collections;

public class TitleScreenControl : MonoBehaviour
{

    public void OnButton_NewGame()
    {
        //todo
    }

    public void OnButton_Credits()
    {
        Application.LoadLevel("Credits");
    }

    public void OnButton_Exit()
    {
        Application.Quit();
    }

}
