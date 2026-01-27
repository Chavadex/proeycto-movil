using System.Collections;
using UnityEngine;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    [Header("UI References (Textos Contador)")]
    [SerializeField] private TextMeshProUGUI nukeCountText;
    [SerializeField] private TextMeshProUGUI speedCountText;
    [SerializeField] private TextMeshProUGUI healCountText;
    [SerializeField] private TextMeshProUGUI instaKillCountText;

    [Header("Inventory (Cantidades Iniciales / Default)")]
    public int defaultNukeAmount = 1;
    public int defaultSpeedAmount = 1;
    public int defaultHealAmount = 1;
    public int defaultInstaKillAmount = 1;

    private int nukeAmount;
    private int speedAmount;
    private int healAmount;
    private int instaKillAmount;

    [Header("Settings")]
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float speedDuration = 20f;
    [SerializeField] private float instaKillDuration = 10f;

    private ClickToMove _playerMovement;
    private CastleHealth _castleHealth;
    private PlayerTouchKiller _playerTouchKiller; 

    private const string KEY_NUKE = "PWR_NUKE";
    private const string KEY_SPEED = "PWR_SPEED";
    private const string KEY_HEAL = "PWR_HEAL";
    private const string KEY_INSTAKILL = "PWR_INSTAKILL";

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _playerMovement = FindFirstObjectByType<ClickToMove>();
        _castleHealth = FindFirstObjectByType<CastleHealth>();
        _playerTouchKiller = FindFirstObjectByType<PlayerTouchKiller>();

        LoadInventory();

        UpdateAllUI();
    }

    public void ActivateNuke()
    {
        if (nukeAmount <= 0) return;

        FlyweightFactory.NukeEnemies();

        nukeAmount--;
        SaveInventory();
        UpdateAllUI();
        Debug.Log("PowerUp: Nuke Activado. Restantes: " + nukeAmount);
    }

    public void ActivateSpeed()
    {
        if (speedAmount <= 0) return;
        if (_playerMovement == null) return;

        StartCoroutine(SpeedRoutine());

        speedAmount--;
        SaveInventory(); 
        UpdateAllUI();
    }

    public void ActivateHeal()
    {
        if (healAmount <= 0) return;
        if (_castleHealth == null) return;

        _castleHealth.RestoreHealth();

        healAmount--;
        SaveInventory(); 
        UpdateAllUI();
    }

    public void ActivateInstaKill()
    {
        if (instaKillAmount <= 0) return;
        if (_playerTouchKiller == null) return;

        StartCoroutine(InstaKillRoutine());

        instaKillAmount--;
        SaveInventory(); 
        UpdateAllUI();
    }


    public void AddPowerUp(string type, int amount)
    {
        switch (type)
        {
            case "Nuke": nukeAmount += amount; break;
            case "Speed": speedAmount += amount; break;
            case "Heal": healAmount += amount; break;
            case "InstaKill": instaKillAmount += amount; break;
        }

        SaveInventory();
        UpdateAllUI();
    }

    private void SaveInventory()
    {
        GameRepository.Instance.SavePowerUpCount(KEY_NUKE, nukeAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_SPEED, speedAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_HEAL, healAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_INSTAKILL, instaKillAmount);
    }

    private void LoadInventory()
    {
        nukeAmount = GameRepository.Instance.LoadPowerUpCount(KEY_NUKE, defaultNukeAmount);
        speedAmount = GameRepository.Instance.LoadPowerUpCount(KEY_SPEED, defaultSpeedAmount);
        healAmount = GameRepository.Instance.LoadPowerUpCount(KEY_HEAL, defaultHealAmount);
        instaKillAmount = GameRepository.Instance.LoadPowerUpCount(KEY_INSTAKILL, defaultInstaKillAmount);
    }

    public void ResetAllData()
    {
        nukeAmount = defaultNukeAmount;
        speedAmount = defaultSpeedAmount;
        healAmount = defaultHealAmount;
        instaKillAmount = defaultInstaKillAmount;
        SaveInventory();
        UpdateAllUI();
        Debug.Log("Datos de PowerUps reiniciados");
    }

    private IEnumerator SpeedRoutine()
    {
        float originalSpeed = _playerMovement.moveSpeed;
        _playerMovement.moveSpeed *= speedMultiplier;
        yield return new WaitForSeconds(speedDuration);
        _playerMovement.moveSpeed = originalSpeed;
    }

    private IEnumerator InstaKillRoutine()
    {
        _playerTouchKiller.ToggleDeadlyMode(true);
        yield return new WaitForSeconds(instaKillDuration);
        _playerTouchKiller.ToggleDeadlyMode(false);
    }

    public void UpdateAllUI()
    {
        if (nukeCountText) nukeCountText.text = nukeAmount.ToString();
        if (speedCountText) speedCountText.text = speedAmount.ToString();
        if (healCountText) healCountText.text = healAmount.ToString();
        if (instaKillCountText) instaKillCountText.text = instaKillAmount.ToString();
    }

    public int GetPowerUpCount(string type)
    {
        switch (type)
        {
            case "Nuke": return nukeAmount;
            case "Speed": return speedAmount;
            case "Heal": return healAmount;
            case "InstaKill": return instaKillAmount;
            default: return 0;
        }
    }
}
