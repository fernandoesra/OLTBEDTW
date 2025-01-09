
public class Event
{
    public string Description_ESP { get; set; }
    public string Description_ENG { get; set; }
    public Effect Effect { get; set; }
    public int Amount { get; set; }

    public Event(string d_esp, string d_eng, Effect e, int a)
    {
        Description_ESP = d_esp;
        Description_ENG = d_eng;
        Effect = e;
        Amount = a;
    }

    public override string ToString()
    {
        return Description_ESP + " / " + Description_ENG + "\n"
            + Effect
            + "\nAmount effect: " + Amount;
    }
}