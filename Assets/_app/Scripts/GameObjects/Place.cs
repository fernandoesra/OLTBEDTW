using UnityEngine;
public class Place
{
    public GameObject Tile;
    public int Icon { get; set; }
    public string PlaceType { get; set; }
    public string Name_ESP { get; set; }
    public string Name_ENG { get; set; }
    public string Description_ESP { get; set; }
    public string Description_ENG { get; set; }
    public bool YetExplored { get; set; }

    // Option 1
    public string Option1_ESP { get; set; }
    public string Option1_ENG { get; set; }
    public string Answer1_ESP { get; set; }
    public string Answer1_ENG { get; set; }
    public Item Item1 { get; set; }
    public Effect Effect1 { get; set; }

    // Option 2
    public string Option2_ESP { get; set; }
    public string Option2_ENG { get; set; }
    public string Answer2_ESP { get; set; }
    public string Answer2_ENG { get; set; }
    public Item Item2 { get; set; }
    public Effect Effect2 { get; set; }

    // Option 3
    public string Option3_ESP { get; set; }
    public string Option3_ENG { get; set; }
    public string Answer3_ESP { get; set; }
    public string Answer3_ENG { get; set; }
    public Item Item3 { get; set; }
    public Effect Effect3 { get; set; }

    public Place(int icon, string placeType, string name_ESP, string name_ENG, string description_ESP, string description_ENG,
              string option1_ESP, string option1_ENG, string answer1_ESP, string answer1_ENG, Item item1, Effect effect1,
              string option2_ESP, string option2_ENG, string answer2_ESP, string answer2_ENG, Item item2, Effect effect2,
              string option3_ESP, string option3_ENG, string answer3_ESP, string answer3_ENG, Item item3, Effect effect3)
    {
        Icon = icon;
        PlaceType = placeType;
        Name_ESP = name_ESP;
        Name_ENG = name_ENG;
        Description_ESP = description_ESP;
        Description_ENG = description_ENG;
        YetExplored = false;

        // Option 1
        Option1_ESP = option1_ESP;
        Option1_ENG = option1_ENG;
        Answer1_ESP = answer1_ESP;
        Answer1_ENG = answer1_ENG;
        Item1 = item1;
        Effect1 = effect1;

        // Option 2
        Option2_ESP = option2_ESP;
        Option2_ENG = option2_ENG;
        Answer2_ESP = answer2_ESP;
        Answer2_ENG = answer2_ENG;
        Item2 = item2;
        Effect2 = effect2;

        // Option 3
        Option3_ESP = option3_ESP;
        Option3_ENG = option3_ENG;
        Answer3_ESP = answer3_ESP;
        Answer3_ENG = answer3_ENG;
        Item3 = item3;
        Effect3 = effect3;
    }

    public Place(string placeType, int icon, string name_ENG)
    {
        Icon = icon;
        PlaceType = placeType;
        Name_ENG = name_ENG;
        YetExplored = false;
    }
    public override string ToString()
    {
        string print = "Place: " + Name_ESP + " / " + Name_ENG + "\n"
            + "Icon and type: " + Icon + " / " + PlaceType + "\n"
            + "Description: " + Description_ESP + " / " + Name_ENG;
        if (Option1_ENG != null && Option1_ENG != "")
        {
            print += "\n      Option1: " + Option1_ESP + " / " + Option1_ENG
                + "      Answer1: " + Answer1_ESP + " / " + Answer1_ENG;
            if (Item1 != null)
            {
                print += "\nItem1: " + Item1.ToString();
            }
            if (Effect1 != null)
            {
                print += "\nEfect1: " + Effect1.ToString();
            }
        }
        if (Option2_ENG != null && Option2_ENG != "")
        {
            print += "\n      Option2: " + Option2_ESP + " / " + Option2_ENG
                + "      Answer2: " + Answer2_ESP + " / " + Answer2_ENG;
            if (Item2 != null)
            {
                print += "\nItem2: " + Item2.ToString();
            }
            if (Effect2 != null)
            {
                print += "\nEfect2: " + Effect2.ToString();
            }
        }
        if (Option3_ENG != null && Option3_ENG != "")
        {
            print += "\n      Option3: " + Option3_ESP + " / " + Option3_ENG
                + "      Answer3: " + Answer3_ESP + " / " + Answer3_ENG;
            if (Item3 != null)
            {
                print += "\nItem3: " + Item3.ToString();
            }
            if (Effect3 != null)
            {
                print += "\nEfect3: " + Effect3.ToString();
            }
        }
        return print;
    }

}