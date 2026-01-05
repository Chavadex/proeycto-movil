using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }
}
