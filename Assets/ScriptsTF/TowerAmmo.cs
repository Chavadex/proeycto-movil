using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerAmmo : MonoBehaviour
{
    public enum AmmoType { Red, Blue, Green }

    [Header("Strategies")]
    [SerializeField] private List<TowerAttackStrategySO> strategies;

    private Dictionary<AmmoType, ITowerAttackStrategy> _strategyMap;
    private ITowerAttackStrategy _currentStrategy;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 20;
    private int currentAmmo;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentAmmoText;

    [SerializeField] private Image ammoIconDisplay;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) _audioSource = gameObject.AddComponent<AudioSource>();

        InitializeStrategies();

        currentAmmo = maxAmmo;
        ChangeAmmoType(AmmoType.Red);
    }

    private void InitializeStrategies()
    {
        _strategyMap = new Dictionary<AmmoType, ITowerAttackStrategy>();
        foreach (var strat in strategies)
        {
            if (!_strategyMap.ContainsKey(strat.Type))
            {
                _strategyMap.Add(strat.Type, strat);
            }
        }
    }

    public void Fire(Transform firePoint, Transform target)
    {
        if (currentAmmo <= 0) return;

        _currentStrategy.Fire(firePoint, target);

        if (_currentStrategy.ShootSound != null)
            _audioSource.PlayOneShot(_currentStrategy.ShootSound);

        ConsumeAmmo();
    }

    public void ChangeAmmoType(AmmoType newType)
    {
        if (_strategyMap.ContainsKey(newType))
        {
            _currentStrategy = _strategyMap[newType];
            UpdateUI();
        }
        else
        {
            Debug.LogError($"No se encontró estrategia para {newType}");
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
        UpdateUI();
    }

    public bool HasAmmo() => currentAmmo > 0;
    public AmmoType GetAmmoType() => _currentStrategy.Type;
    public AudioClip GetAmmoSound() => _currentStrategy.ShootSound;


    private void ConsumeAmmo()
    {
        currentAmmo--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (currentAmmoText != null)
            currentAmmoText.text = $"{currentAmmo}/{maxAmmo}";

        if (ammoIconDisplay != null)
        {
            ammoIconDisplay.enabled = currentAmmo > 0;

            if (_currentStrategy != null && _currentStrategy.AmmoSprite != null)
            {
                ammoIconDisplay.sprite = _currentStrategy.AmmoSprite;

                ammoIconDisplay.color = Color.white;
            }
        }
    }

    public float GetCurrentRecoil()
    {
        if (_currentStrategy != null)
            return _currentStrategy.RecoilStrength;

        return 0.1f; // Valor por defecto
    }
}
