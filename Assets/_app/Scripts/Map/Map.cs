using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
public class Map
{

    public Place[,] map;
    public int height { get; set; }
    public int width { get; set; }
    public System.Numerics.Vector2 PlayerPointer = new System.Numerics.Vector2(0, 0);
    private int[,] Directions = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

    public Map(int height, int width)
    {
        this.height = height;
        this.width = width;
        map = new Place[height, width];
    }

    public void ExtendCaliope()
    {
        List<string> haveCaliope = new List<string>();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i, j] != null)
                {
                    if (map[i, j].PlaceType == "CALIOPE")
                    {
                        for (int k = 0; k < Directions.GetLength(0); k++)
                        {
                            int SearchX = i + Directions[k, 0];
                            int SearchY = j + Directions[k, 1];
                            if (Valid(SearchX, SearchY) && map[SearchX, SearchY].PlaceType != "CALIOPE")
                            {
                                string actual = SearchX + "," + SearchY;
                                if (!haveCaliope.Contains(actual))
                                    haveCaliope.Add(actual);
                            }
                        }
                    }
                }
            }
        }
        foreach (var actual in haveCaliope)
        {
            int newCaliope = UnityEngine.Random.Range(1, 101);
            if (newCaliope <= 35)
            {
                string[] coordinates = actual.Split(',');
                int x = int.Parse(coordinates[0]);
                int y = int.Parse(coordinates[1]);
                int random = UnityEngine.Random.Range(1, 5);
                if (random == 1)
                    map[x, y] = new Place("CALIOPE", 0, "");
                if (random == 2)
                    map[x, y] = new Place("CALIOPE", 1, "");
                if (random == 3)
                    map[x, y] = new Place("CALIOPE", 2, "");
                if (random == 4)
                    map[x, y] = new Place("CALIOPE", 3, "");
            }
        }
    }
    public Place SearchForPlaces()
    {
        for (int i = 0; i < Directions.GetLength(0); i++)
        {
            int SearchX = (int)PlayerPointer.X + Directions[i, 0];
            int SearchY = (int)PlayerPointer.Y + Directions[i, 1];
            if (map[SearchX, SearchY] != null)
            {
                if (map[SearchX, SearchY].PlaceType != "MOUNTAIN" && map[SearchX, SearchY].PlaceType != "WATER" &&
                map[SearchX, SearchY].PlaceType != "FREEPLACE_EMPTY" && map[SearchX, SearchY].PlaceType != "FREEPLACE_GRASS"
                    && map[SearchX, SearchY].PlaceType != "CALIOPE")
                    return map[SearchX, SearchY];
            }

        }
        return null;
    }

    public void FillMap(List<Place> places, GameObject[] Empty, GameObject[] Grass,
        GameObject[] Water, GameObject[] Mountains)
    {
        FillBorders(Empty, Grass, Water, Mountains, places);
        int randomX = UnityEngine.Random.Range(3, height - 3);
        int randomY = UnityEngine.Random.Range(3, width - 3);
        foreach (var place in places)
        {

            if (place.PlaceType == "AU_HOUSE" || place.PlaceType == "STILLNESS")
                continue;
            else
            {
                randomX = UnityEngine.Random.Range(3, height - 3);
                randomY = UnityEngine.Random.Range(3, width - 3);

                while (!Valid(randomX, randomY)
                    || randomX == PlayerPositionX() || randomY == PlayerPositionY()
                        || map[randomX, randomY].PlaceType == "CALIOPE")
                {
                    randomX = UnityEngine.Random.Range(3, height - 3);
                    randomY = UnityEngine.Random.Range(3, width - 3);
                }

                map[randomX, randomY] = place;
            }
        }

    }

    public void FillBorders(GameObject[] Empty, GameObject[] Grass, GameObject[] Water,
        GameObject[] Mountains, List<Place> places)
    {
        // Player start position
        SetPlayerPosition((height / 2) - 2, width / 2);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int random = UnityEngine.Random.Range(1, 101);
                if (random <= 80)
                {
                    int index = UnityEngine.Random.Range(0, Empty.Length);
                    Place empty = new Place("FREEPLACE_EMPTY", index, "");
                    map[i, j] = empty;
                }
                else
                {
                    int index = UnityEngine.Random.Range(0, Grass.Length);
                    Place grass = new Place("FREEPLACE_GRASS", index, "");
                    map[i, j] = grass;
                }
            }
        }
        // Fill two tiles with all water
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1 || i == 1 || i == height - 2 || j == 0 || j == width - 1 || j == 1 || j == width - 2)
                {
                    int index = 15;
                    int randomIndex = UnityEngine.Random.Range(1, 101);
                    if (randomIndex < 75)
                    {
                        index = 13;
                    }
                    else if (randomIndex < 90)
                    {
                        index = 14;
                    }
                    Place actualWater = new Place("WATER", index, "");
                    map[i, j] = actualWater;
                }
            }
        }

        // Fill borders
        GameObject[] midRight = { Water[16], Water[17], Water[18], Water[19] };
        GameObject[] midLeft = { Water[9], Water[10], Water[11], Water[12] };
        GameObject[] downMid = { Water[3], Water[4], Water[5] };
        GameObject[] upMid = { Water[21], Water[22], Water[23] };

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                // Right
                if (j == 2 && i > 2 && i < height - 3)
                {
                    int index = UnityEngine.Random.Range(0, midRight.Length);
                    if (index == 0)
                        index = 16;
                    if (index == 1)
                        index = 17;
                    if (index == 2)
                        index = 18;
                    if (index == 3)
                        index = 19;
                    Place actualWater = new Place("WATER", index, "");
                    map[i, j] = actualWater;
                }
                // Left
                if (j == width - 3 && i > 2 && i < height - 3)
                {
                    int index = UnityEngine.Random.Range(0, midLeft.Length);
                    if (index == 0)
                        index = 9;
                    if (index == 1)
                        index = 10;
                    if (index == 2)
                        index = 11;
                    if (index == 3)
                        index = 12;
                    Place actualWater = new Place("WATER", index, "");
                    map[i, j] = actualWater;
                }
                // Top
                if (i == 2 && j > 2 && j < width - 3)
                {
                    int index = UnityEngine.Random.Range(0, downMid.Length);
                    if (index == 0)
                        index = 3;
                    if (index == 1)
                        index = 4;
                    if (index == 2)
                        index = 5;
                    Place actualWater = new Place("WATER", index, "");
                    map[i, j] = actualWater;
                }
                // Down
                if (i == width - 3 && j > 2 && j < width - 3)
                {
                    int index = UnityEngine.Random.Range(0, upMid.Length);
                    if (index == 0)
                        index = 21;
                    if (index == 1)
                        index = 22;
                    if (index == 2)
                        index = 23;
                    Place actualWater = new Place("WATER", index, "");
                    map[i, j] = actualWater;
                }
                // Borders
                map[2, 2] = new Place("WATER", 24, "");
                map[2, width - 3] = new Place("WATER", 25, "");
                map[height - 3, 2] = new Place("WATER", 26, "");
                map[height - 3, width - 3] = new Place("WATER", 27, "");

            }
        }

        // Spawn Aunias' house
        Place Au_House = places.FirstOrDefault(p => p.PlaceType == "AU_HOUSE");
        map[height / 2, width / 2] = Au_House;

        int stillnewssSpawn = UnityEngine.Random.Range(1, 5);
        Place Stillness = places.FirstOrDefault(p => p.PlaceType == "STILLNESS");
        int s_X = 0;
        int s_Y = 0;
        if (stillnewssSpawn == 1)
        {
            // Donw right
            s_X = height - 20;
            s_Y = width - 20;
        }
        else if (stillnewssSpawn == 2)
        {
            // Donw left
            s_X = height - 20;
            s_Y = 20;
        }
        else if (stillnewssSpawn == 3)
        {
            // Top right
            s_X = 20;
            s_Y = width - 20;
        }
        else if (stillnewssSpawn == 4)
        {
            // Top left
            s_X = 20;
            s_Y = 20;
        }

        // Stillness place
        map[s_X, s_Y] = Stillness;
        map[s_X, s_Y - 1] = null;
        map[s_X, s_Y - 2] = null;
        map[s_X - 1, s_Y] = null;
        map[s_X - 1, s_Y - 1] = null;
        map[s_X - 1, s_Y - 2] = null;
        map[s_X - 2, s_Y] = null;
        map[s_X - 2, s_Y - 1] = null;
        map[s_X - 2, s_Y - 2] = null;
        map[s_X - 3, s_Y] = null;
        map[s_X - 3, s_Y - 1] = null;
        map[s_X - 3, s_Y - 2] = null;

        // Mountains
        // map[s_X, s_Y] = new Place("MOUNTAIN", 0, "");
        // 1 tile mountais
        map[s_X - 4, s_Y + 3] = new Place("MOUNTAIN", 0, "");
        map[s_X - 2, s_Y + 3] = new Place("MOUNTAIN", 0, "");
        map[s_X + 3, s_Y - 1] = new Place("MOUNTAIN", 0, "");
        map[s_X + 1, s_Y + 1] = new Place("MOUNTAIN", 1, "");
        map[s_X - 1, s_Y + 3] = new Place("MOUNTAIN", 1, "");
        map[s_X - 2, s_Y - 3] = new Place("MOUNTAIN", 1, "");

        // 2 tiles mountains
        map[s_X - 4, s_Y - 2] = new Place("MOUNTAIN", 2, "");
        map[s_X - 4, s_Y - 1] = null;
        map[s_X - 4, s_Y] = new Place("MOUNTAIN", 3, "");
        map[s_X - 4, s_Y + 1] = null;
        map[s_X - 5, s_Y - 3] = new Place("MOUNTAIN", 2, "");
        map[s_X - 5, s_Y - 2] = null;
        map[s_X - 2, s_Y - 5] = new Place("MOUNTAIN", 2, "");
        map[s_X - 2, s_Y - 4] = null;
        map[s_X + 2, s_Y + 1] = new Place("MOUNTAIN", 2, "");
        map[s_X + 2, s_Y + 2] = null;
        map[s_X + 1, s_Y - 2] = new Place("MOUNTAIN", 2, "");
        map[s_X + 1, s_Y - 1] = null;
        map[s_X - 1, s_Y - 4] = new Place("MOUNTAIN", 3, "");
        map[s_X - 1, s_Y - 3] = null;

        // 4 TILES MOUNTAINS
        map[s_X - 1, s_Y + 1] = new Place("MOUNTAIN", 4, "");
        map[s_X - 1, s_Y + 2] = null;
        map[s_X, s_Y + 1] = null;
        map[s_X, s_Y + 2] = null;
        map[s_X - 3, s_Y + 1] = new Place("MOUNTAIN", 4, "");
        map[s_X - 3, s_Y + 2] = null;
        map[s_X - 2, s_Y + 1] = null;
        map[s_X - 2, s_Y + 2] = null;
        map[s_X - 6, s_Y - 1] = new Place("MOUNTAIN", 4, "");
        map[s_X - 6, s_Y] = null;
        map[s_X - 5, s_Y - 1] = null;
        map[s_X - 5, s_Y] = null;
        map[s_X - 4, s_Y - 4] = new Place("MOUNTAIN", 4, "");
        map[s_X - 4, s_Y - 3] = null;
        map[s_X - 3, s_Y - 4] = null;
        map[s_X - 3, s_Y - 3] = null;
        map[s_X, s_Y - 4] = new Place("MOUNTAIN", 4, "");
        map[s_X, s_Y - 3] = null;
        map[s_X + 1, s_Y - 4] = null;
        map[s_X + 1, s_Y - 3] = null;


        // Add starting Caliope
        map[s_X + 1, s_Y] = new Place("CALIOPE", 0, "");
        map[s_X + 2, s_Y] = new Place("CALIOPE", 1, "");
        map[s_X + 3, s_Y] = new Place("CALIOPE", 2, "");
        map[s_X + 4, s_Y] = new Place("CALIOPE", 3, "");
        map[s_X + 2, s_Y - 1] = new Place("CALIOPE", 1, "");
        map[s_X + 2, s_Y - 2] = new Place("CALIOPE", 2, "");
        map[s_X + 3, s_Y + 1] = new Place("CALIOPE", 3, "");

        // Draw mountains
        for (int i = 0; i < (width / 5); i++)
        {
            int randomMountain = UnityEngine.Random.Range(1, 101);
            if (randomMountain <= 50)
            {
                int randomX = UnityEngine.Random.Range(3, height - 3);
                int randomY = UnityEngine.Random.Range(3, width - 3);
                while (!Valid(randomX, randomY) || randomX == PlayerPositionX() || randomY == PlayerPositionY())
                {
                    randomX = UnityEngine.Random.Range(3, height - 3);
                    randomY = UnityEngine.Random.Range(3, width - 3);
                }
                int oneTileMountain = UnityEngine.Random.Range(1, 3);
                if (oneTileMountain == 1)
                    map[randomX, randomY] = new Place("MOUNTAIN", 0, "");
                else
                    map[randomX, randomY] = new Place("MOUNTAIN", 1, "");
            }
            else if (randomMountain <= 90)
            {
                int randomX = UnityEngine.Random.Range(3, height - 3);
                int randomY = UnityEngine.Random.Range(3, width - 3);
                while (!Valid(randomX, randomY) || !Valid(randomX, randomY + 1) || randomX == PlayerPositionX() || randomY == PlayerPositionY())
                {
                    randomX = UnityEngine.Random.Range(3, height - 3);
                    randomY = UnityEngine.Random.Range(3, width - 3);
                }
                int twoTilesMountain = UnityEngine.Random.Range(1, 3);
                if (twoTilesMountain == 1)
                    map[randomX, randomY] = new Place("MOUNTAIN", 2, "");
                else
                    map[randomX, randomY] = new Place("MOUNTAIN", 3, "");
                map[randomX, randomY + 1] = null;
            }
            else
            {
                int randomX = UnityEngine.Random.Range(3, height - 3);
                int randomY = UnityEngine.Random.Range(3, width - 3);
                while (!Valid(randomX, randomY) || !Valid(randomX, randomY + 1) || !Valid(randomX + 1, randomY)
                    || !Valid(randomX + 1, randomY + 1) && randomX == PlayerPositionX() && randomY == PlayerPositionY())
                {
                    randomX = UnityEngine.Random.Range(3, height - 3);
                    randomY = UnityEngine.Random.Range(3, width - 3);
                }
                map[randomX, randomY] = new Place("MOUNTAIN", 4, "");
                map[randomX, randomY + 1] = null;
                map[randomX + 1, randomY] = null;
                map[randomX + 1, randomY + 1] = null;
            }

        }

    }

    public bool Valid(int x, int y)
    {
        if (InBounds(x, y) && Empty(x, y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Empty(int x, int y)
    {
        if (InBounds(x, y))
        {
            if (map[x, y] == null)
            {
                return false;
            }
            else
            {
                Place target = map[x, y];
                if (target.PlaceType == "FREEPLACE_EMPTY" || target.PlaceType == "FREEPLACE_GRASS" || target.PlaceType == "CALIOPE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
    }

    public bool InBounds(int x, int y)
    {
        if (x >= 0 && x < height && y >= 0 && y < width)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerPosition(int x, int y)
    {
        PlayerPointer.X = x;
        PlayerPointer.Y = y;
    }

    public int PlayerPositionX()
    {
        return (int)PlayerPointer.X;
    }
    public int PlayerPositionY()
    {
        return (int)PlayerPointer.Y;
    }

    public Place GetPlace(int x, int y)
    {
        return map[x, y];
    }

    public void clearMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                map[i, j] = null;
            }
        }
    }

    public string longestName()
    {
        string longest = "";
        int dimension = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (map[i, j] != null && map[i, j].Name_ENG.Length > dimension)
                {
                    dimension = map[i, j].Name_ENG.Length;
                }
            }
        }
        for (int i = 0; i < dimension; i++)
        {
            longest += " ";
        }
        return longest;
    }

    public string formated(string name, string longest)
    {
        string formated = "";
        for (int i = 0; i < (longest.Length - name.Length + 1) / 2; i++)
        {
            formated += " ";
        }
        formated += name;
        for (int i = 0; i < (longest.Length - name.Length) / 2; i++)
        {
            formated += " ";
        }
        return formated;
    }
    public override string ToString()
    {
        string mapString = "";
        string longest = longestName();
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {

                if (i == PlayerPointer.X && j == PlayerPointer.Y)
                {
                    mapString += "[ " + formated("PLAYER", longest) + " ] ";
                }

                if (map[i, j] != null)
                {
                    mapString += "[ " + formated(map[i, j].Name_ENG, longest) + " ] ";
                }
                else
                {
                    mapString += "[ " + longestName() + " ] ";
                }

            }
            mapString += "\n";
        }
        return mapString;
    }

}
