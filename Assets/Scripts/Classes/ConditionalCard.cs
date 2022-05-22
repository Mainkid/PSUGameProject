using System;

[Serializable]
public class ConditionalCard : ICard
{
    public int Id { get; set; } // ���� �������� �����

    public string Text { get; set; } // �����, ����������� ������������ ��������

    public string Description { get; set; } // �������� �������� �����

    public Action Action1 { get; set; } // �������� 1 �� �����

    public Action Action2 { get; set; } // �������� 2 �� �����

    public string PicturePath { get; set; } // ���� � ��������

    public int Timer { get; set; } // ������, ����������� ���������� ����� ����� �������� ������ ����������� ������ �����

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

    public override string ToString() => $"����� �����: {Text}\n��������: {Description}";

}
