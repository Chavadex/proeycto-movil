using UnityEngine;

public class AdView : MonoBehaviour
{
    [SerializeField] private AdManager _adManager;

    public void ShowAd()
    {
        Debug.Log("Aqui pulsamos el BOTON");
        _adManager.ShowAd(RewardedAdCallBack);
    }

    private void RewardedAdCallBack(bool obj)
    {
        if (obj)
        {
            Debug.Log("Reward the player");
        }
        else
        {
            Debug.Log("Do not reward the player");
        }
    }
}
