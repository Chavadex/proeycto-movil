using UnityEngine;
using static TowerAmmo;

public class Projectile3D : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;

    private AmmoType ammoType;
    private Transform target;


    
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetAmmoType(AmmoType type)
    {
        ammoType = type;
    }

    // ================= MOVIMIENTO =================

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    // ================= IMPACTO =================

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (target == null) return;

        // Evita pegarle a otra cosa
        if (!other.transform.IsChildOf(target)) return;

        if (!IsCorrectTarget(other.tag))
        {
            enemy.TakeDamage(damage * 0.25f);

            Destroy(gameObject);
            return;
        }

       // EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }


    // ================= COLOR LOGIC =================

    private bool IsCorrectTarget(string enemyTag)
    {
        if (ammoType == AmmoType.Blue && enemyTag == "BlueEnemy")
            return true;

        if (ammoType == AmmoType.Red && enemyTag == "RedEnemy")
            return true;

        return false;
    }

}
