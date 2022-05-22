public interface ICard
{
    int Id { get; set; } // Айди карты

    string Text { get; set; } // Текст, описывающий происходящее действие

    string Description { get; set; } // Описание карты

    Action Action1 { get; set; } // Действие 1 на выбор

    Action Action2 { get; set; } // Действие 2 на выбор

    string PicturePath { get; set; } // Путь к картинке карты

}
