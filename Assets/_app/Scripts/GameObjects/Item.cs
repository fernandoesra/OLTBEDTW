
public class Item
{
    public int Icon { get; set; }
    public string Name_ESP { get; set; }
    public string Name_ENG { get; set; }
    public string Description_ESP { get; set; }
    public string Description_ENG { get; set; }
    public Effect Effect { get; set; }
    public int Amount { get; set; }

    public Item(int i, string n_esp, string n_eng, string d_esp, string d_eng, Effect e, int a)
    {
        Icon = i;
        Name_ESP = n_esp;
        Name_ENG = n_eng;
        Description_ESP = d_esp;
        Description_ENG = d_eng;
        Effect = e;
        Amount = a;
    }

    public Item Copy()
    {
        Item copy = new Item(Icon, Name_ESP, Name_ENG, Description_ESP, Description_ENG, Effect, Amount);
        return copy;
    }

    public override string ToString()
    {
        return "Item (" + Icon + "): " + Name_ESP + " / " + Name_ENG + "\n"
            + Description_ESP + " / " + Description_ENG + "\n"
            + Effect
            + "\nAmount effect: " + Amount;
    }
}
