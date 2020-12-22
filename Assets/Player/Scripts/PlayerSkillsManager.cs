using System;
using System.Collections.Generic;

public class PlayerSkillsManager
{
    private List<PlayerAbilityState> _skills;
    private int _currentSkillIndex;
    public Action<int> ChangeSkillEvent;

    public PlayerSkillsManager()
    {
        _skills = new List<PlayerAbilityState>();
        _currentSkillIndex = -1;
    }

    public void AddSkill(PlayerAbilityState newSkill)
    {
        _skills.Add(newSkill);
        _currentSkillIndex++;
    }

    public PlayerAbilityState GetCurrentSkill()
    {
        return _skills[_currentSkillIndex];
    }

    public void ChangeSkillUp()
    {
        _currentSkillIndex++;
        if (_currentSkillIndex >= _skills.Count)
        {
            _currentSkillIndex = 0;
        }

        ChangeSkillEvent?.Invoke(_currentSkillIndex);
    }

    public void ChangeSkillDown()
    {
        _currentSkillIndex--;
        if (_currentSkillIndex < 0)
        {
            _currentSkillIndex = _skills.Count - 1;
        }

        ChangeSkillEvent?.Invoke(_currentSkillIndex);
    }
}
