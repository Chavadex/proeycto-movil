using System.Collections.Generic;
using UnityEngine;

public class Tower3DController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretHead;   // TorretaPrincipal
    [SerializeField] private Transform firePoint;    // Empty en la punta
    [SerializeField] private GameObject projectilePrefab;

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private TowerAmmo towerAmmo;

    private float fireTimer;
    private Queue<Transform> enemyQueue = new Queue<Transform>();

    private void Update()
    {
        if (enemyQueue.Count == 0) return;

        Transform target = enemyQueue.Peek();

        if (target == null)
        {
            enemyQueue.Dequeue();
            return;
        }

        RotateTowardsTarget(target);

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown)
        {
            Fire(target);
            fireTimer = 0f;
        }
    }

    private bool IsEnemy(Collider other)
    {
        return other.CompareTag("BlueEnemy") || other.CompareTag("RedEnemy");
    }

    // ================= ROTACIÓN =================

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = target.position - turretHead.position;
        direction.y = 0f; // evita que incline hacia arriba/abajo

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(
            turretHead.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // ================= DISPARO =================

    private void Fire(Transform target)
    {
        if (!towerAmmo.HasAmmo()) return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        Projectile3D proj = projectile.GetComponent<Projectile3D>();
        proj.SetTarget(target);
        proj.SetAmmoType(towerAmmo.GetAmmoType());

        towerAmmo.ConsumeAmmo();
    }


    // ================= FIFO ENEMIGOS =================

    private void OnTriggerEnter(Collider other)
    {
        if (!IsEnemy(other)) return;

        if (!enemyQueue.Contains(other.transform))
        {
            enemyQueue.Enqueue(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsEnemy(other)) return;

        RemoveEnemyFromQueue(other.transform);
    }





    private void RemoveEnemyFromQueue(Transform enemy)
    {
        Queue<Transform> newQueue = new Queue<Transform>();

        while (enemyQueue.Count > 0)
        {
            Transform current = enemyQueue.Dequeue();
            if (current != enemy)
                newQueue.Enqueue(current);
        }

        enemyQueue = newQueue;
    }
}
