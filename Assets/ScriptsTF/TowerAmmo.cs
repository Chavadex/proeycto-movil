using TMPro;
using UnityEngine;

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

    [Header("Ammo UI")]
    [SerializeField] private TextMeshProUGUI currentAmmoText;

    void Awake()
    {
        currentAmmo = maxAmmo;
        currentAmmoText.text = currentAmmo + "/" + maxAmmo;
    }

    

    // ================= GETTERS =================

    public AmmoType GetAmmoType()
    {
        return currentAmmoType;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    // ================= USO =================

    public void ConsumeAmmo(int amount = 1)
    {
        currentAmmo -= amount;
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        currentAmmoText.text = currentAmmo + "/" + maxAmmo;

    }

    // ================= PLAYER ACTIONS =================

    public void Reload()
    {
        Debug.Log("Ya recargada");
        currentAmmo = maxAmmo;
    }

    public void ChangeAmmoType(AmmoType newType)
    {
        currentAmmoType = newType;
    }
}

