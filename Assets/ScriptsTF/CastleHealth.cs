using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    private GameManagerTD _GameManager;
    private WaveManager _waveManager;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthFillImage;

    [Header("Visual Feedback")]
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float shakeMagnitude = 0.15f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;
    private float currentShakeTimer = 0f;

    private void Start()
    {
        _GameManager = FindFirstObjectByType<GameManagerTD>();
        _waveManager = FindFirstObjectByType<WaveManager>();

        currentHealth = maxHealth;

        originalPosition = transform.position;

        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (
            !other.CompareTag("BlueEnemy") &&
            !other.CompareTag("RedEnemy") &&
            !other.CompareTag("GreenEnemy")
        )
            return;

        RedEnemyFlyweight enemy = other.GetComponent<RedEnemyFlyweight>();

        if (enemy == null)
            return;

        TakeDamage(enemy.GetDamage());

        if (_waveManager != null)
        {
            _waveManager.OnEnemyKilled();
        }

        FlyweightFactory.Release(enemy);
    }


    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateUI();

        TriggerShake();

        if (currentHealth <= 0)
            OnCastleDestroyed();
    }


    private void TriggerShake()
    {

        currentShakeTimer = shakeDuration;

        if (shakeCoroutine == null)
        {
            shakeCoroutine = StartCoroutine(ShakeRoutine());
        }
    }

    private IEnumerator ShakeRoutine()
    {
        while (currentShakeTimer > 0)
        {
            currentShakeTimer -= Time.deltaTime;

            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float z = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPosition + new Vector3(x, 0f, z);

            yield return null;
        }

        transform.position = originalPosition;
        shakeCoroutine = null;
    }

    private void UpdateUI()
    {
        if (healthFillImage != null)
            healthFillImage.fillAmount = currentHealth / maxHealth;
    }

    private void OnCastleDestroyed()
    {
        Debug.Log("El castillo fue destruido ");

        if (_GameManager != null)
            _GameManager.CheckIfFirstDead();

        GetComponent<BoxCollider>().enabled = false;
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        GetComponent<BoxCollider>().enabled = true;

        transform.position = originalPosition;

        UpdateUI();
    }
}