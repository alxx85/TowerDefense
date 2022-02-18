using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MercenaryView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _placeButton;

    private Mercenary _prefab;

    public event UnityAction<Mercenary, MercenaryView> PlacedClick;

    private void OnEnable()
    {
        _placeButton.onClick.AddListener(PlaceButtonClick);
    }

    private void OnDisable()
    {
        _placeButton.onClick.RemoveListener(PlaceButtonClick);
    }

    public void Render(Mercenary mercenary)
    {
        _prefab = mercenary;
        _image.sprite = mercenary.Sprite;
    }

    private void PlaceButtonClick()
    {
        PlacedClick?.Invoke(_prefab, this);
    }
}
