using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _delayViewer;
    
    private Button _button;
    private Skill _currentSkill;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(UsingSkill);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(UsingSkill);
    }

    public void Init(Skill skill)
    {
        _currentSkill = skill;
        _image.sprite = skill.Image;
        _delayViewer.maxValue = skill.Delay;
        _delayViewer.value = 0;
    }

    private void Update()
    {
        if (_delayViewer != null && _delayViewer.value > 0)
        {
            float time = _delayViewer.value - Time.deltaTime;
            Mathf.Clamp(time, 0, _delayViewer.maxValue);
            _delayViewer.value = time;
        }
    }

    private void UsingSkill()
    {
        _currentSkill.UsingSkill();
        _delayViewer.value = _currentSkill.Delay;
    }
}
