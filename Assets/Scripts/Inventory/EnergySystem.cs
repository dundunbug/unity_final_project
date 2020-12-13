using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class EnergySystem : MonoBehaviour
{
    public GameObject panel_droppedItem;
    public Upagradenum upagradeNum;
    public Inventory inventory;

    public Text text1;
    public int num;
    private int selected;


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
    // Start is called before the first frame update
    void Start()
    {
        //num = 
        panel_droppedItem.SetActive(false);
        text1.text = num + "";
        upagradeNum.num = 0;
    }

    public void Close_panel()
    {
        panel_droppedItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNum(int n)
    {
        num = n;
        text1.text = num.ToString();
    } 

    public void DecreaseIntoEnergy()
    {
        if (num > 0)
        {
            num--;
            text1.text = num + "";
            upagradeNum.IncreaseNum();
            inventory.DeleteItem(new Item { itemType = Item.ItemType.DroppedItem });
        }
    }

    public void DecreaseIntoItem()
    {
        if (num > 0)
        {

            switch (selected)
            {
                default:
                case 0:
                    inventory.AddItem(new Item { itemType = Item.ItemType.Bomb_L });
                    break;
                case 1:
                    inventory.AddItem(new Item { itemType = Item.ItemType.Bomb_Timer });
                    break;
                case 2:
                    inventory.AddItem(new Item { itemType = Item.ItemType.Teddy });
                    break;
                case 3:
                    inventory.AddItem(new Item { itemType = Item.ItemType.TransferGate });
                    break;
                case 4:
                    inventory.AddItem(new Item { itemType = Item.ItemType.Bottle });
                    break;
                case 5:
                    inventory.AddItem(new Item { itemType = Item.ItemType.Pillow });
                    break;
            }


            num--;
            text1.text = num + "";
            inventory.DeleteItem(new Item { itemType = Item.ItemType.DroppedItem }) ;
        }
        
    }

    public void ItemSelected(int type)
    {
        if (num==0) return;
        selected = type;

    }
}
