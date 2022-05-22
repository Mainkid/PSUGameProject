using System;

[Serializable]
public class DefaultCard : ICard
{
    public int Id { get; set; } // Айди карты

    public string Text { get; set; } // Текст, описывающий происходящее действие

    public string Description { get; set; } // Описание карты

    public Action Action1 { get; set; } // Действие 1 на выбор

    public Action Action2 { get; set; } // Действие 2 на выбор

    public string PicturePath { get; set; } // Путь к картинке карты

    public float DrawProbability { get; set; } = Rarities.Common; // Вероятность розыгрыша карты

    public DefaultCard() { }

    public DefaultCard(int id, string text, string description, Action action1, Action action2, string picturePath)
    {
        Id = id;
        Text = text;
        Description = description;
        Action1 = action1;
        Action2 = action2;
        PicturePath = picturePath;
    }

    public DefaultCard(int id, string text, string description, Action action1, Action action2, string picturePath, float drawProbability)
        : this(id, text, description, action1, action2, picturePath)
    {
        DrawProbability = drawProbability;
    }

    public override string ToString() => $"Текст карты: {Text}\nОписание: {Description}";

}
