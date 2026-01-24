using UnityEngine;
using static TowerAmmo;

public class AmmoSupply : MonoBehaviour
{
    [SerializeField] private AmmoType ammoType;

    public AmmoType GetAmmoType()
    {
        return ammoType;
    }
}
