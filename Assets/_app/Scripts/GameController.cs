using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<Event> _Events;
    private List<Place> _Places;
    private List<Item> _Items;
    private DBcontroller DBC;
    private int Day;

    [Header("DEBUG")]
    [SerializeField] bool DEBUG = true;

    [Header("TILES")]
    [SerializeField] GameObject Aunias_house;
    [SerializeField] GameObject[] Caliope;
    [SerializeField] GameObject[] Antennas;
    [SerializeField] GameObject[] Construction;
    [SerializeField] GameObject[] Empty;
    [SerializeField] GameObject[] Forest;
    [SerializeField] GameObject[] Grass;
    [SerializeField] GameObject[] Lonely;
    [SerializeField] GameObject[] Mountains;
    [SerializeField] GameObject[] Shelters;
    [SerializeField] GameObject[] Ruins;
    [SerializeField] GameObject Stillness;
    [SerializeField] GameObject[] Tombs;
    [SerializeField] GameObject[] Town;
    [SerializeField] GameObject[] Treatment;
    [SerializeField] GameObject[] Water;
    [SerializeField] GameObject Well;

    [Header("MAP VALUES")]
    [SerializeField] int Map_X;
    [SerializeField] int Map_Y;
    private Map map;

    [Header("PLAYER")]
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Camera;
    [SerializeField] GameObject[] ActionBtn;
    [SerializeField] Animator AuniaAnimator;
    private Coroutine walkAnimationCoroutine;
    [SerializeField] int MovementUnit = 1;
    [SerializeField] int Health = 10;
    [SerializeField] int Sanity = 10;
    [SerializeField] int Hunger = 10;
    [SerializeField] int Rest = 10;
    [SerializeField] int Actions = 20;
    private int InitialActions;
    private List<Item> Inventory;
    private int PlayerX;
    private int PlayerY;

    [Header("GENERAL GAME INTERFACE")]
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject HealthDisplay;
    [SerializeField] Sprite[] HealthImages;
    [SerializeField] GameObject SanityDisplay;
    [SerializeField] Sprite[] SanityImages;
    [SerializeField] GameObject HungerDisplay;
    [SerializeField] Sprite[] HungerImages;
    [SerializeField] GameObject RestDisplay;
    [SerializeField] Sprite[] RestImages;
    [SerializeField] GameObject MovementDisplay;
    [SerializeField] Sprite[] MovementImages;
    [SerializeField] private StadisticsController stadisticsController;
    [SerializeField] GameObject DeathPanel;

    [Header("INTERACT INTERFACE")]
    [SerializeField] GameObject ExplorePanel;
    [SerializeField] TextMeshProUGUI OptionsText;
    [SerializeField] private Button Option1Btn;
    [SerializeField] private Button Option2Btn;
    [SerializeField] private Button Option3Btn;
    [SerializeField] private Button ConfirmBtn;

    [Header("EVENT INTERFACE")]
    [SerializeField] GameObject EventPanel;
    [SerializeField] TextMeshProUGUI EventText;
    [SerializeField] private Button ConfirmEvent;

    [Header("INVENTORY INTERFACE")]
    [SerializeField] RectTransform InventoryContent;
    [SerializeField] GameObject InventoryItemPrefab;
    [SerializeField] Sprite[] ItemsTileSet;
    [SerializeField] TextMeshProUGUI ItemsDescriptionText;
    [SerializeField] private GameObject UseItemOn;
    [SerializeField] private GameObject UseItemOff;
    [SerializeField] private GameObject EraseItemOn;
    [SerializeField] private GameObject EraseItemOff;
    [SerializeField] private Button UseItemBtn;
    [SerializeField] private Button EraseItemBtn;
    [SerializeField] private Button ConfirmEraseBtn;
    [SerializeField] private GameObject ConfirmErasePanel;
    private bool UsingInventory = false;

    [Header("GENERAL MAP INTERFACE")]
    [SerializeField] private Camera MapCamera;
    [SerializeField] GameObject[] MarkersIcons;
    [SerializeField] Canvas PointersCanvas;
    [SerializeField] GameObject PlayerMarker;

    /**
    * -----------------------------------------------------------------------------------------------------------------
    * Unity methods
    * -----------------------------------------------------------------------------------------------------------------
    */
    void Start()
    {
        DBC = new DBcontroller();
        _Places = DBC.GetPlaces();
        _Events = DBC.GetEvents();
        _Items = DBC.GetItems();
        GenerateGrid();
        Player.transform.position = new Vector3(Map_X / 2, (Map_Y / 2) + 1, 0);
        PlayerX = Map_X / 2;
        PlayerY = (Map_Y / 2) - 2;
        Inventory = new List<Item>();
        Item flamethrowerItem = _Items.Find(item => item.Name_ENG == "Flamethrower");
        Inventory.Add(flamethrowerItem);
        MapCamera.transform.position = new Vector3(Map_X / 2, Map_X / 2, MapCamera.transform.position.z);
        MapCamera.orthographicSize = Map_X / 2;
        DrawMapMarkers();
        InitialActions = Actions;
        Day = 0;
        switch (Language())
        {
            case 0:
                LogAreaController.Instance.AddText("Aunia is now alone. If she moves, the world will move with her. She "
                    + "must return to where it all began. Edereta is waiting.\n");
                break;
            case 1:
                LogAreaController.Instance.AddText("Aunia ahora está sola. Si ella se mueve, el mundo se moverá con ella."
                    + " Debe volver a donde todo empezó. Edereta está esperando.\n");
                break;
        }
        if (DEBUG)
        {
            DrawAllGrid();
            for (int i = 0; i < _Items.Count; i++)
            {
                Inventory.Add(_Items[i].Copy());
            }
            Inventory.Add(_Items[9].Copy());
            Inventory.Add(_Items[9].Copy());
            Inventory.Add(_Items[9].Copy());
            Inventory.Add(_Items[9].Copy());
            Inventory.Add(_Items[9].Copy());
        }
        else
        {
            DrawMove(PlayerX, PlayerY);
        }
    }

    /**
    * -----------------------------------------------------------------------------------------------------------------
    * Game methods
    * -----------------------------------------------------------------------------------------------------------------
    */

    public void DrawMapMarkers()
    {
        RectTransform playerMark = PlayerMarker.GetComponent<RectTransform>();
        playerMark.sizeDelta = new Vector2((float)(Map_X / 12.5), (float)(Map_Y / 12.5));
        int UnityX = 0;
        for (int y = Map_Y - 1; y >= 0; y--)
        {
            int UnityY = 0;
            for (int x = 0; x < Map_X; x++)
            {
                GameObject NewMarker = null;
                Place newPlace = map.GetPlace(y, x);
                if (newPlace != null)
                {
                    string type = newPlace.PlaceType;
                    switch (type)
                    {
                        case "AU_HOUSE":
                            NewMarker = Instantiate(MarkersIcons[1], PointersCanvas.transform);
                            break;
                        case "STILLNESS":
                            NewMarker = Instantiate(MarkersIcons[2], PointersCanvas.transform);
                            break;
                        case "TOMB":
                            NewMarker = Instantiate(MarkersIcons[3], PointersCanvas.transform);
                            break;
                        case "ANTENNA":
                        case "CONSTRUCTION":
                        case "FOREST":
                        case "LONELY":
                        case "SHELTER":
                        case "RUINS":
                        case "TOWN":
                        case "PLANT":
                        case "WELL":
                            NewMarker = Instantiate(MarkersIcons[4], PointersCanvas.transform);
                            break;
                    }

                    if (NewMarker != null)
                    {
                        RectTransform markerRectTransform = NewMarker.GetComponent<RectTransform>();
                        float posX = (-(1920 / 2)) + x * 1;
                        float posY = (-((1080 / 2)) + Map_Y - 1 - y) * 1;
                        markerRectTransform.anchoredPosition = new Vector2(posX, posY);
                        markerRectTransform.sizeDelta = new Vector2((float)(Map_X / 12.5), (float)(Map_Y / 12.5));
                        markerRectTransform.localScale = Vector3.one;

                        NewMarker.name = "MARKER [" + UnityX + "," + UnityY + "]";
                        UnityY++;
                    }

                }

            }
            UnityX++;
        }
    }

    public void FinishGame(int end)
    {
        StadisticsAddEnd(1);
        switch (end)
        {
            case 1:
                // Debug.Log("END 01");
                EndController.Instance.SetEnd(1);
                LoadingScene.LoadScene("End");
                break;
            case 2:
                // Debug.Log("END 02");
                EndController.Instance.SetEnd(2);
                LoadingScene.LoadScene("End");
                break;
        }
    }
    public void OpenInventory()
    {
        UseItemOff.SetActive(true);
        UseItemOn.SetActive(false);
        EraseItemOff.SetActive(true);
        EraseItemOn.SetActive(false);
        if (!UsingInventory)
            ItemsDescriptionText.text = "";
        UsingInventory = true;
        // switch (Language())
        // {
        //     case 0:
        //         Inventory.Sort((x, y) => x.Name_ESP.CompareTo(y.Name_ENG));
        //         break;
        //     case 1:
        //         Inventory.Sort((x, y) => x.Name_ESP.CompareTo(y.Name_ESP));
        //         break;
        // }
        foreach (Transform item in InventoryContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Item i in Inventory)
        {
            // Debug.Log(i);
            GameObject NewPrefab = Instantiate(InventoryItemPrefab);
            NewPrefab.transform.SetParent(InventoryContent, false);
            Transform itemIconTransform = NewPrefab.transform.Find("ItemPrefabIcon");
            var itemIcon = itemIconTransform.GetComponentInChildren<Image>();
            Sprite tileSprite = ItemsTileSet[i.Icon];
            itemIcon.sprite = tileSprite;

            Button itemButton = NewPrefab.GetComponent<Button>();
            itemButton.onClick.RemoveAllListeners();
            itemButton.onClick.AddListener(() => ClickItem(i));
        }

    }

    public void ClickItem(Item item)
    {
        if (item.Name_ENG != "Flamethrower")
        {
            UseItemOff.SetActive(false);
            UseItemOn.SetActive(true);
            EraseItemOff.SetActive(false);
            EraseItemOn.SetActive(true);
            UseItemBtn.onClick.RemoveAllListeners();
            UseItemBtn.onClick.AddListener(() => UseItemInventory(item));
            EraseItemBtn.onClick.RemoveAllListeners();
            EraseItemBtn.onClick.AddListener(() => ConfirmDeleteItem(item));
            switch (Language())
            {
                case 0:
                    ItemsDescriptionText.text = item.Name_ENG + "\n\n" + item.Description_ENG;
                    break;
                case 1:
                    ItemsDescriptionText.text = item.Name_ESP + "\n\n" + item.Description_ESP;
                    break;
            }
        }
        else
        {
            switch (Language())
            {
                case 0:
                    ItemsDescriptionText.text = item.Name_ENG + "\n\n" + item.Description_ENG;
                    break;
                case 1:
                    ItemsDescriptionText.text = item.Name_ESP + "\n\n" + item.Description_ESP;
                    break;
            }
            UseItemOff.SetActive(true);
            UseItemOn.SetActive(false);
            EraseItemOff.SetActive(true);
            EraseItemOn.SetActive(false);
        }

    }

    public void UseItemInventory(Item item)
    {
        // Debug.Log(item);
        switch (Language())
        {
            case 0:
                ItemsDescriptionText.text = "By using item " + item.Name_ENG + ", Aunia has obtained " + item.Effect.Name_ENG + " effect "
                    + ".\n\n" + item.Effect.Description_ENG;
                break;
            case 1:
                ItemsDescriptionText.text = "Al usar el objeto " + item.Name_ESP + ", Aunia ha obtenido el efecto " + item.Effect.Name_ESP
                    + ".\n\n" + item.Effect.Description_ESP;
                break;
        }
        ApplyEffect(item.Effect, item.Amount);
        Inventory.Remove(item);
        OpenInventory();
        StadisticsAddItems(1);
    }

    public void ConfirmDeleteItem(Item item)
    {
        ConfirmErasePanel.SetActive(true);
        ConfirmEraseBtn.onClick.RemoveAllListeners();
        ConfirmEraseBtn.onClick.AddListener(() => DeleteItem(item));
    }

    public void DeleteItem(Item item)
    {
        ConfirmErasePanel.SetActive(false);
        switch (Language())
        {
            case 0:
                ItemsDescriptionText.text = "Aunia has removed " + item.Name_ENG + " from her backpack";
                break;
            case 1:
                ItemsDescriptionText.text = "Aunia ha elminado de su mochila " + item.Name_ESP;
                break;
        }
        Inventory.Remove(item);
        OpenInventory();
    }

    public void CloseInventory()
    {
        UsingInventory = false;
    }

    public void EndGameDeath()
    {
        DeathPanel.SetActive(true);
        StadisticsAddDeaths(1);
    }

    public void CheckDeath()
    {
        if (Health <= 0 || Sanity <= 0 || Hunger <= 0 || Rest <= 0)
            EndGameDeath();
    }

    public void ModifyHealth(int modify)
    {
        for (int i = Math.Abs(modify); i > 0; i--)
        {
            if (modify > 0)
                Health++;
            else
                Health--;
            if (Health >= 8)
                HealthDisplay.GetComponent<Image>().sprite = HealthImages[0];
            else if (Health >= 6)
                HealthDisplay.GetComponent<Image>().sprite = HealthImages[1];
            else if (Health >= 4)
                HealthDisplay.GetComponent<Image>().sprite = HealthImages[2];
            else if (Health >= 1)
                HealthDisplay.GetComponent<Image>().sprite = HealthImages[3];

            switch (Health)
            {
                case 3:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia's body will not withstand another wound.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("El cuerpo de Aunia no aguantará otra herida.\n");
                            break;
                    }
                    break;
                case 5:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia's wounds begin to weigh on her.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Las heridas de Aunia empiezan a pesarle.\n");
                            break;
                    }
                    break;
                case 7:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Some superficial wounds begin to bleed on Aunia's body.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Algunas heridas superficiales empiezan a sangrar por el cuerpo de Aunia.\n");
                            break;
                    }
                    break;
            }
            CheckDeath();
        }
    }

    public void ModifySanity(int modify)
    {
        for (int i = Math.Abs(modify); i > 0; i--)
        {
            if (modify > 0)
                Sanity++;
            else
                Sanity--;
            if (Sanity >= 8)
                SanityDisplay.GetComponent<Image>().sprite = SanityImages[0];
            else if (Sanity >= 6)
                SanityDisplay.GetComponent<Image>().sprite = SanityImages[1];
            else if (Sanity >= 4)
                SanityDisplay.GetComponent<Image>().sprite = SanityImages[2];
            else if (Sanity >= 1)
                SanityDisplay.GetComponent<Image>().sprite = SanityImages[3];

            switch (Sanity)
            {
                case 3:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia's head is spinning, the voices are getting closer. She begins to value the option of doing what they ask of her.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("La cabeza de Aunia le da vueltas, las voces cada vez están más cerca. Empieza a valorar la opción de hacer lo que le piden.\n");
                            break;
                    }
                    break;
                case 5:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Strange voices dream in the distance, they are calling Aunia to join them.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Unas voces extrañas sueñan en la lejanía, están llamando a Aunia para que se una a ellas.\n");
                            break;
                    }
                    break;
                case 7:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia thinks someone is following her, but that's not possible.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia piensa que alguien la está siguiendo, pero eso no es posible.\n");
                            break;
                    }
                    break;
            }
            CheckDeath();
        }
    }

    public void ModifyHunger(int modify)
    {
        for (int i = Math.Abs(modify); i > 0; i--)
        {
            if (modify > 0)
                Hunger++;
            else
                Hunger--;
            if (Hunger >= 8)
                HungerDisplay.GetComponent<Image>().sprite = HungerImages[0];
            else if (Hunger >= 6)
                HungerDisplay.GetComponent<Image>().sprite = HungerImages[1];
            else if (Hunger >= 4)
                HungerDisplay.GetComponent<Image>().sprite = HungerImages[2];
            else if (Hunger >= 1)
                HungerDisplay.GetComponent<Image>().sprite = HungerImages[3];

            switch (Hunger)
            {
                case 3:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia falls to her knees and a greenish liquid comes out of her mouth. She needs to eat something in good condition or she won't last much longer.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia cae de rodillas y de su boca sale un liquido verdoso, necesita comer algo en buen estado o no aguantará mucho mas.\n");
                            break;
                    }
                    break;
                case 5:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia no longer remembers what was the last thing she put in her mouth.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia ya no recuerda qué fue lo último que se llevó a la boca.\n");
                            break;
                    }
                    break;
                case 7:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia's stomach starts making strange sounds.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("El estómago de Aunia empieza a hacer sonidos extraños.\n");
                            break;
                    }
                    break;
            }
            CheckDeath();
        }
    }


    public void ModifyRest(int modify)
    {
        for (int i = Math.Abs(modify); i > 0; i--)
        {
            if (modify > 0)
                Rest++;
            else
                Rest--;
            if (Rest >= 8)
                RestDisplay.GetComponent<Image>().sprite = RestImages[0];
            else if (Rest >= 6)
                RestDisplay.GetComponent<Image>().sprite = RestImages[1];
            else if (Rest >= 4)
                RestDisplay.GetComponent<Image>().sprite = RestImages[2];
            else if (Rest >= 1)
                RestDisplay.GetComponent<Image>().sprite = RestImages[3];
            switch (Rest)
            {
                case 3:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia begins to stumble due to fatigue, she fears she will faint if she doesn't sleep soon.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia empieza a tropezar por el cansancio, teme desmayarse si no duerme pronto.\n");
                            break;
                    }
                    break;
                case 5:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia no longer remembers the last time she slept through the night.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia ya no recuerda cuándo fue la última vez que durmió toda la noche entera.\n");
                            break;
                    }
                    break;
                case 7:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia's eyes begin to close on their own due to fatigue.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Los ojos de Aunia empiezan a cerrarse solos por el cansancio.\n");
                            break;
                    }
                    break;
            }
            CheckDeath();
        }
    }

    public int Language()
    {
        return LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
    }

    public void ModifyActions(int modify)
    {
        for (int i = Math.Abs(modify); i > 0; i--)
        {
            if (modify > 0)
                Actions++;
            else
                Actions--;
            if (Actions >= 16)
                MovementDisplay.GetComponent<Image>().sprite = MovementImages[0];
            else if (Actions >= 11)
                MovementDisplay.GetComponent<Image>().sprite = MovementImages[1];
            else if (Actions >= 6)
                MovementDisplay.GetComponent<Image>().sprite = MovementImages[2];
            else if (Actions >= 1)
                MovementDisplay.GetComponent<Image>().sprite = MovementImages[3];
            else if (Actions == 0)
                MovementDisplay.GetComponent<Image>().sprite = MovementImages[4];

            switch (Actions)
            {
                case 0:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia has lost the strength to walk.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia se ha quedado sin fuerzas para caminar.\n");
                            break;
                    }
                    break;
                case 5:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia barely has the strength to move.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia apenas tiene fuerzas para moverse.\n");
                            break;
                    }
                    break;
                case 10:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia begins to feel exhausted.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia empieza a sentirse agotada.\n");
                            break;
                    }
                    break;
                case 15:
                    switch (Language())
                    {
                        case 0:
                            LogAreaController.Instance.AddText("Aunia shows some signs of fatigue.\n");
                            break;
                        case 1:
                            LogAreaController.Instance.AddText("Aunia muestra algún síntoma de cansancio.\n");
                            break;
                    }
                    break;
            }
        }

    }

    public void StadisticsAddStep(int add)
    {
        stadisticsController.SaveData(add, 0, 0, 0, 0);
    }

    public void StadisticsAddTurn(int add)
    {
        stadisticsController.SaveData(0, add, 0, 0, 0);
    }

    public void StadisticsAddItems(int add)
    {
        stadisticsController.SaveData(0, 0, add, 0, 0);
    }

    public void StadisticsAddDeaths(int add)
    {
        stadisticsController.SaveData(0, 0, 0, add, 0);
    }
    public void StadisticsAddEnd(int add)
    {
        stadisticsController.SaveData(0, 0, 0, 0, add);
    }

    public void EndTurn()
    {
        Day++;
        switch (Language())
        {
            case 0:
                LogAreaController.Instance.AddText("\nDay " + Day + " dawns. A new opportunity begins for Aunia.\n");
                break;
            case 1:
                LogAreaController.Instance.AddText("\nAmanece el día " + Day + ".Una nueva oportunidad empieza para Aunia.\n");
                break;
        }
        map.ExtendCaliope();
        DrawMove(PlayerX, PlayerY);
        StadisticsAddTurn(1);
        Actions = InitialActions;
        MovementDisplay.GetComponent<Image>().sprite = MovementImages[0];
        RandomEvent();
        ModifyActions(0);
        AllowInteract();
    }

    public void RandomEvent()
    {
        Event actual = _Events[UnityEngine.Random.Range(0, _Events.Count)];
        GamePanel.SetActive(false);
        EventPanel.SetActive(true);
        switch (Language())
        {
            case 0:
                EventText.text = actual.Description_ENG + "\n\nAunia has obtained " + actual.Effect.Name_ENG + " effect."
                    + "\n" + actual.Effect.Description_ENG; ;
                break;
            case 1:
                EventText.text = actual.Description_ESP + "\n\nAunia ha obtenido el efecto " + actual.Effect.Name_ESP + ".\n"
                    + actual.Effect.Description_ESP;
                break;
        }
        ConfirmEvent.onClick.RemoveAllListeners();
        ConfirmEvent.onClick.AddListener(() => GamePanel.SetActive(true));
        ConfirmEvent.onClick.AddListener(() => EventPanel.SetActive(false));
        if (actual.Effect != null)
        {
            ConfirmEvent.onClick.AddListener(() => ApplyEffect(actual.Effect, actual.Amount));
        }
    }

    public void ApplyEffect(Effect toApplay, int amount)
    {
        string key = toApplay.KeyEffect.Split("_")[0].Trim();
        string effect = toApplay.KeyEffect.Split("_")[1].Trim();
        // Debug.Log("APPLY EFFECT MODEY: " + key + " EFFECT: " + effect);
        if (key == "add")
        {
            switch (effect)
            {
                case "health":
                    ModifyHealth(amount);
                    break;
                case "mind":
                    ModifySanity(amount);
                    break;
                case "hunger":
                    ModifyHunger(amount);
                    break;
                case "rest":
                    ModifyRest(amount);
                    break;
                case "action":
                    ModifyActions(amount);
                    break;
            }
        }
        else if (key == "remove")
        {
            switch (effect)
            {
                case "health":
                    ModifyHealth(-amount);
                    break;
                case "mind":
                    ModifySanity(-amount);
                    break;
                case "hunger":
                    ModifyHunger(-amount);
                    break;
                case "rest":
                    ModifyRest(-amount);
                    break;
                case "action":
                    ModifyActions(-amount);
                    break;
            }
        }
    }

    public void Election(int election, Place actualPlace, Item actualItem, Effect actualEffect)
    {
        if (actualPlace.PlaceType == "STILLNESS")
        {
            switch (election)
            {
                case 1:
                    FinishGame(1);
                    break;
                case 2:
                    FinishGame(2);
                    break;
            }
        }
        else
        {
            // Debug.Log("Item: " + actualItem + "\nEffect: " + actualEffect);
            switch (election)
            {
                case 1:
                    switch (Language())
                    {
                        case 0:
                            OptionsText.text = actualPlace.Answer1_ENG;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\nAunia has found the object " + actualItem.Name_ENG + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\nAunia receives the effect " + actualEffect.Name_ENG + "!"
                                    + "\n" + actualEffect.Description_ENG;
                            break;
                        case 1:
                            OptionsText.text = actualPlace.Answer1_ESP;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\n¡Aunia ha encontrado el objeto " + actualItem.Name_ESP + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\n¡Aunia recibe el efecto " + actualEffect.Name_ESP + "!"
                                    + "\n" + actualEffect.Description_ESP;
                            break;
                    }
                    break;
                case 2:
                    switch (Language())
                    {
                        case 0:
                            OptionsText.text = actualPlace.Answer2_ENG;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\nAunia has found the object " + actualItem.Name_ENG + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\nAunia receives the effect " + actualEffect.Name_ENG + "!"
                                    + "\n" + actualEffect.Description_ENG;
                            break;
                        case 1:
                            OptionsText.text = actualPlace.Answer2_ESP;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\n¡Aunia ha encontrado el objeto " + actualItem.Name_ESP + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\n¡Aunia recibe el efecto " + actualEffect.Name_ESP + "!"
                                    + "\n" + actualEffect.Description_ESP;
                            break;
                    }
                    break;
                case 3:
                    switch (Language())
                    {
                        case 0:
                            OptionsText.text = actualPlace.Answer3_ENG;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\nAunia has found the object " + actualItem.Name_ENG + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\nAunia receives the effect " + actualEffect.Name_ENG + "!"
                                    + "\n" + actualEffect.Description_ENG;
                            break;
                        case 1:
                            OptionsText.text = actualPlace.Answer3_ESP;
                            if (actualItem != null)
                            {
                                OptionsText.text += "\n\n¡Aunia ha encontrado el objeto " + actualItem.Name_ESP + "!";
                                Inventory.Add(actualItem.Copy());
                            }
                            if (actualEffect != null)
                                OptionsText.text += "\n\n¡Aunia recibe el efecto " + actualEffect.Name_ESP + "!"
                                    + "\n" + actualEffect.Description_ESP;
                            break;
                    }
                    break;
            }
            Option1Btn.gameObject.SetActive(false);
            Option2Btn.gameObject.SetActive(false);
            Option3Btn.gameObject.SetActive(false);
            ConfirmBtn.gameObject.SetActive(true);
            actualPlace.YetExplored = true;
        }
    }

    public void Interact()
    {
        Option1Btn.gameObject.SetActive(false);
        Option2Btn.gameObject.SetActive(false);
        Option3Btn.gameObject.SetActive(false);
        ConfirmBtn.gameObject.SetActive(false);
        Place NearPlace = map.SearchForPlaces();
        if (NearPlace != null)
        {
            if (NearPlace.YetExplored)
            {
                GamePanel.SetActive(false);
                ExplorePanel.SetActive(true);
                switch (Language())
                {
                    case 0:
                        OptionsText.text = "\nAunia has already explored this place.";
                        break;
                    case 1:
                        OptionsText.text = "\nAunia ya ha explorado este lugar.";
                        break;
                }
                ConfirmBtn.gameObject.SetActive(true);
            }
            else
            {
                // Debug.Log(NearPlace);
                GamePanel.SetActive(false);
                ExplorePanel.SetActive(true);
                switch (Language())
                {
                    case 0:
                        OptionsText.text = NearPlace.Description_ENG;
                        break;
                    case 1:
                        OptionsText.text = NearPlace.Description_ESP;
                        break;
                }

                if (NearPlace.Option1_ENG != null && NearPlace.Option1_ENG != "")
                {
                    Option1Btn.gameObject.SetActive(true);
                    TextMeshProUGUI Text1 = Option1Btn.GetComponentInChildren<TextMeshProUGUI>();
                    switch (Language())
                    {
                        case 0:
                            Text1.SetText(NearPlace.Option1_ENG);
                            break;
                        case 1:
                            Text1.SetText(NearPlace.Option1_ESP);
                            break;
                    }
                    Item item = NearPlace.Item1;
                    Effect effect = NearPlace.Effect1;
                    Option1Btn.onClick.RemoveAllListeners();
                    Option1Btn.onClick.AddListener(() => Election(1, NearPlace, item, effect));
                }

                if (NearPlace.Option2_ENG != null && NearPlace.Option2_ENG != "")
                {
                    Option2Btn.gameObject.SetActive(true);
                    TextMeshProUGUI Text2 = Option2Btn.GetComponentInChildren<TextMeshProUGUI>();
                    switch (Language())
                    {
                        case 0:
                            Text2.SetText(NearPlace.Option2_ENG);
                            break;
                        case 1:
                            Text2.SetText(NearPlace.Option2_ESP);
                            break;
                    }
                    Item item = NearPlace.Item2;
                    Effect effect = NearPlace.Effect2;
                    Option2Btn.onClick.RemoveAllListeners();
                    Option2Btn.onClick.AddListener(() => Election(2, NearPlace, item, effect));
                }

                if (NearPlace.Option3_ENG != null && NearPlace.Option3_ENG != "")
                {
                    Option3Btn.gameObject.SetActive(true);
                    TextMeshProUGUI Text3 = Option3Btn.GetComponentInChildren<TextMeshProUGUI>();
                    switch (Language())
                    {
                        case 0:
                            Text3.SetText(NearPlace.Option3_ENG);
                            break;
                        case 1:
                            Text3.SetText(NearPlace.Option3_ESP);
                            break;
                    }
                    Item item = NearPlace.Item3;
                    Effect effect = NearPlace.Effect3;
                    Option3Btn.onClick.RemoveAllListeners();
                    Option3Btn.onClick.AddListener(() => Election(3, NearPlace, item, effect));
                }
            }
        }
    }

    public void AllowInteract()
    {
        Place NearPlace = map.SearchForPlaces();
        if (NearPlace != null && Actions > 0)
        {
            ActionBtn[0].SetActive(false);
            ActionBtn[1].SetActive(true);
        }
        else
        {
            ActionBtn[0].SetActive(true);
            ActionBtn[1].SetActive(false);
        }
    }

    public IEnumerator WalkAnimation()
    {
        AuniaAnimator.SetBool("Movement", true);
        yield return new WaitForSeconds(1.1f);
        AuniaAnimator.SetBool("Movement", false);
    }


    public void MoveUp()
    {
        if (Actions > 0)
        {
            int TargetX = map.PlayerPositionX() - 1;
            if (map.Valid(TargetX, map.PlayerPositionY()))
            {
                if (map.GetPlace(map.PlayerPositionX() - 1, map.PlayerPositionY()).PlaceType == "CALIOPE")
                {
                    int newCaliope = UnityEngine.Random.Range(1, 4);
                    if (newCaliope == 1)
                    {
                        ModifySanity(-1);
                        switch (Language())
                        {
                            case 0:
                                LogAreaController.Instance.AddText("\nAunia's mind suffers the effects of walking through Calliope's flowers.\n");
                                break;
                            case 1:
                                LogAreaController.Instance.AddText("La mente de Aunia sufre los efectos de andar por las flores de Caliope.\n");
                                break;
                        }
                    }
                }
                ModifyActions(-1);
                Player.transform.position += Vector3.up * MovementUnit;
                map.SetPlayerPosition(TargetX, map.PlayerPositionY());
                AllowInteract();
                StadisticsAddStep(1);
                if (walkAnimationCoroutine != null)
                    StopCoroutine(walkAnimationCoroutine);
                walkAnimationCoroutine = StartCoroutine(WalkAnimation());
                PlayerY--;
                if (DEBUG)
                    DebugMove();
                else
                    DrawMove(PlayerX, PlayerY);
            }
        }
    }

    public void MoveDown()
    {
        if (Actions > 0)
        {
            int TargetX = map.PlayerPositionX() + 1;
            if (map.Valid(TargetX, map.PlayerPositionY()))
            {
                if (map.GetPlace(map.PlayerPositionX() + 1, map.PlayerPositionY()).PlaceType == "CALIOPE")
                {
                    int newCaliope = UnityEngine.Random.Range(1, 4);
                    if (newCaliope == 1)
                    {
                        ModifySanity(-1);
                        switch (Language())
                        {
                            case 0:
                                LogAreaController.Instance.AddText("\nAunia's mind suffers the effects of walking through Calliope's flowers.\n");
                                break;
                            case 1:
                                LogAreaController.Instance.AddText("La mente de Aunia sufre los efectos de andar por las flores de Caliope.\n");
                                break;
                        }
                    }
                }
                ModifyActions(-1);
                Player.transform.position += Vector3.down * MovementUnit;
                map.SetPlayerPosition(TargetX, map.PlayerPositionY());
                AllowInteract();
                StadisticsAddStep(1);
                if (walkAnimationCoroutine != null)
                    StopCoroutine(walkAnimationCoroutine);
                walkAnimationCoroutine = StartCoroutine(WalkAnimation());
                PlayerY++;
                if (DEBUG)
                    DebugMove();
                else
                    DrawMove(PlayerX, PlayerY);
            }
        }
    }

    public void MoveLeft()
    {
        if (Actions > 0)
        {
            int TargetY = map.PlayerPositionY() - 1;
            if (map.Valid(map.PlayerPositionX(), TargetY))
            {
                if (map.GetPlace(map.PlayerPositionX(), map.PlayerPositionY() - 1).PlaceType == "CALIOPE")
                {
                    int newCaliope = UnityEngine.Random.Range(1, 4);
                    if (newCaliope == 1)
                    {
                        ModifySanity(-1);
                        switch (Language())
                        {
                            case 0:
                                LogAreaController.Instance.AddText("\nAunia's mind suffers the effects of walking through Calliope's flowers.\n");
                                break;
                            case 1:
                                LogAreaController.Instance.AddText("La mente de Aunia sufre los efectos de andar por las flores de Caliope.\n");
                                break;
                        }
                    }
                }
                ModifyActions(-1);
                Player.transform.position += Vector3.left * MovementUnit;
                map.SetPlayerPosition(map.PlayerPositionX(), TargetY);
                AllowInteract();
                StadisticsAddStep(1);
                if (walkAnimationCoroutine != null)
                    StopCoroutine(walkAnimationCoroutine);
                walkAnimationCoroutine = StartCoroutine(WalkAnimation());
                PlayerX--;
                if (DEBUG)
                    DebugMove();
                else
                    DrawMove(PlayerX, PlayerY);
            }
        }
    }

    public void MoveRight()
    {
        if (Actions > 0)
        {
            int TargetY = map.PlayerPositionY() + 1;
            if (map.Valid(map.PlayerPositionX(), TargetY))
            {
                if (map.GetPlace(map.PlayerPositionX(), map.PlayerPositionY() + 1).PlaceType == "CALIOPE")
                {
                    int newCaliope = UnityEngine.Random.Range(1, 4);
                    if (newCaliope == 1)
                    {
                        ModifySanity(-1);
                        switch (Language())
                        {
                            case 0:
                                LogAreaController.Instance.AddText("\nAunia's mind suffers the effects of walking through Calliope's flowers.\n");
                                break;
                            case 1:
                                LogAreaController.Instance.AddText("La mente de Aunia sufre los efectos de andar por las flores de Caliope.\n");
                                break;
                        }
                    }
                }
                ModifyActions(-1);
                Player.transform.position += Vector3.right * MovementUnit;
                map.SetPlayerPosition(map.PlayerPositionX(), TargetY);
                AllowInteract();
                StadisticsAddStep(1);
                if (walkAnimationCoroutine != null)
                    StopCoroutine(walkAnimationCoroutine);
                walkAnimationCoroutine = StartCoroutine(WalkAnimation());
                PlayerX++;
                if (DEBUG)
                    DebugMove();
                else
                    DrawMove(PlayerX, PlayerY);
            }
        }

    }

    private void GenerateGrid()
    {
        map = new Map(Map_X, Map_Y);
        map.FillMap(_Places, Empty, Grass, Water, Mountains);
        // Debug.Log(map);
    }

    private void DrawMove(int centerX, int centerY)
    {
        foreach (Transform objetoHijo in transform)
        {
            Destroy(objetoHijo.gameObject);
        }
        DrawGridAroundPoint(centerX, centerY);
    }

    private void DrawGridAroundPoint(int centerX, int centerY)
    {
        int startX = Mathf.Clamp(centerX - 10, 0, Map_X - 1);
        int endX = Mathf.Clamp(centerX + 10, 0, Map_X - 1);
        int startY = Mathf.Clamp(centerY - 10, 0, Map_Y - 1);
        int endY = Mathf.Clamp(centerY + 10, 0, Map_Y - 1);

        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                GameObject NewTile = null;
                Place newPlace = map.GetPlace(y, x);
                if (newPlace != null)
                {
                    string type = newPlace.PlaceType;
                    int icon = newPlace.Icon;
                    switch (type)
                    {
                        case "AU_HOUSE":
                            NewTile = Instantiate(Aunias_house, transform);
                            break;
                        case "ANTENNA":
                            NewTile = Instantiate(Antennas[icon], transform);
                            break;
                        case "CONSTRUCTION":
                            NewTile = Instantiate(Construction[icon], transform);
                            break;
                        case "FOREST":
                            NewTile = Instantiate(Forest[icon], transform);
                            break;
                        case "LONELY":
                            NewTile = Instantiate(Lonely[icon], transform);
                            break;
                        case "SHELTER":
                            NewTile = Instantiate(Shelters[icon], transform);
                            break;
                        case "RUINS":
                            NewTile = Instantiate(Ruins[icon], transform);
                            break;
                        case "TOMB":
                            NewTile = Instantiate(Tombs[icon], transform);
                            break;
                        case "TOWN":
                            NewTile = Instantiate(Town[icon], transform);
                            break;
                        case "PLANT":
                            NewTile = Instantiate(Treatment[icon], transform);
                            break;
                        case "WELL":
                            NewTile = Instantiate(Well, transform);
                            break;
                        case "STILLNESS":
                            NewTile = Instantiate(Stillness, transform);
                            break;
                        case "CALIOPE":
                            NewTile = Instantiate(Caliope[icon], transform);
                            break;
                        /**
                        * -----------------------------------------------------------------
                        * EMPTY SECTIONS
                        * -----------------------------------------------------------------
                        **/
                        case "MOUNTAIN":
                            NewTile = Instantiate(Mountains[icon], transform);
                            break;
                        case "WATER":
                            NewTile = Instantiate(Water[icon], transform);
                            break;
                        case "FREEPLACE_EMPTY":
                            NewTile = Instantiate(Empty[icon], transform);
                            break;
                        case "FREEPLACE_GRASS":
                            NewTile = Instantiate(Grass[icon], transform);
                            break;
                        case null:
                            break;
                        default:
                            NewTile = Instantiate(Empty[1], transform);
                            break;
                    }

                    if (NewTile != null)
                    {
                        float posX = x * 1;
                        float posY = (Map_Y - 1 - y) * 1;
                        if (type == "MOUNTAIN" && (icon == 2 || icon == 3))
                            posX += 0.5f;
                        if (type == "MOUNTAIN" && (icon == 4))
                        {
                            posX += 0.5f;
                            posY -= 0.5f;
                        }
                        if (type == "STILLNESS")
                        {
                            posX -= 1f;
                            posY += 1.5f;
                        }
                        NewTile.transform.position = new Vector2(posX, posY);
                        NewTile.name = "U:[" + y + "," + x + "] - A:[" + y + "," + x + "]";
                    }
                }
            }
        }
    }

    public void DebugMove()
    {
        foreach (Transform objetoHijo in transform)
        {
            Destroy(objetoHijo.gameObject);
        }
        DrawAllGrid();
    }

    private void DrawAllGrid()
    {

        int UnityX = 0;
        for (int y = Map_Y - 1; y >= 0; y--)
        {
            int UnityY = 0;
            for (int x = 0; x < Map_X; x++)
            {
                GameObject NewTile = null;
                Place newPlace = map.GetPlace(y, x);
                if (newPlace != null)
                {
                    string type = newPlace.PlaceType;
                    int icon = newPlace.Icon;
                    switch (type)
                    {
                        case "AU_HOUSE":
                            NewTile = Instantiate(Aunias_house, transform);
                            break;
                        case "ANTENNA":
                            NewTile = Instantiate(Antennas[icon], transform);
                            break;
                        case "CONSTRUCTION":
                            NewTile = Instantiate(Construction[icon], transform);
                            break;
                        case "FOREST":
                            NewTile = Instantiate(Forest[icon], transform);
                            break;
                        case "LONELY":
                            NewTile = Instantiate(Lonely[icon], transform);
                            break;
                        case "SHELTER":
                            NewTile = Instantiate(Shelters[icon], transform);
                            break;
                        case "RUINS":
                            NewTile = Instantiate(Ruins[icon], transform);
                            break;
                        case "TOMB":
                            NewTile = Instantiate(Tombs[icon], transform);
                            break;
                        case "TOWN":
                            NewTile = Instantiate(Town[icon], transform);
                            break;
                        case "PLANT":
                            NewTile = Instantiate(Treatment[icon], transform);
                            break;
                        case "WELL":
                            NewTile = Instantiate(Well, transform);
                            break;
                        case "STILLNESS":
                            NewTile = Instantiate(Stillness, transform);
                            break;
                        case "CALIOPE":
                            NewTile = Instantiate(Caliope[icon], transform);
                            break;
                        /**
                        * -----------------------------------------------------------------
                        * EMPTY SECTIONS
                        * -----------------------------------------------------------------
                        **/
                        case "MOUNTAIN":
                            NewTile = Instantiate(Mountains[icon], transform);
                            break;
                        case "WATER":
                            NewTile = Instantiate(Water[icon], transform);
                            break;
                        case "FREEPLACE_EMPTY":
                            NewTile = Instantiate(Empty[icon], transform);
                            break;
                        case "FREEPLACE_GRASS":
                            NewTile = Instantiate(Grass[icon], transform);
                            break;
                        case null:
                            break;
                        default:
                            NewTile = Instantiate(Empty[1], transform);
                            break;
                    }

                    if (NewTile != null)
                    {
                        float posX = x * 1;
                        float posY = (Map_Y - 1 - y) * 1;
                        if (type == "MOUNTAIN" && (icon == 2 || icon == 3))
                            posX += 0.5f;
                        if (type == "MOUNTAIN" && (icon == 4))
                        {
                            posX += 0.5f;
                            posY -= 0.5f;
                        }
                        if (type == "STILLNESS")
                        {
                            posX -= 1f;
                            posY += 1.5f;
                        }
                        NewTile.transform.position = new Vector2(posX, posY);
                        NewTile.name = "U:[" + UnityY + "," + UnityX + "] - A:[" + y + "," + x + "]";
                        UnityY++;
                    }

                }

            }
            UnityX++;
        }

    }

}
