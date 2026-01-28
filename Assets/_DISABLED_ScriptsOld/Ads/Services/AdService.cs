using Unity.VisualScripting;
using UnityEngine;
using System;

public class AdService
{
    private AdConfigurationScriptable adConfiguration;
    private IAdStrategy adStrategy;

    public AdService(AdConfigurationScriptable adConfiguration, IAdStrategy adStrategy)
    {
        this.adConfiguration = adConfiguration;
        this.adStrategy = adStrategy;
        Initialize();
    }


    private void Initialize()
    {
        adStrategy.Initialize(adConfiguration);
    }

    public void ShowAd(System.Action<bool> callBack)
    {
        adStrategy.ShowAd(callBack);
    }
}
