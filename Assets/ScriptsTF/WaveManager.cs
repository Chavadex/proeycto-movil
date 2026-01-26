using UnityEngine;
using System.Collections;
using TMPro;

public class WaveManager : MonoBehaviour
{
    private GameManagerTD _gameManager;
    private CastleHealth _castleHealth;

    [Header("Wave Settings")]
    [SerializeField] private int startingEnemies = 10;
    [SerializeField] private int enemiesIncrement = 5;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("References")]
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWave;
    private int enemiesToSpawn;
    private int enemiesRemaining;

    private bool isWaitingNextWave;

    private const string WAVE_KEY = "CURRENT_WAVE";

    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManagerTD>();
        _castleHealth = FindFirstObjectByType<CastleHealth>();
        StartCoroutine(StartFirstWave());
       // LoadWave();
        //StartWave();
    }

    // =========================
    // WAVE FLOW
    // =========================

    private void StartWave()
    {

        isWaitingNextWave = false;

        enemiesToSpawn = startingEnemies + (currentWave * enemiesIncrement);
        enemiesRemaining = enemiesToSpawn;

        enemySpawner.StartSpawning(enemiesToSpawn);
        UpdateWaveUI();

        Debug.Log($"OLEADA {currentWave + 1} - Enemigos: {enemiesToSpawn}");
    }

    public void OnEnemyKilled()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0 && !isWaitingNextWave)
        {
            isWaitingNextWave = true;
            enemySpawner.StopSpawning();
            StartCoroutine(NextWaveRoutine());
        }
    }

    private IEnumerator NextWaveRoutine()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        SaveWave();
        StartWave();
    }

    // =========================
    // UI
    // =========================

    private void UpdateWaveUI()
    {
        Debug.Log("Actualizando wave");
        if (waveText != null)
            waveText.text = $"{currentWave + 1}";
    }

    // =========================
    // SAVE / LOAD
    // =========================

    private void SaveWave()
    {
        PlayerPrefs.SetInt(WAVE_KEY, currentWave);
        PlayerPrefs.Save();
    }

    private void LoadWave()
    {
        currentWave = PlayerPrefs.GetInt(WAVE_KEY, 0);
    }

    // =========================
    // RETRY / RESTART
    // =========================

    public void RestartCurrentWave()
    {
        StartCoroutine(RestartWaveRoutine());
        
    }

    private IEnumerator RestartWaveRoutine()
    {
        Debug.Log("Reiniciando oleada actual");

        isWaitingNextWave = true;

        enemySpawner.StopSpawning();
        enemySpawner.ClearEnemies();

        _gameManager.Resume();

        yield return new WaitForSecondsRealtime(timeBetweenWaves);

        isWaitingNextWave = false;
        StartWave();
        _castleHealth.RestoreHealth();
    }

    public void ResetWavesFromStart()
    {
        Debug.Log("Reiniciando oleadas desde el inicio");

        StopAllCoroutines();

        isWaitingNextWave = false;

        // Reset datos
        currentWave = 0;
        SaveWave();

        // Detener y limpiar enemigos
        enemySpawner.StopSpawning();
        enemySpawner.ClearEnemies();

        // Reset castillo
        if (_castleHealth != null)
            _castleHealth.RestoreHealth();

        // Actualizar UI
        UpdateWaveUI();

        // Iniciar primera oleada con delay normal
//        StartCoroutine(ResetAndStartRoutine());
    }

    private IEnumerator StartFirstWave()
    {
        yield return new WaitForSecondsRealtime(10);
        LoadWave();
        StartWave();
    }




}
