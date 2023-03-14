using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveLoadManager
{
    #region COIN

    const string KEY_COIN = "coinajsfakshdsa";

    public static int GetCoin() => PlayerPrefs.GetInt(KEY_COIN, 0);

    public static void AddCoin(int amount)
    {
        PlayerPrefs.SetInt(KEY_COIN, GetCoin() + amount);
    }

    #endregion
    
    
    
}
