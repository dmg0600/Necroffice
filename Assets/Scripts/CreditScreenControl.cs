using UnityEngine;
using System.Collections;

public class CreditScreenControl : MonoBehaviour 
{
    public void OnButton_Quit()
    {
        Application.LoadLevel("TitleScreen");
    }
}
