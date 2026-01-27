using UnityEngine;
using static TowerAmmo;

public interface ITowerAttackStrategy
{
    AmmoType Type { get; }

    Sprite AmmoSprite { get; }

    AudioClip ShootSound { get; }
    void Fire(Transform spawnPoint, Transform target);
}