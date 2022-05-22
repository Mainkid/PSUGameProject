public class Player
{
    public static readonly int Threshold = 10;

    public int Intelligence { get; set; }
    public int Charisma { get; set; }
    public int Stamina { get; set; }

    SecureRandom rngProvider = new SecureRandom();

    public Player()
    {
        Intelligence = rngProvider.Next(4, 7);
        Charisma = rngProvider.Next(4, 7);
        Stamina = rngProvider.Next(4, 7);
    }

    public void updateCharacteristics(int intelligence, int charisma, int stamina)
    {
        int newIntelligence = Intelligence + intelligence;
        int newCharisma = Charisma + charisma;
        int newStamina = Stamina + stamina;

        Intelligence = newIntelligence > Threshold ? Threshold : newIntelligence < 0 ? 0 : newIntelligence;
        Charisma = newCharisma > Threshold ? Threshold : newCharisma < 0 ? 0 : newCharisma;
        Stamina = newStamina > Threshold ? Threshold : newStamina < 0 ? 0 : newStamina;
    }

    public bool isDead() => Intelligence <= 0 || Charisma <= 0 || Stamina <= 0;

    public override string ToString() => $"Интеллект: {Intelligence}\nОбаяние: {Charisma}\nВыносливость: {Stamina}";
}
