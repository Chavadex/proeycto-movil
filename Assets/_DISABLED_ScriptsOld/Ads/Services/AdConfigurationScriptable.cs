using UnityEngine;

[CreateAssetMenu(fileName = "AdConfiguration", menuName = "Services/Ad Configuration", order = 0)]
public class AdConfigurationScriptable : ScriptableObject
{
    [field: SerializeField]
    public string GameID
    {
        private set;
        get;
    }

    [field: SerializeField]
    public string RewardedAdUnitID
    {
        private set;
        get;
    }
}