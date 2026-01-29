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

    [Header("Audio")]
    [SerializeField] private AudioSource bocina;
    [SerializeField] private AudioClip finishedWave;
    [SerializeField] private AudioClip kill;

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

        if (enemySpawner != null)
            enemySpawner.StartSpawning(enemiesToSpawn);
        else
            Debug.LogError("WaveManager: enemySpawner es null, no se pueden spawneer enemigos");
        
        UpdateWaveUI();

        Debug.Log($"OLEADA {currentWave + 1} - Enemigos: {enemiesToSpawn}");
    }

    public void OnEnemyKilled()
    {
        enemiesRemaining--;
        bocina.PlayOneShot(kill);

        if (enemiesRemaining <= 0 && !isWaitingNextWave)
        {
            isWaitingNextWave = true;
            if (enemySpawner != null)
                enemySpawner.StopSpawning();
            StartCoroutine(NextWaveRoutine());
            bocina.PlayOneShot(finishedWave);
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

        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
            enemySpawner.ClearEnemies();
        }

        if (_gameManager != null)
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

        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
            enemySpawner.ClearEnemies();
        }

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