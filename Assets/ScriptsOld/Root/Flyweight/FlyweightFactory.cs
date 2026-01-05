using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlyweightFactory : MonoBehaviour
{
    public static FlyweightFactory Instance { get; private set; }
    [SerializeField] private bool _collectionCheck = true;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _MaxCapacity = 50;
    private Dictionary<FlyweightType, IObjectPool<Flyweight>> _objectPools = new();

    public static Flyweight Spawn(FlyweightSettings flyweightSettings) => Instance.GetPool(flyweightSettings)?.Get();
    public static void Release(Flyweight flyweight) => Instance.GetPool(flyweight.flyweightSettings)?.Release(flyweight);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private IObjectPool<Flyweight> GetPool(FlyweightSettings flyweightsettings)
    {
        if (_objectPools.TryGetValue(flyweightsettings.FType, out var pool))
        {
            return pool;
        }
        pool = new ObjectPool<Flyweight>(
            flyweightsettings.Create,
            flyweightsettings.OnGet,
            flyweightsettings.OnRealise,
            flyweightsettings.OnDispose,
            _collectionCheck,
            _defaultCapacity,
            _MaxCapacity);

        _objectPools.Add(flyweightsettings.FType, pool);

        return pool;
    }
}

