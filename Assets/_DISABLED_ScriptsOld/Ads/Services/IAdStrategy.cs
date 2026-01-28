public interface IAdStrategy
{
    void ShowAd(System.Action<bool> callBack);
    void Initialize(AdConfigurationScriptable adConfiguration);


}
