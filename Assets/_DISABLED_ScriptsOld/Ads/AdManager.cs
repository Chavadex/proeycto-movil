using UnityEngine;
using Unity.Services.LevelPlay;
using System;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    [SerializeField] private string androidGameId = string.Empty;
    [SerializeField] private string rewardedAdUnity = string.Empty;
    private LevelPlayRewardedAd _rewardedAd;
    private Action<bool> _callBack;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        LevelPlay.Init(androidGameId);
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        LevelPlay.ValidateIntegration();
#endif
        LevelPlay.OnInitSuccess += LevelPlayOnOnInitSucces;


    }

    private void LevelPlayOnOnInitSucces(LevelPlayConfiguration obj)
    {
        _rewardedAd = new LevelPlayRewardedAd(rewardedAdUnity);

        _rewardedAd.OnAdLoaded += RewardedOnAdLoadedEvent;
        _rewardedAd.OnAdLoadFailed += RewardedOnAdLoadFailedEvent;
        _rewardedAd.OnAdDisplayed += RewardedOnAdDisplayedEvent;
        _rewardedAd.OnAdDisplayFailed += RewardedOnAdDisplayFailedEvent;
        _rewardedAd.OnAdRewarded += RewardedOnAdRewardedEvent;
        _rewardedAd.OnAdClosed += RewardedOnAdClosedEvent;
        _rewardedAd.LoadAd();
    }

    private void RewardedOnAdClosedEvent(LevelPlayAdInfo obj)
    {
        _rewardedAd.LoadAd();
    }

    private void RewardedOnAdRewardedEvent(LevelPlayAdInfo arg1, LevelPlayReward arg2)
    {
        if (arg2 != null)
            _callBack?.Invoke(true);
        _callBack = null;
    }

    private void RewardedOnAdDisplayFailedEvent(LevelPlayAdInfo adInfo, LevelPlayAdError adError)
    {
        Debug.Log($"Error showing ad: {adError.ErrorCode} - {adError.ErrorMessage}");
    }


    private void RewardedOnAdDisplayedEvent(LevelPlayAdInfo obj)
    {

    }

    private void RewardedOnAdLoadFailedEvent(LevelPlayAdError obj)
    {
        _callBack?.Invoke(false);
    }

    private void RewardedOnAdLoadedEvent(LevelPlayAdInfo obj)
    {

    }

    public void ShowAd(Action<bool> callBack)
    {
        if (!_rewardedAd.IsAdReady()) return;
        _callBack = callBack;
        _rewardedAd.ShowAd(rewardedAdUnity);
    }

}
