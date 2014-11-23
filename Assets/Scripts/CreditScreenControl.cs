using UnityEngine;
using System.Collections;

public class CreditScreenControl : MonoBehaviour
{
    public UILabel Title;
    public TweenColor Color;

    public void OnButton_Quit()
    {
        Application.LoadLevel("TitleScreen");
    }

    void OnLevelWasLoaded()
    {
        if (Defines.GameWin)
        {
            Title.text = "Congratulations!";
            Color.enabled = true;
        }
    }
}
