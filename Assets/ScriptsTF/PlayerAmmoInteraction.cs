using UnityEngine;
using UnityEngine.UI;
using static TowerAmmo;

public class PlayerAmmoInteraction : MonoBehaviour
{
    [Header("Player Ammo")]
    [SerializeField] private AmmoType carriedAmmoType;
    [SerializeField] private bool hasAmmo = false;

    [Header("UI References")]
    [SerializeField] private Image ammoIcon;

    [Header("Ammo Sprites (Arrastra tus imágenes aquí)")]
    [SerializeField] private Sprite redAmmoSprite;
    [SerializeField] private Sprite blueAmmoSprite;
    [SerializeField] private Sprite greenAmmoSprite; 

    private void Start()
    {
        UpdateAmmoUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoSupplie"))
        {
            PickAmmo(other);
        }

        if (other.CompareTag("Torreta"))
        {
            ReloadTower(other);
        }
    }

    private void PickAmmo(Collider ammoSupply)
    {
        if (hasAmmo) return; 

        AmmoSupply supply = ammoSupply.GetComponent<AmmoSupply>();
        if (supply == null) return;

        carriedAmmoType = supply.GetAmmoType();
        hasAmmo = true;

        UpdateAmmoUI();
    }

    private void ReloadTower(Collider towerCollider)
    {
        if (!hasAmmo) return;

        TowerAmmo towerAmmo = towerCollider.GetComponentInParent<TowerAmmo>();
        if (towerAmmo == null) return;

        towerAmmo.ChangeAmmoType(carriedAmmoType);
        towerAmmo.Reload();

        hasAmmo = false;
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoIcon == null) return;

        ammoIcon.enabled = hasAmmo;

        if (!hasAmmo) return;

        ammoIcon.color = Color.white;

        switch (carriedAmmoType)
        {
            case AmmoType.Red:
                ammoIcon.sprite = redAmmoSprite;
                break;
            case AmmoType.Blue:
                ammoIcon.sprite = blueAmmoSprite;
                break;
            case AmmoType.Green:
                ammoIcon.sprite = greenAmmoSprite;
                break;
        }
    }
}