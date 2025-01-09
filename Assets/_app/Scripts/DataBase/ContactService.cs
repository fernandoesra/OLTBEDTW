using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite4Unity3d;
using SQLiteConnection = SQLite4Unity3d.SQLiteConnection;
using TMPro;
using UnityEngine;

public class ContactService
{

    OLTtDataService dataService;

    public ContactService()
    {
        dataService = new OLTtDataService();
    }

    public List<Place> FetchPlacesDB(List<Item> itemsList, List<Effect> effectsList)
    {
        List<Place> placesList = new List<Place>();
        using (var connection = new SQLiteConnection(ShowPath(), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            var places = connection.Query<TempPlace>("SELECT place.ID, place.ICON, place.PLACETYPE, place.NAME_ESP, place.NAME_ENG, place.DESCRIPTION_ESP, " +
                "place.DESCRIPTION_ENG, place.OPTION1_ESP, place.OPTION1_ENG, place.ANSWER1_ESP, place.ANSWER1_ENG, " +
                "item1.NAME_ENG AS ITEM1_NAME_ENG, effect1.NAME_ENG AS EFFECT1_NAME_ENG, " +
                "place.OPTION2_ESP, place.OPTION2_ENG, place.ANSWER2_ESP, place.ANSWER2_ENG, " +
                "item2.NAME_ENG AS ITEM2_NAME_ENG, effect2.NAME_ENG AS EFFECT2_NAME_ENG, " +
                "place.OPTION3_ESP, place.OPTION3_ENG, place.ANSWER3_ESP, place.ANSWER3_ENG, " +
                "item3.NAME_ENG AS ITEM3_NAME_ENG, effect3.NAME_ENG AS EFFECT3_NAME_ENG " +
                "FROM place " +
                "LEFT JOIN item AS item1 ON place.ITEM1 = item1.ID " +
                "LEFT JOIN effect AS effect1 ON place.EFFECT1 = effect1.ID " +
                "LEFT JOIN item AS item2 ON place.ITEM2 = item2.ID " +
                "LEFT JOIN effect AS effect2 ON place.EFFECT2 = effect2.ID " +
                "LEFT JOIN item AS item3 ON place.ITEM3 = item3.ID " +
                "LEFT JOIN effect AS effect3 ON place.EFFECT3 = effect3.ID;");
            foreach (var place in places)
            {
                Effect fe1 = effectsList.FirstOrDefault(effect => effect.Name_ENG == place.EFFECT1_NAME_ENG);
                Effect fe2 = effectsList.FirstOrDefault(effect => effect.Name_ENG == place.EFFECT2_NAME_ENG);
                Effect fe3 = effectsList.FirstOrDefault(effect => effect.Name_ENG == place.EFFECT3_NAME_ENG);
                Item it1 = itemsList.FirstOrDefault(item => item.Name_ENG == place.ITEM1_NAME_ENG);
                Item it2 = itemsList.FirstOrDefault(item => item.Name_ENG == place.ITEM2_NAME_ENG);
                Item it3 = itemsList.FirstOrDefault(item => item.Name_ENG == place.ITEM3_NAME_ENG);
                Place actual = new Place(place.ICON, place.PLACETYPE, place.NAME_ESP, place.NAME_ENG, place.DESCRIPTION_ESP, place.DESCRIPTION_ENG
                    , place.OPTION1_ESP, place.OPTION1_ENG, place.ANSWER1_ESP, place.ANSWER1_ENG, it1, fe1
                    , place.OPTION2_ESP, place.OPTION2_ENG, place.ANSWER2_ESP, place.ANSWER2_ENG, it2, fe2
                    , place.OPTION3_ESP, place.OPTION3_ENG, place.ANSWER3_ESP, place.ANSWER3_ENG, it3, fe3);
                placesList.Add(actual);
            }
        }
        return placesList;
    }

    // Effect foundEffect = effectsList.FirstOrDefault(effect => effect.Name_ENG == item.EFFECT_NAME);
    // Item actual = new Item(item.ICON, item.NAME_ESP, item.NAME_ENG, item.DESCRIPTION_ESP, item.DESCRIPTION_ENG, foundEffect, item.AMOUNT);
    // itemsList.Add(actual);
    public class TempPlace
    {
        public int ID { get; set; }
        public int ICON { get; set; }
        public string PLACETYPE { get; set; }
        public string NAME_ESP { get; set; }
        public string NAME_ENG { get; set; }
        public string DESCRIPTION_ESP { get; set; }
        public string DESCRIPTION_ENG { get; set; }
        public string OPTION1_ESP { get; set; }
        public string OPTION1_ENG { get; set; }
        public string ANSWER1_ESP { get; set; }
        public string ANSWER1_ENG { get; set; }
        public string ITEM1_NAME_ENG { get; set; }
        public string EFFECT1_NAME_ENG { get; set; }
        public string OPTION2_ESP { get; set; }
        public string OPTION2_ENG { get; set; }
        public string ANSWER2_ESP { get; set; }
        public string ANSWER2_ENG { get; set; }
        public string ITEM2_NAME_ENG { get; set; }
        public string EFFECT2_NAME_ENG { get; set; }
        public string OPTION3_ESP { get; set; }
        public string OPTION3_ENG { get; set; }
        public string ANSWER3_ESP { get; set; }
        public string ANSWER3_ENG { get; set; }
        public string ITEM3_NAME_ENG { get; set; }
        public string EFFECT3_NAME_ENG { get; set; }

        public override string ToString()
        {
            return "ID: " + ID +
                   "\nICON: " + ICON +
                   "\nPLACETYPE: " + PLACETYPE +
                   "\nNAME_ESP: " + NAME_ESP +
                   "\nNAME_ENG: " + NAME_ENG +
                   "\nDESCRIPTION_ESP: " + DESCRIPTION_ESP +
                   "\nDESCRIPTION_ENG: " + DESCRIPTION_ENG +
                   "\nOPTION1_ESP: " + OPTION1_ESP +
                   "\nOPTION1_ENG: " + OPTION1_ENG +
                   "\nANSWER1_ESP: " + ANSWER1_ESP +
                   "\nANSWER1_ENG: " + ANSWER1_ENG +
                   "\nITEM1_NAME_ENG: " + ITEM1_NAME_ENG +
                   "\nEFFECT1_NAME_ENG: " + EFFECT1_NAME_ENG +
                   "\nOPTION2_ESP: " + OPTION2_ESP +
                   "\nOPTION2_ENG: " + OPTION2_ENG +
                   "\nANSWER2_ESP: " + ANSWER2_ESP +
                   "\nANSWER2_ENG: " + ANSWER2_ENG +
                   "\nITEM2_NAME_ENG: " + ITEM2_NAME_ENG +
                   "\nEFFECT2_NAME_ENG: " + EFFECT2_NAME_ENG +
                   "\nOPTION3_ESP: " + OPTION3_ESP +
                   "\nOPTION3_ENG: " + OPTION3_ENG +
                   "\nANSWER3_ESP: " + ANSWER3_ESP +
                   "\nANSWER3_ENG: " + ANSWER3_ENG +
                   "\nITEM3_NAME_ENG: " + ITEM3_NAME_ENG +
                   "\nEFFECT3_NAME_ENG: " + EFFECT3_NAME_ENG;
        }
    }

    public List<Event> FetchEventsDB(List<Effect> effectsList)
    {
        List<Event> eventsList = new List<Event>();
        using (var connection = new SQLiteConnection(ShowPath(), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            var events = connection.Query<TempEvent>("SELECT event.DESCRIPTION_ESP, event.DESCRIPTION_ENG, " +
            "event.AMOUNT, effect.NAME_ENG AS EFFECT_NAME FROM event INNER JOIN effect ON event.EFFECT = effect.ID;");
            foreach (var actualEvent in events)
            {
                Effect foundEffect = effectsList.FirstOrDefault(effect => effect.Name_ENG == actualEvent.EFFECT_NAME);
                Event actual = new Event(actualEvent.DESCRIPTION_ESP, actualEvent.DESCRIPTION_ENG, foundEffect, actualEvent.AMOUNT);
                eventsList.Add(actual);
            }
        }

        return eventsList;
    }

    public class TempEvent
    {
        public string DESCRIPTION_ESP { get; set; }
        public string DESCRIPTION_ENG { get; set; }
        public string EFFECT_NAME { get; set; }
        public int AMOUNT { get; set; }

    }

    public List<Item> FetchItemsDB(List<Effect> effectsList)
    {
        List<Item> itemsList = new List<Item>();
        using (var connection = new SQLiteConnection(ShowPath(), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            var items = connection.Query<TempItem>("SELECT item.ID, item.ICON, item.NAME_ESP, item.NAME_ENG, item.DESCRIPTION_ESP, item.DESCRIPTION_ENG, " +
            "item.AMOUNT, effect.NAME_ENG AS EFFECT_NAME FROM item INNER JOIN effect ON item.EFFECT = effect.ID;");
            foreach (var item in items)
            {
                Effect foundEffect = effectsList.FirstOrDefault(effect => effect.Name_ENG == item.EFFECT_NAME);
                Item actual = new Item(item.ICON, item.NAME_ESP, item.NAME_ENG, item.DESCRIPTION_ESP, item.DESCRIPTION_ENG, foundEffect, item.AMOUNT);
                itemsList.Add(actual);
            }
        }

        return itemsList;
    }

    public class TempItem
    {
        public int ICON { get; set; }
        public string NAME_ESP { get; set; }
        public string NAME_ENG { get; set; }
        public string DESCRIPTION_ESP { get; set; }
        public string DESCRIPTION_ENG { get; set; }
        public string EFFECT_NAME { get; set; }
        public int AMOUNT { get; set; }

        public override string ToString()
        {
            return "" + ICON + " / " + NAME_ESP + " / " + NAME_ENG + " / " + DESCRIPTION_ESP + " / " + DESCRIPTION_ENG + " / " + EFFECT_NAME + " / " + AMOUNT;
        }
    }

    public List<Effect> FetchEffectsDB()
    {

        List<Effect> effectsList = new List<Effect>();

        using (var connection = new SQLiteConnection(ShowPath(), SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            var effects = connection.Query<TempEffect>("SELECT NAME_ESP, NAME_ENG, DESCRIPTION_ESP, DESCRIPTION_ENG, EFFECT FROM effect");
            foreach (var condition in effects)
            {
                Effect actual = new Effect(condition.NAME_ESP, condition.NAME_ENG, condition.DESCRIPTION_ESP, condition.DESCRIPTION_ENG, condition.EFFECT);
                effectsList.Add(actual);
            }
        }

        return effectsList;
    }

    public class TempEffect
    {
        public string NAME_ESP { get; set; }
        public string NAME_ENG { get; set; }
        public string DESCRIPTION_ESP { get; set; }
        public string DESCRIPTION_ENG { get; set; }
        public string EFFECT { get; set; }
    }

    public void InsertPlaces()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StartingPlaces.csv");
        WWW www = new WWW(filePath);
        string[] lines = www.text.Split('#');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            string ic = fields[0];
            string pt = fields[1];
            string n_es = fields[2];
            string n_en = fields[3];
            string d_es = fields[4];
            string d_en = fields[5];
            // Option 1
            string op1_es = fields[6];
            string op1_en = fields[7];
            string as1_es = fields[8];
            string as1_en = fields[9];
            string i1 = fields[10];
            string ef1 = fields[11];
            // Option 2
            string op2_es = fields[12];
            string op2_en = fields[13];
            string as2_es = fields[14];
            string as2_en = fields[15];
            string i2 = fields[16];
            string ef2 = fields[17];
            // Option 3
            string op3_es = fields[18];
            string op3_en = fields[19];
            string as3_es = fields[20];
            string as3_en = fields[21];
            string i3 = fields[22];
            string ef3 = fields[23];

            n_en = n_en.Replace("'", "''");
            d_en = d_en.Replace("'", "''");
            op1_en = op1_en.Replace("'", "''");
            as1_en = as1_en.Replace("'", "''");
            op2_en = op2_en.Replace("'", "''");
            as2_en = as2_en.Replace("'", "''");
            op3_en = op3_en.Replace("'", "''");
            as3_en = as3_en.Replace("'", "''");

            d_en = d_en.Replace("\"", "");
            d_es = d_es.Replace("\"", "");

            as1_es = as1_es.Replace("\"", "");
            as2_es = as2_es.Replace("\"", "");
            as3_es = as3_es.Replace("\"", "");
            as1_en = as1_en.Replace("\"", "");
            as2_en = as2_en.Replace("\"", "");
            as3_en = as3_en.Replace("\"", "");

            dataService.GetConnection().Execute("INSERT INTO place (ICON, PLACETYPE, NAME_ESP, NAME_ENG, DESCRIPTION_ESP, DESCRIPTION_ENG, "
            + "OPTION1_ESP, OPTION1_ENG, ANSWER1_ESP, ANSWER1_ENG, ITEM1, EFFECT1, "
            + "OPTION2_ESP, OPTION2_ENG, ANSWER2_ESP, ANSWER2_ENG, ITEM2, EFFECT2, "
            + "OPTION3_ESP, OPTION3_ENG, ANSWER3_ESP, ANSWER3_ENG, ITEM3, EFFECT3) "
            + $"VALUES ('{ic}', '{pt}', '{n_es}', '{n_en}', '{d_es}', '{d_en}', '{op1_es}', '{op1_en}', '{as1_es}', '{as1_en}', '{i1}', '{ef1}', '{op2_es}', '{op2_en}' "
            + $", '{as2_es}', '{as2_en}', '{i2}', '{ef2}', '{op3_es}', '{op3_en}', '{as3_es}', '{as3_en}', '{i3}', '{ef3}');");

        }
    }

    public void InsertEvents()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StartingEvents.csv");
        WWW www = new WWW(filePath);
        string[] lines = www.text.Split('#');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            // Debug.Log(line);
            string[] fields = line.Split(';');
            string d_esp = fields[0];
            string d_eng = fields[1];
            string e = fields[2];
            string am = fields[3];
            d_eng = d_eng.Replace("'", "''");

            d_esp = d_esp.Replace("\"", "");
            d_eng = d_eng.Replace("\"", "");

            // Debug.Log(d_esp + "\n\n" + d_eng + "\n\n Effect: " + e + " / Amount: " + am + "\n\n");

            dataService.GetConnection().Execute($"INSERT INTO event (DESCRIPTION_ESP, DESCRIPTION_ENG, EFFECT, AMOUNT) VALUES" +
                $"('{d_esp}', '{d_eng}', '{e}', '{am}');");
        }
    }

    public void InsertEffects()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StartingEffects.csv");
        WWW www = new WWW(filePath);
        string[] lines = www.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            string n_esp = fields[0];
            string n_eng = fields[1];
            string d_esp = fields[2];
            string d_eng = fields[3];
            string e = fields[4];
            n_eng = n_eng.Replace("'", "''");
            d_eng = d_eng.Replace("'", "''");
            dataService.GetConnection().Execute($"INSERT INTO effect (NAME_ESP, NAME_ENG, DESCRIPTION_ESP, DESCRIPTION_ENG, EFFECT) VALUES" +
                $"('{n_esp}', '{n_eng}', '{d_esp}', '{d_eng}', '{e}');");
        }
    }

    public void InsertItems()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "StartingItems.csv");
        WWW www = new WWW(filePath);
        string[] lines = www.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            string ic = fields[0];
            string n_esp = fields[1];
            string n_eng = fields[2];
            string d_esp = fields[3];
            string d_eng = fields[4];
            string e = fields[5];
            string a = fields[6];
            n_eng = n_eng.Replace("'", "''");
            d_eng = d_eng.Replace("'", "''");
            dataService.GetConnection().Execute($"INSERT INTO item (ICON, NAME_ESP, NAME_ENG, DESCRIPTION_ESP, DESCRIPTION_ENG, EFFECT, AMOUNT) VALUES" +
                $"('{ic}', '{n_esp}', '{n_eng}', '{d_esp}', '{d_eng}', '{e}', '{a}');");
        }
    }

    public void CreateTables()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "CreateBD.sql");
        WWW www = new WWW(path);
        if (string.IsNullOrEmpty(www.error))
        {
            string text = www.text;
            // Debug.Log("Existe \"create.sql\" en " + path + " y su contenido es:\n" + text + "\n\n");
        }
        else
        {
            Debug.LogError("Failed to load SQL file: " + www.error);
        }
        string scriptContent = www.text;
        ExecuteScript(scriptContent);
    }

    public void InsertTestPlaces()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "InsertTestPlaces.csv");
        WWW www = new WWW(filePath);
        string[] lines = www.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            string name = fields[0];
            string description = fields[1];
            string Type = fields[2];
            string Icon = fields[3];
            Debug.Log(name + ", " + description + ", " + Type + ", " + Icon);
            dataService.GetConnection().Execute($"INSERT INTO testplace (Name, Description, Type, Icon) VALUES ('{name}', '{description}', '{Type}', '{Icon}');");
        }
    }

    public void CreateTestTables()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "CreateTestDB.sql");
        WWW www = new WWW(path);
        if (string.IsNullOrEmpty(www.error))
        {
            string text = www.text;
            Debug.Log("Existe \"create.sql\" en " + path + " y su contenido es:\n" + text + "\n\n");
        }
        else
        {
            Debug.LogError("Failed to load SQL file: " + www.error);
        }
        string scriptContent = www.text;
        ExecuteScript(scriptContent);
    }

    public void ExecuteScript(string script)
    {
        string[] commands = script.Split(';');
        foreach (string command in commands)
        {
            if (!string.IsNullOrWhiteSpace(command))
                dataService.GetConnection().Execute(command + ";");

        }
    }

    public string ShowPath()
    {
        return dataService.ShowPath();
    }

}