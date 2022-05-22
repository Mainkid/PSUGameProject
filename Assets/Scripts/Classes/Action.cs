using System.Collections.Generic;
using System.Linq;
using System;

// ��������, ������� �������� ������������ ��� ������� �����
[Serializable]
public class Action
{
    public string Text { get; set; } // ����� ��������

    public int CharismaChange { get; set; } // ����������, ���������� ����� �������

    public int IntelligenceChange { get; set; } // ����������, ���������� ����� ����������

    public int StaminaChange { get; set; } // ����������, ���������� ����� ������������

    public int AchievementId { get; set; } = -1; // ����, ������������ ����������, ���� ����

    public List<int> ConditionalCardIds { get; set; } = null; // ���� ����������� "��������" ����, ���� ����� ����

    public Action() { }

    public Action(string text, int charismaChange, int intelligenceChange, int staminaChange)
    {
        Text = text;
        CharismaChange = charismaChange;
        IntelligenceChange = intelligenceChange;
        StaminaChange = staminaChange;
    }

    public Action(string text, int charismaChange, int intelligenceChange, int staminaChange, int achievementId)
        : this(text, charismaChange, intelligenceChange, staminaChange) => AchievementId = achievementId;

    public Action(string text, int charismaChange, int intelligenceChange, int staminaChange, IEnumerable<int> conditionalCardIds)
        : this(text, charismaChange, intelligenceChange, staminaChange) => ConditionalCardIds = conditionalCardIds.ToList();

    public Action(string text, int charismaChange, int intelligenceChange, int staminaChange, int achievementId, IEnumerable<int> conditionalCardIds)
        : this(text, charismaChange, intelligenceChange, staminaChange)
    {
        AchievementId = achievementId;
        ConditionalCardIds = conditionalCardIds.ToList();
    }

    public override string ToString() => Text;
}
