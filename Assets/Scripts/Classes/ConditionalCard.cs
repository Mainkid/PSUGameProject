using System;

[Serializable]
public class ConditionalCard : ICard
{
    public int Id { get; set; } // Айди условной карты

    public string Text { get; set; } // Текст, описывающий происходящее действие

    public string Description { get; set; } // Описание условной карты

    public Action Action1 { get; set; } // Действие 1 на выбор

    public Action Action2 { get; set; } // Действие 2 на выбор

    public string PicturePath { get; set; } // Путь к картинке

    public int Timer { get; set; } // Таймер, указывающий количество ходов после которого должна разыграться данная карта

    public ConditionalCard() { }

    public ConditionalCard(int id, string text, string description, Action action1, Action action2, string picturePath, int timer)
    {
        Id = id;
        Text = text;
        Description = description;
        Action1 = action1;
        Action2 = action2;
        PicturePath = picturePath;
        Timer = timer;
    }

    public int TickTimer()
    {
        if (Timer > 0)
            Timer--;
        return Timer;
    }

    public override string ToString() => $"Текст карты: {Text}\nОписание: {Description}";

}
