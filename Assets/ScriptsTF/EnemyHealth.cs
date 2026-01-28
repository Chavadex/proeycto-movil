using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    [SerializeField] private Image healthBarFill;

    [Header("Visual Feedback")]
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Color damageColor = Color.red;

    [Header("References")]
    [SerializeField] private Renderer enemyRenderer;

    private Color _originalColor = Color.white;

    private Coroutine _flashCoroutine;

    private static readonly int BaseColorID = Shader.PropertyToID("_BaseColor"); 
    private static readonly int ColorID = Shader.PropertyToID("_Color");        

    public bool IsDead { get; private set; }
    public event Action OnDeath;

    private void Awake()
    {
        if (enemyRenderer == null)
            enemyRenderer = GetComponentInChildren<Renderer>();

        if (enemyRenderer != null)
        {
            if (enemyRenderer.material.HasProperty(BaseColorID))
            {
                _originalColor = enemyRenderer.material.GetColor(BaseColorID);
            }
            else if (enemyRenderer.material.HasProperty(ColorID))
            {
                _originalColor = enemyRenderer.material.GetColor(ColorID);
            }
        }
    }

    private void OnDisable()
    {
        OnDeath = null;
        IsDead = false;
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        ResetColor();
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        FlashDamage();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void FlashDamage()
    {
        if (enemyRenderer == null) return;
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        SetMaterialColor(damageColor);

        yield return new WaitForSeconds(flashDuration);

        ResetColor();
        _flashCoroutine = null;
    }

    private void ResetColor()
    {
        if (enemyRenderer != null)
        {
            SetMaterialColor(_originalColor);
        }
    }

    private void SetMaterialColor(Color color)
    {
        if (enemyRenderer == null) return;

        if (enemyRenderer.material.HasProperty(BaseColorID))
        {
            enemyRenderer.material.SetColor(BaseColorID, color);
        }
        else if (enemyRenderer.material.HasProperty(ColorID))
        {
            enemyRenderer.material.SetColor(ColorID, color);
        }
    }

    private void Die()
    {
        if (IsDead) return;
        IsDead = true;
        OnDeath?.Invoke();
    }

    public void ResetHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        IsDead = false;

        ResetColor();

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}