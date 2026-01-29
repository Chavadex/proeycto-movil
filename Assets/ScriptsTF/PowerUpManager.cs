using System.Collections;
using UnityEngine;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

    [Header("Visual Effects")]
    [SerializeField] private NukeVisualEffect nukeEffect;
    [SerializeField] private TrailRenderer playerSpeedTrail;
    [SerializeField] private ParticleSystem healParticles; // 1. ARRASTRA AQUI TU SISTEMA DE PARTICULAS DE CURACION

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

    [Header("Audios")]
    [SerializeField] private AudioSource bocina;
    [SerializeField] private AudioClip healing;
    [SerializeField] private AudioClip nuke;
    [SerializeField] private AudioClip instaKill;
    [SerializeField] private AudioClip speedUp;

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

        // Asegurar que las partículas empiecen apagadas
        if (healParticles != null) healParticles.gameObject.SetActive(false);

        LoadInventory();
        UpdateAllUI();
    }

    public void ActivateNuke()
    {
        if (nukeAmount <= 0) return;

        FlyweightFactory.NukeEnemies();
        bocina.PlayOneShot(nuke);

        nukeAmount--;
        SaveInventory();
        UpdateAllUI();
        if (nukeEffect != null)
        {
            nukeEffect.PlayNukeEffect();
        }
        Debug.Log("PowerUp: Nuke Activado. Restantes: " + nukeAmount);
    }

    public void ActivateSpeed()
    {
        if (speedAmount <= 0) return;
        if (_playerMovement == null) return;

        StartCoroutine(SpeedRoutine());
        StartCoroutine(SpeedRoutine2());
        bocina.PlayOneShot(speedUp);
        speedAmount--;
        SaveInventory();
        UpdateAllUI();
    }

    // Rutina visual del Speed (Trail)
    private IEnumerator SpeedRoutine2()
    {
        if (playerSpeedTrail != null) playerSpeedTrail.emitting = true;

        yield return new WaitForSeconds(speedDuration); // Usamos speedDuration para que coincida

        if (playerSpeedTrail != null) playerSpeedTrail.emitting = false;
    }

    public void ActivateHeal()
    {
        if (healAmount <= 0) return;
        if (_castleHealth == null) return;

        _castleHealth.RestoreHealth();
        bocina.PlayOneShot(healing);

        // 2. ACTIVAMOS LA RUTINA VISUAL DE CURACION
        if (healParticles != null)
        {
            StartCoroutine(HealVisualRoutine());
        }

        healAmount--;
        SaveInventory();
        UpdateAllUI();
    }

    // 3. CORRUTINA PARA ACTIVAR Y DESACTIVAR PARTICULAS DE HEAL
    private IEnumerator HealVisualRoutine()
    {
        healParticles.gameObject.SetActive(true);
        healParticles.Play();

        // Esperamos lo que duren las partículas (ej. 2 segundos)
        yield return new WaitForSeconds(2f);

        healParticles.Stop();
        healParticles.gameObject.SetActive(false);
    }

    public void ActivateInstaKill()
    {
        if (instaKillAmount <= 0) return;
        if (_playerTouchKiller == null) return;

        StartCoroutine(InstaKillRoutine());
        bocina.PlayOneShot(instaKill);

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
        if (GameRepository.Instance == null)
        {
            Debug.LogError("PowerUpManager: GameRepository.Instance es null, no se pueden guardar datos");
            return;
        }
        GameRepository.Instance.SavePowerUpCount(KEY_NUKE, nukeAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_SPEED, speedAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_HEAL, healAmount);
        GameRepository.Instance.SavePowerUpCount(KEY_INSTAKILL, instaKillAmount);
    }

    private void LoadInventory()
    {
        if (GameRepository.Instance == null)
        {
            Debug.LogError("PowerUpManager: GameRepository.Instance es null, usando valores por defecto");
            nukeAmount = defaultNukeAmount;
            speedAmount = defaultSpeedAmount;
            healAmount = defaultHealAmount;
            instaKillAmount = defaultInstaKillAmount;
            return;
        }
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

    // 4. LOGICA DE DIOS (BLANCO) MODIFICADA
    private IEnumerator InstaKillRoutine()
    {
        if (_playerTouchKiller == null) yield break;

        _playerTouchKiller.ToggleDeadlyMode(true);

        // --- CAMBIO DE COLOR VISUAL ---
        Renderer playerRenderer = _playerMovement.GetComponentInChildren<Renderer>(); // Buscamos el renderer del modelo
        Color originalColor = Color.white;
        Color originalEmission = Color.black;

        if (playerRenderer != null)
        {
            // Guardamos color original
            originalColor = playerRenderer.material.color;

            // Intentamos guardar la emisión si el shader lo permite, si no, default negro
            if (playerRenderer.material.HasProperty("_EmissionColor"))
                originalEmission = playerRenderer.material.GetColor("_EmissionColor");

            // PONEMOS MODO DIOS (Blanco brillante)
            playerRenderer.material.color = Color.white;

            // Activamos emisión para que brille
            playerRenderer.material.EnableKeyword("_EMISSION");
            playerRenderer.material.SetColor("_EmissionColor", new Color(0.8f, 0.8f, 0.8f)); // Un blanco brillante
        }
        // -----------------------------

        yield return new WaitForSeconds(instaKillDuration);

        // --- RESTAURAR COLOR VISUAL ---
        if (playerRenderer != null)
        {
            playerRenderer.material.color = originalColor;
            playerRenderer.material.SetColor("_EmissionColor", originalEmission);
        }
        // -----------------------------

        if (_playerTouchKiller != null)
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