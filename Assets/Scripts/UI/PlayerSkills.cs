using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private SkillView _template;
    [SerializeField] private List<Skill> _skills;

    public void Init(Transform container)
    {
        _container = container;

        foreach (var skill in _skills)
        {
            ShowSkill(skill);
        }
    }

    public void AddNewSkill(Skill skill)
    {
        _skills.Add(skill);
    }

    private void ShowSkill(Skill skill)
    {
        var currentSkill = Instantiate(_template, _container);
        currentSkill.Init(skill);
    }
}
