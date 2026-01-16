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

    [Header("Spawn Limits")]
    [SerializeField] private int maxEnemies = 20;
    [SerializeField] private int currentEnemies = 0;

    [Header("Cooldown")]
    [SerializeField] private float spawnCd = 1f;
    private float currentCd;

    private void Update()
    {
        if (currentEnemies >= maxEnemies)
            return;

        if (currentCd <= 0f)
        {
            TrySpawnEnemy();
            currentCd = spawnCd;
        }

        currentCd -= Time.deltaTime;
    }

    // =========================
    // SPAWN LOGIC
    // =========================
    private void TrySpawnEnemy()
    {
        RedEnemyFlyweightSettings selectedSettings = GetRandomAllowedEnemy();

        if (selectedSettings == null)
            return;

        var enemy = FlyweightFactory.Spawn(selectedSettings);
        if (enemy == null)
            return;

        enemy.transform.position = spawnPoint.position;
        currentEnemies++;
    }

    // =========================
    // ENEMY SELECTION
    // =========================
    private RedEnemyFlyweightSettings GetRandomAllowedEnemy()
    {
        var available = new System.Collections.Generic.List<RedEnemyFlyweightSettings>();

        if (spawnRedEnemy && redEnemySettings != null)
            available.Add(redEnemySettings);

        if (spawnBlueEnemy && blueEnemySettings != null)
            available.Add(blueEnemySettings);

        if (spawnGreenEnemy && greenEnemySettings != null)
            available.Add(greenEnemySettings);

        if (available.Count == 0)
            return null;

        int index = Random.Range(0, available.Count);
        return available[index];
    }

    // =========================
    // PUBLIC API
    // =========================
    public void OnEnemyDeath()
    {
        currentEnemies--;
    }
}
