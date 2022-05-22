using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Pool<DefaultCard> DefaultPool { get; private set; }

    public Pool<ConditionalCard> ActiveConditionalPool { get; private set; } = new Pool<ConditionalCard>();

    public List<ConditionalCard> AllConditionalCards { get; private set; }

    public List<DefaultCard> AllDefaultCards { get; private set; }

    public List<Achievement> AllAchievements { get; private set; }

    public List<DefaultCard> FinalCards { get; private set; }

    string saveProgressDirection; 

    private void Awake()
    {
        saveProgressDirection = $"{Application.persistentDataPath}/Achievements.xml";

        AllDefaultCards = XMLSerializer.LoadFromResources<List<DefaultCard>>("XML/DefaultCards");
        AllConditionalCards = XMLSerializer.LoadFromResources<List<ConditionalCard>>("XML/ConditionalCards");

        AllAchievements = System.IO.File.Exists(saveProgressDirection) ? XMLSerializer.Load<List<Achievement>>(saveProgressDirection)
            : XMLSerializer.LoadFromResources<List<Achievement>>("XML/Achievements");

        FinalCards = XMLSerializer.LoadFromResources<List<DefaultCard>>("XML/EndingDefaultCards");

    }

    public void Initialize()
    {
        DefaultPool?.Clear();
        ActiveConditionalPool?.Clear();
        DefaultPool = new Pool<DefaultCard>(AllDefaultCards, withShuffle: true);
    }

    public void SaveProgress()
    {
        XMLSerializer.Save(AllAchievements, saveProgressDirection);
    }
}
