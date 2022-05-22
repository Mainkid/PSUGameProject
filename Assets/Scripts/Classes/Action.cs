using System.Collections.Generic;
using System.Linq;
using System;

// ƒействие, которое выбирает пользователь при текущей карте
[Serializable]
public class Action
{
    public string Text { get; set; } // “екст действи€

    public int CharismaChange { get; set; } //  оличество, измен€емых очков оба€ни€

    public int IntelligenceChange { get; set; } //  оличество, измен€емых очков интеллекта

    public int StaminaChange { get; set; } //  оличество, измен€емых очков выносливости

    public int AchievementId { get; set; } = -1; // јйди, открываемого достижени€, если есть

    public List<int> ConditionalCardIds { get; set; } = null; // јйди открываемых "условных" карт, если такие есть

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
