using System;
using UnityEngine;

public class No_Ads : IAdStrategy
{
    public void Initialize(AdConfigurationScriptable adConfiguration)
    {
        Debug.Log("No ads Strategy Initialized");
    }

    public void ShowAd(Action<bool> callBack)
    {
        callBack.Invoke(true);
    }
}