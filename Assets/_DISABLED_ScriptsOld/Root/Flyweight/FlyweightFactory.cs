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
    private HashSet<Flyweight> _activeFlyweights = new();

    public static Flyweight Spawn(FlyweightSettings settings)
    {
        var fw = Instance.GetPool(settings)?.Get();
        Instance._activeFlyweights.Add(fw);
        return fw;
    }

    public static void Release(Flyweight flyweight)
    {
        if (flyweight == null) return;

        Instance._activeFlyweights.Remove(flyweight);
        Instance.GetPool(flyweight.flyweightSettings)?.Release(flyweight);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private IObjectPool<Flyweight> GetPool(FlyweightSettings settings)
    {
        if (_objectPools.TryGetValue(settings.FType, out var pool))
            return pool;

        pool = new ObjectPool<Flyweight>(
            settings.Create,
            fw => {
                settings.OnGet(fw);
                _activeFlyweights.Add(fw);
            },
            fw => {
                settings.OnRealise(fw);
                _activeFlyweights.Remove(fw);
            },
            settings.OnDispose,
            _collectionCheck,
            _defaultCapacity,
            _MaxCapacity
        );

        _objectPools.Add(settings.FType, pool);
        return pool;
    }

    public static void ClearAll()
    {
        var copy = new List<Flyweight>(Instance._activeFlyweights);

        foreach (var fw in copy)
        {
            Release(fw);
        }

        Instance._activeFlyweights.Clear();
        Debug.Log("FlyweightFactory: Todos los enemigos limpiados");
    }

    public static void NukeEnemies()
    {
        var activeEnemies = new List<Flyweight>(Instance._activeFlyweights);

        foreach (var fw in activeEnemies)
        {
            var health = fw.GetComponent<EnemyHealth>();
            if (health != null && !health.IsDead)
            {

                health.TakeDamage(99999f);
            }
        }
        Debug.Log("NUKE: Todos los enemigos eliminados.");
    }

}
