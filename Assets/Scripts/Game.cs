using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Placeholder _placeholder;
    [SerializeField] private PlayerAttacks _player;
    [SerializeField] private Mercenaries[] _mercenaries;
    [Space]
    [SerializeField] private SelectingHero _selectHeroMenu;

    private Location _currentLocation;
    private PlayerOptions _options;
    private bool _waveChanged = true;

    public Location CurrentLocation => _currentLocation;
    
    public static Game Instance;

    private void Awake()
    {
        if (Game.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Game.Instance = this;

        _options = GetComponent<PlayerOptions>();
        _spawner.EndWave += OnWaveChanged;
    }

    private void OnDisable()
    {
        _spawner.EndWave -= OnWaveChanged;
    }

    public void SelectedLocation(Location location)
    {
        _currentLocation = location;

        if (_options.IsPlaced)
        {
            if (_waveChanged)
                _selectHeroMenu.Init(CreatMercenariesLest());
            _selectHeroMenu.gameObject.SetActive(true);
        }
        else
        {
            var player = Instantiate(_player);
            location.Placing(player.GetComponent<Placement>());
            _options.Placing();
            _player = player;
        }
    }

    public void SelectedDirection(Mercenary hero)
    {

    }

    public Enemy IsAcquireTarget(Vector2 position, float range)
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(position, new Vector2(range, range), 0);

        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].TryGetComponent(out Enemy enemy))
            {
                if (enemy.IsAlive)
                    return enemy;
            }
        }
        return null;
    }

    public Enemy DidLoseTarget(Vector2 position, Enemy target, float range)
    {
        float distance = Vector2.Distance(target.transform.position, position);

        if (distance > range || !target.IsAlive)
            return null;

        return target;
    }

    private void OnWaveChanged()
    {
        _waveChanged = true;
        _placeholder.Clearing();
    }

    private List<Mercenary> CreatMercenariesLest()
    {
        List<Mercenary> heroList = new List<Mercenary>();
        for (int i = 0; i < _mercenaries.Length; i++)
        {
            for (int j = 0; j < _mercenaries[i].Count; j++)
                heroList.Add(_mercenaries[i].Template);
        }

        _waveChanged = false;
        return heroList;
    }
}

[System.Serializable]
public class Mercenaries
{
    public Mercenary Template;
    public int Count;
}

