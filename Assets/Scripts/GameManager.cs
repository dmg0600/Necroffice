using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Ya hay más de un GameManager en " + Application.loadedLevelName);
            Destroy(gameObject);
        }
        Instance = this;
    }

    #endregion


}
