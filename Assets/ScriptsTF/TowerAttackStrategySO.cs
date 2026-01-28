using UnityEngine;
using static TowerAmmo;

[CreateAssetMenu(fileName = "TowerStrategy", menuName = "ScriptableObjects/TowerStrategy")]
public class TowerAttackStrategySO : ScriptableObject, ITowerAttackStrategy
{
    [Header("Configuración")]
    [SerializeField] private AmmoType ammoType;

    [SerializeField] private Sprite ammoSprite;

    [SerializeField] private AudioClip shootSound;

    [Header("Flyweight Settings")]
    [SerializeField] private ProjectileFlyweightSettings projectileSettings;

    [Header("Visual Feedback")]
    [SerializeField] private float recoilStrength = 0.2f;

    public float RecoilStrength => recoilStrength;

    public AmmoType Type => ammoType;

    public Sprite AmmoSprite => ammoSprite;

    public AudioClip ShootSound => shootSound;

    public void Fire(Transform spawnPoint, Transform target)
    {
        Flyweight fw = FlyweightFactory.Spawn(projectileSettings);

        fw.transform.position = spawnPoint.position;
        fw.transform.rotation = spawnPoint.rotation;


        Projectile3D proj = (Projectile3D)fw;
        if (proj != null)
        {
            proj.Initialize(target);
        }
    }
}