using UnityEngine;
using UnityEngine.Events;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private string deathTag;
    [SerializeField] private UnityEvent onDeath;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(deathTag))
        {
            onDeath?.Invoke();
        }
    }
}
