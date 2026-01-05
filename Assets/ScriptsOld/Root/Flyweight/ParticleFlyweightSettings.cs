using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleFlyweightSettings", menuName = "Scriptable Objects/ParticleFlyweightSettings")]
public class ParticleFlyweightSettings : FlyweightSettings
{
    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = name;

        var particleFlyweight = go.GetOrAddComponent<ParticleFlyweight>();
        //var particleFlyweight = go.GetComponent<ParticleFlyweight>();
        //if(particleFlyweight == null )
        //    particleFlyweight = go.AddComponent<ParticleFlyweight>();

        particleFlyweight.flyweightSettings = this;

        return particleFlyweight;
    }
}

