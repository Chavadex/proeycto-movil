using UnityEngine;

public class RedEnemyFlyweight : Flyweight
{
    private RedEnemyFlyweightSettings _settings;

    private int _currentHealth;

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

        _currentHealth = _settings.MaxHealth;

        SetState(EnemyState.MoveRight); // estado inicial
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
        switch (_currentState)
        {
            case EnemyState.MoveUp:
            case EnemyState.MoveDown:
            case EnemyState.MoveLeft:
            case EnemyState.MoveRight:
                Move();
                break;

            case EnemyState.Attacking:
                // lógica de ataque luego
                break;
        }
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

        switch (newState)
        {
            case EnemyState.MoveUp:
                _moveDirection = Vector3.forward; // Z+
                break;

            case EnemyState.MoveDown:
                _moveDirection = Vector3.back; // Z-
                break;

            case EnemyState.MoveLeft:
                _moveDirection = Vector3.left; // X-
                break;

            case EnemyState.MoveRight:
                _moveDirection = Vector3.right; // X+
                break;

            case EnemyState.Attacking:
                _moveDirection = Vector3.zero;
                break;
        }
    }

    // =========================
    // TRIGGERS
    // =========================
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "MoveUp":
                SetState(EnemyState.MoveUp);
                break;

            case "MoveDown":
                SetState(EnemyState.MoveDown);
                break;

            case "MoveLeft":
                SetState(EnemyState.MoveLeft);
                break;

            case "MoveRight":
                SetState(EnemyState.MoveRight);
                break;

            case "Attack":
                SetState(EnemyState.Attacking);
                break;
        }
    }

    // =========================
    // DAMAGE / DEATH
    // =========================
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            OnDeath();
    }

    public int GetDamage()
    {
        return _settings.Damage;
    }

    public void OnDeath()
    {
        FindFirstObjectByType<EnemySpawner>()?.OnEnemyDeath();
        FlyweightFactory.Release(this);
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