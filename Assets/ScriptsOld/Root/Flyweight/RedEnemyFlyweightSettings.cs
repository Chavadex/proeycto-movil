
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
