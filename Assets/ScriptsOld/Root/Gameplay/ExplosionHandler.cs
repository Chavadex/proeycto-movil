using System.Security.Cryptography;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{

    [SerializeField] private float radius = 10;
    [SerializeField] private ParticleFlyweightSettings flyweightSettings;
    [SerializeField] private float cd = 0.5f;
    private float currentCd = 0f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && currentCd <= 0f)
        {
            var randonNumberInSphere = UnityEngine.Random.insideUnitSphere * radius;
            randonNumberInSphere += transform.position;
            var particles = FlyweightFactory.Spawn(flyweightSettings);
            if (particles == null) return;
            particles.transform.position = randonNumberInSphere;
            currentCd = cd;
        }
        currentCd -= Time.deltaTime;
    }

}
