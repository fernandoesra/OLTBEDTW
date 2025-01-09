using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class StadisticsController : MonoBehaviour
{
    private string filePath;
    [SerializeField] TextMeshProUGUI StepsCounter;
    [SerializeField] TextMeshProUGUI TurnsCounter;
    [SerializeField] TextMeshProUGUI ItemsCounter;
    [SerializeField] TextMeshProUGUI DeathsCounter;
    [SerializeField] TextMeshProUGUI EndCounters;
    private int steps = 0;
    private int turns = 0;
    private int items = 0;
    private int deaths = 0;
    private int end = 0;
    public bool InGame = false;

    void Start()
    {
        if (!InGame)
            LoadData();
    }

    public void Reset()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "PlayerStadistics.csv");
        WWW www = new WWW(filePath);
        string register = "STEPS;TURNS;ITEMS;DEATHS;END\n0;0;0;0;0";
        File.WriteAllText(filePath, register);
        LoadData();
    }

    public void LoadData()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "PlayerStadistics.csv");
        WWW www = new WWW(filePath);
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            string steps = fields[0];
            string turns = fields[1];
            string items = fields[2];
            string deaths = fields[3];
            string end = fields[4];
            StepsCounter.text = steps.ToString();
            TurnsCounter.text = turns.ToString();
            ItemsCounter.text = items.ToString();
            DeathsCounter.text = deaths.ToString();
            EndCounters.text = end.ToString();
        }

    }

    public void SaveData(int s, int t, int it, int d, int e)
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "PlayerStadistics.csv");
        WWW www = new WWW(filePath);
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(';');
            s += int.Parse(fields[0]);
            t += int.Parse(fields[1]);
            it += int.Parse(fields[2]);
            d += int.Parse(fields[3]);
            e += int.Parse(fields[4]);
        }

        string register = "STEPS;TURNS;ITEMS;DEATHS;END\n"
            + s + ";" + t + ";" + it + ";" + d + ";" + e;

        File.WriteAllText(filePath, register);
    }

    public void AddSteps(int add)
    {
        steps += add;
    }

    public void AddTurns(int add)
    {
        turns += add;
    }

    public void AddItems(int add)
    {
        items += add;
    }

    public void AddDeaths(int add)
    {
        deaths += add;
    }

    public void AddEnd(int add)
    {
        end += add;
    }

}
