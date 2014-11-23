using UnityEngine;
using System.Collections;

public class TitleScreenControl : MonoBehaviour
{

    public TweenAlpha FadeCurtain;

    public void OnButton_NewGame()
    {
        StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        FadeCurtain.enabled = true;

        yield return new WaitForSeconds(0.6f);

        Application.LoadLevel("Main");
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
