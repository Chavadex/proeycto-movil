using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static TowerAmmo;

public class PlayerAmmoInteraction : MonoBehaviour
{
    [Header("Player Ammo")]
    [SerializeField] private AmmoType carriedAmmoType;
    [SerializeField] private bool hasAmmo = false;

    [Header("UI")]
    [SerializeField] private Image ammoIcon;

    private void Start()
    {
        UpdateAmmoUI();
    }

    // ================= TRIGGERS =================

    private void OnTriggerEnter(Collider other)
    {
        // -------- RECOGER MUNICIÓN --------
        if (other.CompareTag("AmmoSupplie"))
        {
            Debug.Log("Recogiste ammo");
            PickAmmo(other);
        }

        // -------- RECARGAR TORRETA --------
        if (other.CompareTag("Torreta"))
        {
            Debug.Log("Torreta");
            ReloadTower(other);
        }
    }

    // ================= LOGIC =================

    private void PickAmmo(Collider ammoSupply)
    {
        if (hasAmmo) return; // No puede cargar más de una

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

    // ================= UI =================

    private void UpdateAmmoUI()
    {
        if (ammoIcon == null) return;

        ammoIcon.enabled = hasAmmo;

        if (!hasAmmo) return;

        switch (carriedAmmoType)
        {
            case AmmoType.Red:
                ammoIcon.color = Color.red;
                break;
            case AmmoType.Blue:
                ammoIcon.color = Color.blue;
                break;
            case AmmoType.Green:
                ammoIcon.color = Color.green;
                break;
        }
    }
}
