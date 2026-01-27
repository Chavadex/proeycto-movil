using UnityEngine;

public class ExplosionFlyweight : Flyweight
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void OnSpawn()
    {
        if (_particleSystem != null)
        {
            _particleSystem.Clear();
            _particleSystem.Play();
        }
    }

    private void OnEnable()
    {
        if (_particleSystem != null)
        {
            _particleSystem.Play();
        }
    }
}