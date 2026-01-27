using UnityEngine;
using static TowerAmmo; 

[CreateAssetMenu(fileName = "ProjectileSettings", menuName = "ScriptableObjects/FlyweightSettings/Projectile")]
public class ProjectileFlyweightSettings : FlyweightSettings
{
    [Header("Projectile Stats")]
    [field: SerializeField] public float Speed { get; private set; } = 10f;
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public AmmoType AmmoColor { get; private set; } 

    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        var proj = go.GetOrAddComponent<Projectile3D>();
        proj.flyweightSettings = this;
        return proj;
    }

    public override void OnGet(Flyweight flyweight)
    {
        base.OnGet(flyweight);
    }
}