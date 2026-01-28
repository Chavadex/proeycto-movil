using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private RedEnemyFlyweightSettings redEnemySettings;
    [SerializeField] private RedEnemyFlyweightSettings blueEnemySettings;
    [SerializeField] private RedEnemyFlyweightSettings greenEnemySettings;

    [Header("Spawn Control")]
    [SerializeField] private bool spawnRedEnemy = true;
    [SerializeField] private bool spawnBlueEnemy = true;
    [SerializeField] private bool spawnGreenEnemy = true;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnCd = 1f;

    [Header("References")]
    [SerializeField] private WaveManager waveManager;

    private int enemiesToSpawn;
    private int spawnedEnemies;
    private float currentCd;
    private bool isSpawning;


    private void Update()
    {
        if (!isSpawning)
            return;

        if (spawnedEnemies >= enemiesToSpawn)
            return;

        currentCd -= Time.deltaTime;

        if (currentCd <= 0f)
        {
            SpawnEnemy();
            currentCd = spawnCd;
        }
    }

    public void StartSpawning(int amount)
    {
        enemiesToSpawn = amount;
        spawnedEnemies = 0;
        currentCd = 0f;
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }


    public void OnEnemyDeath()
    {
        if (waveManager == null)
        {
            Debug.LogWarning("EnemySpawner: waveManager es NULL");
            return;
        }

        waveManager.OnEnemyKilled();
    }


    public void ClearEnemies()
    {
        StopSpawning();
        FlyweightFactory.ClearAll();
    }
    private void SpawnEnemy()
    {
        var settings = GetRandomAllowedEnemy();
        if (settings == null) return;

        var enemy = FlyweightFactory.Spawn(settings);
        enemy.transform.position = spawnPoint.position;

        spawnedEnemies++;
    }

    private RedEnemyFlyweightSettings GetRandomAllowedEnemy()
    {
        var list = new System.Collections.Generic.List<RedEnemyFlyweightSettings>();

        if (spawnRedEnemy && redEnemySettings) list.Add(redEnemySettings);
        if (spawnBlueEnemy && blueEnemySettings) list.Add(blueEnemySettings);
        if (spawnGreenEnemy && greenEnemySettings) list.Add(greenEnemySettings);

        if (list.Count == 0) return null;

        return list[Random.Range(0, list.Count)];
    }
}
