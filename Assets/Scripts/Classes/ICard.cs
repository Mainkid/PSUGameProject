public interface ICard
{
    int Id { get; set; } // ���� �����

    string Text { get; set; } // �����, ����������� ������������ ��������

    string Description { get; set; } // �������� �����

    Action Action1 { get; set; } // �������� 1 �� �����

    Action Action2 { get; set; } // �������� 2 �� �����

    string PicturePath { get; set; } // ���� � �������� �����

}
