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

    // Variables para el Shake Optimizado
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;
    private float currentShakeTimer = 0f; // Timer para controlar la duración

    private void Start()
    {
        _GameManager = FindFirstObjectByType<GameManagerTD>();
        _waveManager = FindFirstObjectByType<WaveManager>();

        currentHealth = maxHealth;

        // Guardamos la posición inicial exacta
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

        // Llamamos al Shake
        TriggerShake();

        if (currentHealth <= 0)
            OnCastleDestroyed();
    }

    // ==========================================
    // LÓGICA DEL SHAKE (CORREGIDA "ADDITIVE")
    // ==========================================

    private void TriggerShake()
    {
        // En lugar de detener la corrutina, simplemente REINICIAMOS el tiempo.
        // Esto asegura que si llegan 10 enemigos, el tiempo se mantiene lleno
        // y el castillo sigue temblando sin cortes.
        currentShakeTimer = shakeDuration;

        // Solo iniciamos la corrutina si NO está corriendo ya.
        if (shakeCoroutine == null)
        {
            shakeCoroutine = StartCoroutine(ShakeRoutine());
        }
    }

    private IEnumerator ShakeRoutine()
    {
        // Mientras quede tiempo en el temporizador...
        while (currentShakeTimer > 0)
        {
            // Reducimos el tiempo
            currentShakeTimer -= Time.deltaTime;

            // Generamos el desplazamiento
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float z = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPosition + new Vector3(x, 0f, z);

            yield return null; // Esperamos al siguiente frame
        }

        // Al terminar todo el alboroto, regresamos a la posición original
        transform.position = originalPosition;
        shakeCoroutine = null; // Liberamos la variable para la próxima vez
    }

    // ==========================================

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

        // Importante: Aseguramos que el castillo esté en su sitio al reiniciar
        transform.position = originalPosition;

        UpdateUI();
    }
}