using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    public Transform itemslot;
    public Transform itemslotTemp;


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        InventoryItemsRefresh();
    }

    public void InventoryItemsRefresh()//refreshing inventory items
    {
        int x = 0, y = 0;
        int i = 0;
        float itemslotCellSize = 90f;
        foreach (Item item in inventory.GetList())
        {
            i++;
            RectTransform itemslotRTransform = Instantiate(itemslotTemp, itemslot).GetComponent<RectTransform>();            

            itemslotRTransform.gameObject.SetActive(true);

            itemslotRTransform.anchoredPosition = new Vector2(-300 + x * itemslotCellSize, 130 + y * itemslotCellSize);

            itemslotRTransform.GetComponent<ShowInfo>().SetType(item);
            
            Image image = itemslotRTransform.Find("image").GetComponent<Image>();

            image.sprite = item.GetSprite();
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
        

    }

   
}




