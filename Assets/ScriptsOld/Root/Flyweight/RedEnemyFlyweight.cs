using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RedEnemyFlyweight : Flyweight
{
    private ScoreManager _scoreManager;

    private Transform _targetTransform;
    private RedEnemyFlyweightSettings _settings;
    [SerializeField] private int _counter = 0;
    private int _currentHealth;


    private void OnEnable()
    {
        if (_settings == null)
            _settings = (RedEnemyFlyweightSettings)flyweightSettings;
        _counter++;
        _currentHealth = _settings.Health;
    }

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

    private void Update()
    {
        if (_targetTransform == null) return;
        var direction = (_targetTransform.position - transform.position).normalized;
        transform.position += direction * (_settings.Speed * Time.deltaTime);
    }

    public void OnDeath()
    {
        FlyweightFactory.Release(this);
        
        _scoreManager.LoseLife();
    }

    public void OnClicked()
    {
        //Destroy(gameObject);
        FlyweightFactory.Release(this);
        _scoreManager.addScore();
    }

}
