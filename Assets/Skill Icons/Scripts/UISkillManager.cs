using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillManager : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _skillsIcons;
    [SerializeField]
    private Image _skillImage;

    void Start()
    {
        FindObjectOfType<Player>().SkillManager.ChangeSkillEvent += OnChangeSkill;
    }

    public void OnChangeSkill(int skillIndex)
    {
        _skillImage.sprite = _skillsIcons[skillIndex];
    }
}
