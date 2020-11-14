using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    public GameObject panel_droppedItem;
    public Upagradenum upagradeNum;
    public Inventory inventory;

    public Text text1;
    public int num;


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }
    // Start is called before the first frame update
    void Start()
    {
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
    } 

    public void DecreaseIntoEnergy()
    {
        if (num > 0)
        {
            num--;
            text1.text = num + "";
            upagradeNum.IncreaseNum();
        }
    }

    public void DecreaseIntoItem()
    {
        if (num > 0)
        {
            num--;
            text1.text = num + "";
        }
    }

    public void ItemSelected(int type)
    {

        switch (type)
        {
            default:
            case 1: break;
        }
            

    }
}
