using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthBarFill;

    public bool IsDead { get; private set; }

    public event Action OnDeath;

    // =========================
    // LIFECYCLE
    // =========================
    private void OnEnable()
    {
        ResetHealth(maxHealth);
    }

    private void OnDisable()
    {
        OnDeath = null;
        IsDead = false;
    }


    // =========================
    // DAMAGE
    // =========================
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // =========================
    // DEATH
    // =========================
    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();
    }

    // =========================
    // HEALTH RESET (Flyweight-friendly)
    // =========================
    public void ResetHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        IsDead = false;
        UpdateHealthBar();
    }

    // =========================
    // UI
    // =========================
    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
