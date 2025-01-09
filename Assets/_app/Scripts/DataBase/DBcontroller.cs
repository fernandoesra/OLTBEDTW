using System.Collections.Generic;
using UnityEngine;

public class DBcontroller
{
    ContactService CS;
    List<Effect> effectsList;
    List<Item> itemsList;
    List<Place> placesList;
    List<Event> eventsList;

    public DBcontroller()
    {
        CS = new ContactService();
        CS.CreateTables();
        CS.InsertEffects();
        CS.InsertItems();
        CS.InsertEvents();
        effectsList = CS.FetchEffectsDB();
        itemsList = CS.FetchItemsDB(effectsList);
        eventsList = CS.FetchEventsDB(effectsList);
        CS.InsertPlaces();
        placesList = CS.FetchPlacesDB(itemsList, effectsList);
    }

    public List<Item> GetItems()
    {
        return itemsList;
    }

    public List<Event> GetEvents()
    {
        return eventsList;
    }

    public List<Place> GetPlaces()
    {
        return placesList;
    }

}