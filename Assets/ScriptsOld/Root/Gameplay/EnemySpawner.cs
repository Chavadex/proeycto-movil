using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RedEnemyFlyweightSettings redEnemySettings;
    [SerializeField] private RedEnemyFlyweightSettings blueEnemySettings;
    [SerializeField] private RedEnemyFlyweightSettings greenEnemySettings;

    [SerializeField] private float spawnCd = 1f;
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private Transform spawnPoint3;
    private float currentCd = 0f;

    private void Update()
    {
        if(currentCd <= 0)
        {
            int enemySpawned = Random.Range(1, 4);

            switch (enemySpawned)
            {
                case 1:
                    var enemy = FlyweightFactory.Spawn(redEnemySettings);
                    if (enemy == null) return;
                    int randPos = Random.Range(1, 4);
                    switch(randPos)
                    {
                        case 1:
                            enemy.transform.position = spawnPoint1.position;
                            break;
                        case 2:
                            enemy.transform.position = spawnPoint2.position;
                            break;
                        case 3:
                            enemy.transform.position = spawnPoint3.position;
                            break;
                    }

                    currentCd = spawnCd;


                    break;
                case 2:
                    var enemy2 = FlyweightFactory.Spawn(blueEnemySettings);
                    if (enemy2 == null) return;
                    int randPos2 = Random.Range(1, 4);
                    switch (randPos2)
                    {
                        case 1:
                            enemy2.transform.position = spawnPoint1.position;
                            break;
                        case 2:
                            enemy2.transform.position = spawnPoint2.position;
                            break;
                        case 3:
                            enemy2.transform.position = spawnPoint3.position;
                            break;
                    }

                    currentCd = spawnCd;
                    break;
                case 3:
                    var enemy3 = FlyweightFactory.Spawn(greenEnemySettings);
                    if (enemy3 == null) return;
                    int randPos3 = Random.Range(1, 4);
                    switch (randPos3)
                    {
                        case 1:
                            enemy3.transform.position = spawnPoint1.position;
                            break;
                        case 2:
                            enemy3.transform.position = spawnPoint2.position;
                            break;
                        case 3:
                            enemy3.transform.position = spawnPoint3.position;
                            break;
                    }

                    currentCd = spawnCd;


                    break;
            }
            /*
            var enemy = FlyweightFactory.Spawn(redEnemySettings);
            if (enemy == null) return;
            enemy.transform.position = spawnPoint1.position;
            
            currentCd = spawnCd;*/
        }
        currentCd -= Time.deltaTime;
    }
}
