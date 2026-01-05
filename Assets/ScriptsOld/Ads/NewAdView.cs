using UnityEngine;


public class NewAdView : MonoBehaviour
{
    [SerializeField] LaserController _laserController;

   // [SerializeField] private Score _score;

    private void Start()
    {
      //  _score = FindFirstObjectByType<Score>();
    }
    public void ShowRewardAd()
    {
        var showRewardAdUseCase = Locator.Get<ShowRewardAdUseCase>();
        showRewardAdUseCase.Show(RewardedAdCallback);
        _laserController.addOneLaser();
    }

    public void ShowRewardAd2()
    {
        var showRewardAdUseCase = Locator.Get<ShowRewardAdUseCase>();
        showRewardAdUseCase.Show(RewardedAdCallback2);
    }


    private void RewardedAdCallback(bool obj)
    {
        if (obj)
        {
            Debug.Log("Reward the player");
            _laserController.addOneLaser();
            //  _score.DuplicateScore();
        }
        else
        {
            Debug.Log("Do not reward the player");
        }
    }

    private void RewardedAdCallback2(bool obj)
    {
        if (obj)
        {
            Debug.Log("Reward the player reviviendo");
            _laserController.addLaserAdsReward();
           // _score.RevivePlayer();
        }
        else
        {
            Debug.Log("Do not reward the player");
        }
    }

}