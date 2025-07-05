using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ebac.Core.Singleton;

public class UIInGameManager : Singleton<UIInGameManager>
{
    public TextMeshProUGUI uiTextCoins;
    public TextMeshProUGUI uiTextSatellites;
    
    public static void UpdateTextCoins(string c)
    {
        Instance.uiTextCoins.text = c;
    }
    public static void UpdateTextSatellites(string s)
    {
        Instance.uiTextSatellites.text = s;
    }
}
