using UnityEngine;

public class ParticleFlyweight : Flyweight
{
    private void OnDisable()
    {
        FlyweightFactory.Release(this);
    }
}
