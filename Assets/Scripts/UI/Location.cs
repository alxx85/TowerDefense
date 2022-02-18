using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    [SerializeField] private Button _placeButton;

    private bool _isPlaced = false;

    public event UnityAction<Mercenary> PlacedHero;

    public bool IsPlaced => _isPlaced;

    private void OnEnable()
    {
        _placeButton.onClick.AddListener(SelectLocation);
    }

    private void OnDisable()
    {
        _placeButton.onClick.RemoveListener(SelectLocation);
    }

    public void Placing(Placement hero)
    {
        _isPlaced = true;
        hero.transform.SetParent(transform);
        hero.transform.localPosition = Vector2.zero;

        if (hero.TryGetComponent(out Mercenary mercenary))
        {
            PlacedHero?.Invoke(mercenary);
            mercenary.Init();
        }
    }

    public void Clearing()
    {
        _isPlaced = false;
    }

    private void SelectLocation()
    {
        if (!_isPlaced)
            Game.Instance.SelectedLocation(this);
        else
        {
            //change direction
            Mercenary hero = transform.GetComponentInChildren<Mercenary>();
            if (hero != null)
            {
                Game.Instance.SelectedDirection(hero);
            }
        }
    }
}
