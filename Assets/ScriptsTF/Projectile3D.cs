using UnityEngine;
using static TowerAmmo;

public class Projectile3D : Flyweight
{
    private ProjectileFlyweightSettings _settings;
    private Transform _target;
    private float _wrongColorMultiplier = 0.25f;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 5f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Initialize(Transform newTarget)
    {
        if (_settings == null)
        {
            _settings = (ProjectileFlyweightSettings)flyweightSettings;
        }

        _target = newTarget;
    }

    private void Update()
    {
        if (_settings == null) return;

        if (_target == null || !_target.gameObject.activeInHierarchy)
        {
            ReturnToPool();
            return;
        }

        Vector3 direction = (_target.position - transform.position).normalized;

        transform.position += direction * _settings.Speed * Time.deltaTime;
        transform.forward = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_target == null || _settings == null) return;

        if (!other.transform.IsChildOf(_target) && other.transform != _target) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            float finalDamage = IsCorrectTarget(other.tag)
                ? _settings.Damage
                : _settings.Damage * _wrongColorMultiplier;

            enemy.TakeDamage(finalDamage);
        }

        ReturnToPool();
    }

    private bool IsCorrectTarget(string enemyTag)
    {
        if (_settings == null) return false;

        AmmoType type = _settings.AmmoColor;
        return (type == AmmoType.Blue && enemyTag == "BlueEnemy") ||
               (type == AmmoType.Red && enemyTag == "RedEnemy") ||
               (type == AmmoType.Green && enemyTag == "GreenEnemy");
    }

    private void ReturnToPool()
    {
        FlyweightFactory.Release(this);
    }
}
