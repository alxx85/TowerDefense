using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Skill
{
    private Location _currentLocations;
    private PlayerOptions _options;

    public override void UsingSkill()
    {
        _options = Game.Instance.GetComponent<PlayerOptions>();
        _currentLocations = Game.Instance.Player.GetComponentInParent<Location>();
        if (_currentLocations != null)
        {
            Debug.Log("Teleport");
            _currentLocations.ChangePlace();
            _options.ChangePlace();
        }
    }
}
