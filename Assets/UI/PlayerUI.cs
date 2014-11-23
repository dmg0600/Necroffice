using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance = null;

    void Awake()
    {
        Instance = this;
    }

    public UILabel PlayerName;

    public UISprite[] PWR, AGI;

    public UITexture WeaponTexture;

    public UISlider HealthSlider;

    public void Refresh(Creature Data)
    {
        PlayerName.text = Data.Name;

        DrawPower(Data._Stats.Power.value, Data._Weapon.Power);

        DrawPower(Data._Stats.Agility.value, Data._Weapon.Agility);

        WeaponTexture.mainTexture = Data._Weapon._icon;

        HealthSlider.value = Data._Life.life.Get01Value();
    }

    void DrawPower(int innate, int bonus)
    {
        int _total = innate + bonus;

        for (int i = 0; i < 5; i++)
        {
            if (i < innate)
                PWR[i].enabled = true;
            else
                PWR[i].enabled = false;
        }
    }

    void DrawAgility(int innate, int bonus)
    {
        int _total = innate + bonus;

        for (int i = 0; i < 5; i++)
        {
            if (i < innate)
                AGI[i].enabled = true;
            else
                AGI[i].enabled = false;
        }
    }

}
