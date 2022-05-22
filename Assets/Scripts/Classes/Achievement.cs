using System;

[Serializable]
public class Achievement
{
    public int Id { get; set; } // ���� ����������

    public string Text { get; set; } // ����� ����������

    public string PicturePath { get; set; } // ���� � �������� ����������

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
