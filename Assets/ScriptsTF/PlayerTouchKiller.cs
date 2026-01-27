using UnityEngine;

public class PlayerTouchKiller : MonoBehaviour
{
    private bool _isDeadly = false;

    [SerializeField] private GameObject visualEffect; 

    public void ToggleDeadlyMode(bool isActive)
    {
        _isDeadly = isActive;
        if (visualEffect != null) visualEffect.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isDeadly) return;

        if (other.CompareTag("BlueEnemy") || other.CompareTag("RedEnemy") || other.CompareTag("GreenEnemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(9999f);
                Debug.Log("Enemigo aniquilado por contacto!");
            }
        }
    }
}