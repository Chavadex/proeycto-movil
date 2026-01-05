using UnityEngine;
using System;
using Unity.Services.LevelPlay;

public class LevelPlayAdStrategy : IAdStrategy
{
    private string _rewardedAdUnitID;
    private LevelPlayRewardedAd _rewardedAd;
    private Action<bool> _callBack;

    public void Initialize(AdConfigurationScriptable adConfiguration)
    {
        LevelPlay.Init(adConfiguration.GameID);
        _rewardedAdUnitID = adConfiguration.RewardedAdUnitID;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LevelPlay.ValidateIntegration();
#endif

        LevelPlay.OnInitSuccess += LevelPlayOnOnInitSucces;

#if UNITY_EDITOR
        LevelPlayOnOnInitSucces(null);
#endif
    }

    private void LevelPlayOnOnInitSucces(LevelPlayConfiguration obj)
    {
        _rewardedAd = new LevelPlayRewardedAd(_rewardedAdUnitID);

        _rewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed += OnAdDisplayFailed;
        _rewardedAd.OnAdRewarded += RewardedOnAdRewardedEvent;
        _rewardedAd.OnAdClosed += RewardedOnAdClosedEvent;

        _rewardedAd.LoadAd();
    }

    private void RewardedOnAdClosedEvent(LevelPlayAdInfo adInfo)
    {
        _rewardedAd.LoadAd();
    }

    private void RewardedOnAdRewardedEvent(LevelPlayAdInfo adInfo, LevelPlayReward reward)
    {
        if (reward != null)
            _callBack?.Invoke(true);

        _callBack = null;
    }

    private void OnAdDisplayFailed(LevelPlayAdInfo adInfo, LevelPlayAdError adError)
    {
        Debug.Log($"Error showing ad: {adError.ErrorCode} - {adError.ErrorMessage}");
    }

    private void RewardedOnAdDisplayedEvent(LevelPlayAdInfo adInfo)
    {
    }


    private void RewardedOnAdLoadFailedEvent(LevelPlayAdError error)
    {
        _callBack?.Invoke(false);
    }

    private void RewardedOnAdLoadedEvent(LevelPlayAdInfo adInfo)
    {
        // Opcional
    }

    public void ShowAd(Action<bool> callBack)
    {
        if (!_rewardedAd.IsAdReady()) return;

        _callBack = callBack;
        _rewardedAd.ShowAd(_rewardedAdUnitID);
    }
}
