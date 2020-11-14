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
        AddItem(new Item { itemType = Item.ItemType.Teddy});
        AddItem(new Item { itemType = Item.ItemType.CardBoard});
        AddItem(new Item { itemType = Item.ItemType.Pillow});
        AddItem(new Item { itemType = Item.ItemType.DroppedItem});
        AddItem(new Item { itemType = Item.ItemType.DroppedItem});
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        bool inInventory = false;
        foreach (Item itemInList in itemList)
        {
            if (item.itemType == itemInList.itemType)
            {
                itemInList.Num++;
                inInventory = true;
            }
        }

        if (!inInventory)
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
