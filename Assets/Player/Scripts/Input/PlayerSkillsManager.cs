using System.Collections.Generic;

public class PlayerSkillsManager
{
    private List<PlayerAbilityState> _skills;
    private int _currentSkillIndex;

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
    }

    public void ChangeSkillDown()
    {
        _currentSkillIndex--;
        if (_currentSkillIndex >= _skills.Count)
        {
            _currentSkillIndex = 0;
        }
    }
}
