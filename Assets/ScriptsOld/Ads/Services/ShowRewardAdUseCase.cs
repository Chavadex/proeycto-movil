
public class ShowRewardAdUseCase
{
    private AdService _adService;
    private int _dataLenght;

    public ShowRewardAdUseCase(AdService adService)
    {
        _adService = adService;
    }

    public void Show(System.Action<bool> callBack)
    {
        _adService.ShowAd(callBack);
    }
}
