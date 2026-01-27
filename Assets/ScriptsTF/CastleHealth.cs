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

    private void Start()
    {
        _GameManager = FindFirstObjectByType<GameManagerTD>();

        _waveManager = FindFirstObjectByType<WaveManager>();

        currentHealth = maxHealth;
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

        if (currentHealth <= 0)
            OnCastleDestroyed();
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

        UpdateUI();
    }
}