#define USE_LEVELPLAY
using UnityEngine;

public class GameInitializer : MonoBehaviour
{

    private AdService _adService;
    public AdConfigurationScriptable adConfiguration;
    ShowRewardAdUseCase _showRewardedAdUseCase;
    private void Start()
    {
        Debug.Log("Game Initialzied");
        IAdStrategy adStrategy;

#if USE_LEVELPLAY
        adStrategy = new LevelPlayAdStrategy();
#elif USE_ADMON
#else
adStrategy = new No_Ads();
#endif


        _adService = new AdService(adConfiguration, adStrategy);
        _showRewardedAdUseCase = new ShowRewardAdUseCase(_adService);

        Locator.Set(_showRewardedAdUseCase);
    }
}
