
public class Effect
{

    public string Name_ESP { get; set; }
    public string Name_ENG { get; set; }
    public string Description_ESP { get; set; }
    public string Description_ENG { get; set; }
    public string KeyEffect { get; set; }

    public Effect(string n_esp, string n_eng, string d_esp, string d_eng, string k)
    {
        Name_ESP = n_esp;
        Name_ENG = n_eng;
        Description_ESP = d_esp;
        Description_ENG = d_eng;
        KeyEffect = k;
    }

    public override string ToString()
    {
        return "Effect " + Name_ESP + " / " + Name_ENG + "\n"
            + Description_ESP + " / " + Description_ENG + "\n"
            + "Key: " + KeyEffect;
    }

}