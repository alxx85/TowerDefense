using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeholder : MonoBehaviour
{
    [SerializeField] private List<Location> _locations;
    [SerializeField] private List<Mercenary> _listHero;

    private void Start()
    {
        _listHero = new List<Mercenary>();
        _locations = new List<Location>(GetComponentsInChildren<Location>());
        for (int i = 0; i < _locations.Count; i++)
        {
            _locations[i].PlacedHero += OnPlasedHero;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _locations.Count; i++)
        {
            _locations[i].PlacedHero -= OnPlasedHero;
        }
    }

    public void Clearing()
    {
        foreach (var hero in _listHero)
        {
            Destroy(hero.gameObject);
        }

        _listHero.Clear();

        foreach (var location in _locations)
        {
            if (!location.GetComponentInChildren<PlayerAttacks>())
                location.Clearing();
        }
    }

    private void OnPlasedHero(Mercenary hero)
    {
        _listHero.Add(hero);
    }
}
