using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingHero : MonoBehaviour
{
    [SerializeField] private List<Mercenary> _mercenarys;
    [SerializeField] private MercenaryView _template;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private MercenaryDirection _directionContainer;

    private List<MercenaryView> _previewTemplates = new List<MercenaryView>();

    public void Init(List<Mercenary> mercenaries)
    {
        _mercenarys = mercenaries;
       
        for (int i = 0; i < _mercenarys.Count; i++)
        {
            AddViewHero(_mercenarys[i]);
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ChangeDirectionHero(Mercenary hero)
    {
        _directionContainer.Init(hero);
        _directionContainer.gameObject.SetActive(true);
    }

    private void AddViewHero(Mercenary mercenary)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.PlacedClick += OnPlacedHero;
        view.Render(mercenary);
    }

    private void OnPlacedHero(Mercenary hero, MercenaryView view)
    {
        if (TryPlacedHero(hero, view))
        {
            _mercenarys.Remove(hero);
            Destroy(view.gameObject);
        }
        
        gameObject.SetActive(false);
        _directionContainer.gameObject.SetActive(true);
    }

    private bool TryPlacedHero(Mercenary hero, MercenaryView view)
    {
        if (Game.Instance.CurrentLocation.IsPlaced == false)
        {
            var mercenary = Instantiate(hero);
            Game.Instance.CurrentLocation.Placing(mercenary.GetComponent<Placement>());
            view.PlacedClick -= OnPlacedHero;
            _directionContainer.Init(mercenary);
           return true;
        }
        return false;
    }
}
