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


    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManagerTD>();
        _castleHealth = FindFirstObjectByType<CastleHealth>();

        StartCoroutine(StartFirstWave());
    }

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

    private void UpdateWaveUI()
    {
        if (waveText != null)
            waveText.text = $"{currentWave + 1}";
    }

    private void SaveWave()
    {

        if (GameRepository.Instance != null)
        {
            GameRepository.Instance.SaveWave(currentWave);
        }
    }

    private void LoadWave()
    {
        if (GameRepository.Instance != null)
        {
            currentWave = GameRepository.Instance.LoadWave();
        }
    }

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

        if (_castleHealth != null)
            _castleHealth.RestoreHealth();
    }

    public void ResetWavesFromStart()
    {
        Debug.Log("Reiniciando oleadas desde el inicio");

        StopAllCoroutines();
        isWaitingNextWave = false;

        currentWave = 0;
        SaveWave();

        enemySpawner.StopSpawning();
        enemySpawner.ClearEnemies();

        if (_castleHealth != null)
            _castleHealth.RestoreHealth();

        UpdateWaveUI();

    }

    private IEnumerator StartFirstWave()
    {
        yield return new WaitForSecondsRealtime(2f);

        LoadWave(); 
        StartWave();
    }
}