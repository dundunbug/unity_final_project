using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemslot;
    private Transform itemslotTemp;
    private Transform pic;

    private void Awake()
    {
        itemslot = transform.Find("Panel2");
        itemslotTemp = itemslot.Find("itemslotTemp");
        pic = itemslot.Find("pic");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        InventoryItemsRefresh();
    }

    public void InventoryItemsRefresh()//refreshing inventory items
    {
        int x = 0, y = 0;
        float itemslotCellSize = 90f;
        foreach (Item item in inventory.GetList())
        {
            RectTransform itemslotRTransform = Instantiate(itemslotTemp, itemslot).GetComponent<RectTransform>();
            itemslotRTransform.gameObject.SetActive(true);



            itemslotRTransform.anchoredPosition = new Vector2(-300 + x * itemslotCellSize, 130 + y * itemslotCellSize);

            Image image = itemslotRTransform.Find("image").GetComponent<Image>();
            Image image2 = itemslotRTransform.Find("pic").GetComponent<Image>();
            image.sprite = item.GetSprite();
            image2.sprite = item.GetSprite();
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }


    }

   /* private void OnMouseOver()
    {
        foreach (Item item in inventory.GetList())
        {
            RectTransform picTransform = Instantiate(pic,itemslot).GetComponent<RectTransform>();
            picTransform.gameObject.SetActive(true);

            picTransform.anchoredPosition = new Vector2(-300 , 130 );



            Image image = picTransform.Find("pic").GetComponent<Image>();
           image.sprite = item.GetSprite();

}
}*/

}
