using UnityEngine;
using static TowerAmmo;

public class Projectile3D : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float wrongColorMultiplier = 0.25f;

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
        if (target == null) return;

        // Solo colisiona con su target
        if (!other.transform.IsChildOf(target)) return;

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null) return;

        float finalDamage = IsCorrectTarget(other.tag)
            ? damage
            : damage * wrongColorMultiplier;

        enemy.TakeDamage(finalDamage);
        Debug.Log("Le diste");
        Destroy(gameObject);
    }

    // ================= COLOR LOGIC =================
    private bool IsCorrectTarget(string enemyTag)
    {
        return (ammoType == AmmoType.Blue && enemyTag == "BlueEnemy") ||
               (ammoType == AmmoType.Red && enemyTag == "RedEnemy");
    }
}
