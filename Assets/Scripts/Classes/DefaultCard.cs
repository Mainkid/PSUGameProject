using System;

[Serializable]
public class DefaultCard : ICard
{
    public int Id { get; set; } // ���� �����

    public string Text { get; set; } // �����, ����������� ������������ ��������

    public string Description { get; set; } // �������� �����

    public Action Action1 { get; set; } // �������� 1 �� �����

    public Action Action2 { get; set; } // �������� 2 �� �����

    public string PicturePath { get; set; } // ���� � �������� �����

    public float DrawProbability { get; set; } = Rarities.Common; // ����������� ��������� �����

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

    public override string ToString() => $"����� �����: {Text}\n��������: {Description}";

}
