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
        enemy.flyweightSettings = this; // referencia compartida

        return enemy;
    }
}



///////////////////////////////////////////////////////////////////////

/*
using UnityEngine;

[CreateAssetMenu(fileName ="RedEnemyFlyWeightSettings", menuName ="ScriptableObjects/FlyWeightSettings/RedEnemy")]

public class RedEnemyFlyweightSettings:FlyweightSettings
{
    [field:SerializeField] public float Speed { private set; get; } = 2f;
    [field:SerializeField] public int Damage { private set; get; } = 10;
    [field:SerializeField] public int Health { private set; get; } = 100;
    [field:SerializeField] public GameObject visualPrefab { private set; get; }
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        var vgo = Instantiate(visualPrefab, Vector3.zero,
            visualPrefab.transform.rotation, go.transform.GetChild(0).transform);
        vgo.transform.localPosition = Vector3.zero;
        RedEnemyFlyweight redEnemyFlyweight = go.GetOrAddComponent<RedEnemyFlyweight>();
        redEnemyFlyweight.flyweightSettings = this; //Ref
        return redEnemyFlyweight;
    }
}
*/