using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryDirection : MonoBehaviour
{
    [SerializeField] private Mercenary _mercenary;
    [SerializeField] private Button _buttonLeft;
    [SerializeField] private Button _buttonRight;
    [SerializeField] private Button _buttonUp;
    [SerializeField] private Button _buttonDown;

    private void OnEnable()
    {
        _buttonLeft.onClick.AddListener(DirectionLeft);
        _buttonRight.onClick.AddListener(DirectionRight);
        _buttonUp.onClick.AddListener(DirectionUp);
        _buttonDown.onClick.AddListener(DirectionDown);
    }

    private void OnDisable()
    {
        _buttonLeft.onClick.RemoveListener(DirectionLeft);
        _buttonRight.onClick.RemoveListener(DirectionRight);
        _buttonUp.onClick.RemoveListener(DirectionUp);
        _buttonDown.onClick.RemoveListener(DirectionDown);
    }

    public void Init(Mercenary hero)
    {
        _mercenary = hero;
        transform.position = hero.transform.position;
    }

    public void DirectionLeft()
    {
        Activate(MercenaryLayer.Left);
    }

    public void DirectionRight()
    {
        Activate(MercenaryLayer.Right);
    }

    public void DirectionDown()
    {
        Activate(MercenaryLayer.Down);
    }

    public void DirectionUp()
    {
        Activate(MercenaryLayer.Up);
    }

    private void Activate(MercenaryLayer direction)
    {
        _mercenary.SetDirection(direction);
        gameObject.SetActive(false);
    }
}
