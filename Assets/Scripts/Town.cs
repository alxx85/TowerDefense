using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Town : MonoBehaviour
{
    [SerializeField] private TownDewelopment[] _dewelopmentProgress;
    [SerializeField, Range(100,1000)] private int _maxDefensePower;
    [SerializeField] private PlayerOptions player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _townMenu;

    private int _defensePower;
    private float _townProgress = 0;
    private int _curentDevelopment = 0;

    public event UnityAction<float, float> DefensePowerChanged;
    public event UnityAction<float, float> TownProgressChanged;

    private void OnEnable()
    {
        _spawner.EnemyKilling += AddTownProgress;
    }

    private void OnDisable()
    {
        _spawner.EnemyKilling -= AddTownProgress;
    }

    private void Start()
    {
        _defensePower = _maxDefensePower;
        AddTownProgress(0);
        DefensePowerChanged?.Invoke(_defensePower, _maxDefensePower);
    }

    private void AddTownProgress(float count)
    {
        _townProgress += count;
        
        if (_curentDevelopment < _dewelopmentProgress.Length)
            if (_townProgress >= _dewelopmentProgress[_curentDevelopment].progressValue)
            {
                _townProgress = 0;
                _dewelopmentProgress[_curentDevelopment].townTemplate.SetActive(false);
                _dewelopmentProgress[++_curentDevelopment].townTemplate.SetActive(true);
            }

        float progressMaxValue = _dewelopmentProgress[_curentDevelopment].progressValue;

        Debug.Log($"{_townProgress}, max = {progressMaxValue}");
        TownProgressChanged?.Invoke(_townProgress, progressMaxValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _defensePower -= enemy.FinishPath();
            DefensePowerChanged?.Invoke(_defensePower, _maxDefensePower);
        }
    }
}


[System.Serializable]
public class TownDewelopment
{
    public GameObject townTemplate;
    public float progressValue;
}
