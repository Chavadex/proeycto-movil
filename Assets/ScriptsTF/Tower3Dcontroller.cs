using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower3DController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform firePoint;
    [SerializeField] private TowerAmmo towerAmmo;

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Visuals")]
    [SerializeField] private float recoilDuration = 0.15f;

    private float fireTimer;
    private Queue<Transform> enemyQueue = new Queue<Transform>();

    private Vector3 initialHeadLocalPos;
    private Coroutine recoilCoroutine;

    private void Start()
    {
        if (turretHead != null)
            initialHeadLocalPos = turretHead.localPosition;
    }

    private void Update()
    {
        CleanEnemyQueue();

        if (enemyQueue.Count == 0) return;

        Transform target = enemyQueue.Peek();

        RotateTowardsTarget(target);

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireCooldown)
        {
            Fire(target);
            fireTimer = 0f;
        }
    }

    private void RotateTowardsTarget(Transform target)
    {
        if (turretHead == null || target == null) return;
        
        Vector3 direction = target.position - turretHead.position;
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void Fire(Transform target)
    {
        if (towerAmmo == null || !towerAmmo.HasAmmo()) return;
        if (firePoint == null || target == null) return;

        towerAmmo.Fire(firePoint, target);

        float kickStrength = towerAmmo.GetCurrentRecoil();

        if (recoilCoroutine != null) StopCoroutine(recoilCoroutine);

        recoilCoroutine = StartCoroutine(RecoilRoutine(kickStrength));
    }

    private IEnumerator RecoilRoutine(float strength)
    {
        Vector3 recoilTargetPos = initialHeadLocalPos + (Vector3.forward * strength);

        turretHead.localPosition = recoilTargetPos;

        float elapsed = 0f;
        while (elapsed < recoilDuration)
        {
            turretHead.localPosition = Vector3.Lerp(recoilTargetPos, initialHeadLocalPos, elapsed / recoilDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        turretHead.localPosition = initialHeadLocalPos;
    }

    private void CleanEnemyQueue()
    {
        while (enemyQueue.Count > 0)
        {
            Transform enemy = enemyQueue.Peek();
            if (enemy == null) { enemyQueue.Dequeue(); continue; }

            EnemyHealth health = enemy.GetComponentInParent<EnemyHealth>();
            if (health == null || health.IsDead) { enemyQueue.Dequeue(); continue; }

            break;
        }
    }

    private bool IsEnemy(Collider other)
    {
        return other.CompareTag("BlueEnemy") || other.CompareTag("RedEnemy") || other.CompareTag("GreenEnemy");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsEnemy(other)) return;
        if (!enemyQueue.Contains(other.transform)) enemyQueue.Enqueue(other.transform);
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
            if (current != enemy) newQueue.Enqueue(current);
        }
        enemyQueue = newQueue;
    }
}