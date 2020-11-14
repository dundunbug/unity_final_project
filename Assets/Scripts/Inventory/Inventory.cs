using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
        Debug.Log("Inventory Built");
        AddItem(new Item { itemType = Item.ItemType.Teddy, Num = 1 });
        AddItem(new Item { itemType = Item.ItemType.CardBoard, Num = 1 });
        AddItem(new Item { itemType = Item.ItemType.Pillow, Num = 1 });
        AddItem(new Item { itemType = Item.ItemType.DroppedItem, Num = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
       // if(item.ItemType == )
        itemList.Add(item);
    }

    public void DeleteItem(Item item)
    {
        
        itemList.Remove(item);
    }

    public List<Item> GetList()
    {
        return itemList;
    }
}
