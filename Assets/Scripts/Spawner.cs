using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Path _path;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private List<Wave> _waves;

    private Wave _currentWave;
    private List<Enemy> _spawnedEnemys = new List<Enemy>();
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _offset = 0.3f;
    private int _spawned;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        _timeAfterLastSpawn += Time.deltaTime;
        StartWave();
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    public void StartWave()
    {
        if (_currentWave == null)
            return;

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

    private void OnEnemyDying(Enemy enemy)
    {
        _spawnedEnemys.Remove(enemy);
        enemy.Dying -= OnEnemyDying;
        Debug.Log(enemy.name + " - убит");
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
