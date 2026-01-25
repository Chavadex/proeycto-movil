using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthFillImage;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar enemigos por tag
        if (!other.CompareTag("BlueEnemy") || !other.CompareTag("RedEnemy") || !other.CompareTag("GreenEnemy"))
            return;

        // Intentar obtener el flyweight del enemigo
        RedEnemyFlyweight enemy = other.GetComponent<RedEnemyFlyweight>();
        if (enemy == null)
            return;

        // Quitar vida según el daño del enemigo
        TakeDamage(enemy.GetDamage());

        // Regresar enemigo a su pool (flyweight)
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
        // Aquí luego puedes:
        // - terminar partida
        // - mostrar UI de derrota
        // - pausar el juego
    }
}
