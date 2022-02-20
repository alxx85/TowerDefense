using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Button _startButton;
    [SerializeField] private List<Wave> _waves;

    private Wave _currentWave;
    private List<Enemy> _spawnedEnemys = new List<Enemy>();
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _offset = 0.3f;
    private int _spawned;
    private bool _isRunningAttack = false;

    public event UnityAction<float> EnemyKilling;
    public event UnityAction EndWave;
    public event UnityAction StartWave;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartAttack);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartAttack);
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        if (_isRunningAttack)
            RunningWave();
    }

    public void StartAttack()
    {
        SetWave(_currentWaveNumber);
        _startButton.interactable = false;
        _isRunningAttack = true;
    }

    public void NextWave()
    {
        _currentWaveNumber++;
        _spawned = 0;
    }

    private void RunningWave()
    {
        if (_currentWave == null)
        {
            _isRunningAttack = false;
            _spawned = 0;
            StartWave?.Invoke();
            return;
        }

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
        {
            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        int randomEnemy = Random.Range(0, _currentWave.Template.Length);
        Enemy enemy = Instantiate(_currentWave.Template[randomEnemy], transform.position, transform.rotation, _enemyContainer).GetComponent<Enemy>();
        enemy.Init(_path, _offset);
        _spawnedEnemys.Add(enemy);
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy, float reward)
    {
        _spawnedEnemys.Remove(enemy);
        enemy.Dying -= OnEnemyDying;
       
        if (reward > 0)
            EnemyKilling?.Invoke(reward);

        if (_spawnedEnemys.Count == 0)
        {
            _startButton.interactable = true;
            EndWave?.Invoke();
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }
}


[System.Serializable]
public class Wave
{
    public GameObject[] Template;
    public int Count;
    public float Delay;
}
