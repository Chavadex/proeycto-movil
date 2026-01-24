using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerAmmo : MonoBehaviour
{
    public enum AmmoType
    {
        Red,
        Blue,
        Green
    }

    [Header("Ammo Settings")]
    [SerializeField] private AmmoType currentAmmoType;
    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private int currentAmmo;

    [Header("Ammo UI - Text")]
    [SerializeField] private TextMeshProUGUI currentAmmoText;

    [Header("Ammo UI - Images")]
    [SerializeField] private Image imageAmmoRed;
    [SerializeField] private Image imageAmmoBlue;
    [SerializeField] private Image imageAmmoGreen;

    void Awake()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
        UpdateAmmoImages();
    }

    // ================= GETTERS =================

    public AmmoType GetAmmoType() => currentAmmoType;
    public int GetCurrentAmmo() => currentAmmo;
    public bool HasAmmo() => currentAmmo > 0;

    // ================= USO =================

    public void ConsumeAmmo(int amount = 1)
    {
        currentAmmo -= amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        UpdateAmmoText();
    }

    // ================= PLAYER ACTIONS =================

    public void Reload()
    {
        Debug.Log("Ya recargada");
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    public void ChangeAmmoType(AmmoType newType)
    {
        currentAmmoType = newType;
        UpdateAmmoImages();
    }

    // ================= UI =================

    private void UpdateAmmoText()
    {
        if (currentAmmoText != null)
        {
            currentAmmoText.text = $"{currentAmmo}/{maxAmmo}";
        }
    }

    private void UpdateAmmoImages()
    {
        if (imageAmmoRed != null) imageAmmoRed.enabled = false;
        if (imageAmmoBlue != null) imageAmmoBlue.enabled = false;
        if (imageAmmoGreen != null) imageAmmoGreen.enabled = false;

        switch (currentAmmoType)
        {
            case AmmoType.Red:
                if (imageAmmoRed != null) imageAmmoRed.enabled = true;
                break;

            case AmmoType.Blue:
                if (imageAmmoBlue != null) imageAmmoBlue.enabled = true;
                break;

            case AmmoType.Green:
                if (imageAmmoGreen != null) imageAmmoGreen.enabled = true;
                break;
        }
    }
}


