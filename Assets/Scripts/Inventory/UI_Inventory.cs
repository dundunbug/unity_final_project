using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UI_Inventory : MonoBehaviour
{
    public Inventory inventory;
    public GameData gameData;//SaveData
    public Transform itemslot;
    public Transform itemslotTemp;
    public Transform back;

    public Text T;


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        /*Save Data*/
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        gameData.inventory = inventory;

        /*subscribe event listChange*/
        inventory.ListChanged += Inventory_ListChanged;
        InventoryItemsRefresh();
    }

    private void Inventory_ListChanged(object sender, EventArgs e)
    {
        InventoryItemsRefresh();
    }

    public void InventoryItemsRefresh()//refreshing inventory items
    {
        int x = 0, y = 0;
        int i = 0;
        float itemslotCellSize = 90f;

        foreach (Transform child in itemslot)
        {
            if (child == itemslotTemp || child == back) continue;
            Destroy(child.gameObject);
        }



        foreach (Item item in inventory.GetList())
        {
            i++;
            RectTransform itemslotRTransform = Instantiate(itemslotTemp, itemslot).GetComponent<RectTransform>();

            itemslotRTransform.gameObject.SetActive(true);

            itemslotRTransform.anchoredPosition = new Vector2(-300 + x * itemslotCellSize, 130 + y * itemslotCellSize);

            itemslotRTransform.GetComponent<ShowInfo>().SetType(item, inventory);

            Image image = itemslotRTransform.Find("image").GetComponent<Image>();

            image.sprite = item.GetSprite();
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }

            /*SetnumText*/
            T = itemslotRTransform.Find("Itemnum").GetComponent<Text>();
            T.text = item.Num.ToString();

        }
        
    

    }

   
}




