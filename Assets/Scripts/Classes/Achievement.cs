using System;

[Serializable]
public class Achievement
{
    public int Id { get; set; } // Айди достижения

    public string Text { get; set; } // Текст достижения

    public string PicturePath { get; set; } // Путь к картинке достижения

    public bool isReceived { get; set; } = false;

    public Achievement() { }

    public Achievement(int id, string text, string picturePath)
    {
        Id = id;
        Text = text;
        PicturePath = picturePath;
    }

    public override string ToString() => Text;

}
