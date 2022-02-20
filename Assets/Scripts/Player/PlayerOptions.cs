using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOptions : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _swingSpeedIncreasePercent = 0;
    [SerializeField] private int _damageIncreasePercent = 0;

    private bool _isPlaced = false;

    public event UnityAction<HeroLayer> WeaponChanged;

    public Weapon Weapon => _currentWeapon;
    public bool IsPlaced => _isPlaced;
    public int SwingSpeedIncreasePercent => _swingSpeedIncreasePercent;
    public int DamageIncrease => _damageIncreasePercent;

    public void ChangeWeapon(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
        WeaponChanged?.Invoke(_currentWeapon.WeaponType);
    }

    public void AddSwingSpeedBonus(int percent)
    {
        _swingSpeedIncreasePercent += percent;
    }

    public void Placing()
    {
        _isPlaced = true;
    }

    public void ChangePlace()
    {
        _isPlaced = false;
    }
}

public enum HeroLayer
{
    Default,
    WoodenBow,
    CompositeBow,
    LegendaryBow,
    WoodenStaff,
    LegendaryStaff
}