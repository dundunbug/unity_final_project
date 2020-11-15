using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler ListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        Debug.Log("Inventory Built");
        //AddItem(new Item { itemType = Item.ItemType.Teddy});
        AddItem(new Item { itemType = Item.ItemType.CardBoard});
        AddItem(new Item { itemType = Item.ItemType.Pillow});
        AddItem(new Item { itemType = Item.ItemType.DroppedItem});
        AddItem(new Item { itemType = Item.ItemType.DroppedItem});
        AddItem(new Item { itemType = Item.ItemType.DroppedItem });
        AddItem(new Item { itemType = Item.ItemType.DroppedItem });
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
                break;
            }
        }

        if (!inInventory)
            itemList.Add(item);

        ListChanged?.Invoke(this, EventArgs.Empty);


    }

    public void DeleteItem(Item item)
    {
        foreach (Item itemInList in itemList)
        {
            if (item.itemType == itemInList.itemType)
            {
                if (itemInList.Num > 1)
                {
                    itemInList.Num--;
                }
                else
                    itemList.Remove(itemInList);

                break;

            }
        }

        ListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetList()
    {
        return itemList;
    }
}
