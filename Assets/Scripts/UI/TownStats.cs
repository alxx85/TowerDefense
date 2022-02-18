using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownStats : MonoBehaviour
{
    [SerializeField] private Town _town;
    [SerializeField] private Slider _townDefensePower;
    [SerializeField] private Slider _townProgress;

    private void OnEnable()
    {
        _town.DefensePowerChanged += OnDefensePowerChanged;
        _town.TownProgressChanged += OnTownProgressChanged;
    }

    private void OnDisable()
    {
        _town.DefensePowerChanged -= OnDefensePowerChanged;
    }

    private void OnDefensePowerChanged(float value, float powerMaxValue)
    {
        _townDefensePower.maxValue = powerMaxValue;
        _townDefensePower.value = value;
    }

    private void OnTownProgressChanged(float value, float progressMaxValue)
    {
        _townProgress.maxValue = progressMaxValue;
        _townProgress.value = value;
    }
}
