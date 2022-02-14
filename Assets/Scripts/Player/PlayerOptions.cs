using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOptions : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;
    [SerializeField] private int _swingSpeedIncreasePercent = 0;
    [SerializeField] private int _damageIncreasePercent = 0;
    [SerializeField] private Mercenary _mercenary;

    public event UnityAction<int> SwingSpeedIncreaseChanged;
    public event UnityAction<Layer> WeaponChanged;

    public Weapon Weapon => _currentWeapon;
    public int DamageIncrease => _damageIncreasePercent;

    public void ChangeWeapon(Weapon newWeapon)
    {
        _currentWeapon = newWeapon;
        WeaponChanged?.Invoke(_currentWeapon.WeaponType);
    }

    public void AddSwingSpeedBonus(int percent)
    {
        _swingSpeedIncreasePercent += percent;
        SwingSpeedIncreaseChanged?.Invoke(_swingSpeedIncreasePercent);
    }
}
