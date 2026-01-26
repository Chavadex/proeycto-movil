using UnityEngine;

public class RedEnemyFlyweight : Flyweight
{
    private EnemySpawner _cachedSpawner;

    private RedEnemyFlyweightSettings _settings;
    private EnemyHealth _enemyHealth;

    private EnemyState _currentState;
    private Vector3 _moveDirection;

    private enum EnemyState
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Attacking
    }

    private void OnEnable()
    {
        if (_settings == null)
            _settings = (RedEnemyFlyweightSettings)flyweightSettings;

        _enemyHealth = GetComponent<EnemyHealth>();

        _enemyHealth.OnDeath -= OnDeath; // limpia antes
        _enemyHealth.ResetHealth(_settings.MaxHealth);
        _enemyHealth.OnDeath += OnDeath;

        SetState(EnemyState.MoveRight);
        if (_cachedSpawner == null)
            _cachedSpawner = FindFirstObjectByType<EnemySpawner>();

    }


    private void OnDisable()
    {
        if (_enemyHealth != null)
            _enemyHealth.OnDeath -= OnDeath;
    }

    private void Update()
    {
        HandleState();
    }

    // =========================
    // STATE LOGIC
    // =========================
    private void HandleState()
    {
        if (_currentState != EnemyState.Attacking)
            Move();
    }

    // =========================
    // MOVEMENT
    // =========================
    private void Move()
    {
        transform.position += _moveDirection * _settings.Speed * Time.deltaTime;
    }

    private void SetState(EnemyState newState)
    {
        _currentState = newState;

        _moveDirection = newState switch
        {
            EnemyState.MoveUp => Vector3.forward,
            EnemyState.MoveDown => Vector3.back,
            EnemyState.MoveLeft => Vector3.left,
            EnemyState.MoveRight => Vector3.right,
            _ => Vector3.zero
        };
    }

    // =========================
    // TRIGGERS
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "MoveUp": SetState(EnemyState.MoveUp); break;
            case "MoveDown": SetState(EnemyState.MoveDown); break;
            case "MoveLeft": SetState(EnemyState.MoveLeft); break;
            case "MoveRight": SetState(EnemyState.MoveRight); break;
            case "Attack": SetState(EnemyState.Attacking); break;
        }
    }

    // =========================
    // DEATH
    // =========================
    private void OnDeath()
    {
        Debug.Log("Evento OnDeath");

        if (_settings == null)
        {
            FlyweightFactory.Release(this);
            return;
        }

        if (CoinManager.Instance != null)
            CoinManager.Instance.AddCoins(_settings.Coins);

        if (_cachedSpawner != null)
            _cachedSpawner.OnEnemyDeath();

        FlyweightFactory.Release(this);
    }



    public int GetDamage()
    {
        return _settings.Damage;
    }
}





//////////////////////////////////////////////////////////

/*using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RedEnemyFlyweight : Flyweight
{
   // private ScoreManager _scoreManager;

   // private Transform _targetTransform;
    private RedEnemyFlyweightSettings _settings;
   // [SerializeField] private int _counter = 0;
    private int _currentHealth;


    private void OnEnable()
    {
        if (_settings == null)
            _settings = (RedEnemyFlyweightSettings)flyweightSettings;
       // _counter++;
       // _currentHealth = _settings.Health;
    }
    /*
    private void Start()
    {
        _scoreManager = FindFirstObjectByType<ScoreManager>();
        int randomTarget = Random.Range(1, 4);
        switch (randomTarget)
        {
            case 1:
                _targetTransform = GameObject.FindGameObjectWithTag("FinishB").transform;
                break;
            case 2:
                _targetTransform = GameObject.FindGameObjectWithTag("FinishC").transform;
                break;
            case 3:
                _targetTransform = GameObject.FindGameObjectWithTag("FinishD").transform;
                break;
        }
       // _targetTransform = GameObject.FindGameObjectWithTag("FinishB").transform;
        //_settings = (RedEnemyFlyweightSettings)flyweightSettings;
    }
    /*
    private void Update()
    {
        if (_targetTransform == null) return;
        var direction = (_targetTransform.position - transform.position).normalized;
        transform.position += direction * (_settings.Speed * Time.deltaTime);
    }

    public void OnDeath()
    {
        FlyweightFactory.Release(this);
        
       // _scoreManager.LoseLife();
    }
/*
    public void OnClicked()
    {
        //Destroy(gameObject);
        FlyweightFactory.Release(this);
       // _scoreManager.addScore();
    }

}
*/