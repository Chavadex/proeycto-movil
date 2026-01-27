using UnityEngine;

[CreateAssetMenu(
    fileName = "RedEnemyFlyweightSettings",
    menuName = "ScriptableObjects/FlyweightSettings/RedEnemy"
)]
public class RedEnemyFlyweightSettings : FlyweightSettings
{
    [Header("Stats (Shared)")]
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public float Speed { get; private set; } = 2f;

    [field: SerializeField] public int Coins { get; private set; } = 2;

    [Header("Prefabs")]
    [SerializeField] private GameObject visualPrefab;

    public override Flyweight Create()
    {
        var go = Instantiate(prefab);

        if (visualPrefab != null && go.transform.childCount > 0)
        {
            var visual = Instantiate(
                visualPrefab,
                go.transform.GetChild(0)
            );
            visual.transform.localPosition = Vector3.zero;
        }

        var enemy = go.GetOrAddComponent<RedEnemyFlyweight>();
        enemy.flyweightSettings = this; 

        return enemy;
    }
}

