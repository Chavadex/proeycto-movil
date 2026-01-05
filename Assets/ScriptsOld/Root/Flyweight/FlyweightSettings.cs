using UnityEngine;

[CreateAssetMenu(fileName = "FlyweightSettings", menuName = "Scriptable Objects/FlyweightSettings")]
public abstract class FlyweightSettings : ScriptableObject

{
    public FlyweightType FType = FlyweightType.RedEnemy; 
    public GameObject prefab = null;
    public abstract Flyweight Create();
    public virtual void OnGet(Flyweight flyweight)
    {
        flyweight.gameObject.SetActive(true);
    }
    public virtual void OnRealise(Flyweight flyweight)
    {
        flyweight.gameObject.SetActive(false);
    }
    public virtual void OnDispose(Flyweight flyweight)
    {
        Destroy(flyweight.gameObject);
    }
}

