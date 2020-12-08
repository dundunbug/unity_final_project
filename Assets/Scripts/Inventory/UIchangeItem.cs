using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class UIchangeItem : MonoBehaviour
{
    player player_script;
    Inventory inventory;
    List<Item> throwList = new List<Item>();
    List<Item> dropList = new List<Item>();
    string throwType="";
    string dropType="";
    public Image itemImage;
    public bool isThrow = true;
    public Text itemNumText;

    public string[] item_types = new string[] {"Bomb_L","Bomb_Timer","Bomb_S","Carton",
    "Lego","TransferGate","CardBoard","Pillow","Bottle","Teddy"};
    public List<GameObject> item_prehabs = new List<GameObject>();
    public string[] throw_types = new string[] {"Bomb_L","Bomb_Timer","Bomb_S","Carton"
    ,"TransferGate","Pillow","Bottle","Teddy"};
    public string[] drop_types = new string[] {"Bomb_L","Bomb_Timer","Carton",
    "Lego","TransferGate","CardBoard","Pillow"};
    List<Item.ItemType> Types = new List<Item.ItemType> {
        Item.ItemType.Bomb_L,Item.ItemType.Bomb_Timer,Item.ItemType.Bomb_S,
        Item.ItemType.Carton,Item.ItemType.Lego,Item.ItemType.TransferGate,
        Item.ItemType.CardBoard,Item.ItemType.Pillow,
        Item.ItemType.Bottle,Item.ItemType.Teddy,
        Item.ItemType.DroppedItem
    };
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player>();
        inventory = player_script.inventory;
    }
    public void checkItemNum(){
        inventory = player_script.inventory;
        foreach (Item item in inventory.GetList()){
            print(item.itemType);
            print(item.GetSprite());
            print(item.Num);
            // itemImage.sprite = item.GetSprite();
        }
    }

    public void switchItem(bool right){
        List<Item> itemList;
        string itemType;
        if (isThrow){
            itemList = throwList;
            itemType = throwType;
        }else{ 
            itemList = dropList;
            itemType = dropType;
        }
        int index = itemList.Count;
        if (itemList.Count >= 1){
            for (int i = 0; i < itemList.Count; i++){
                Item item = itemList[i];
                if (item.itemType.ToString().Equals(itemType)){
                    index = i;
                    break;
                }
            }
            int nextIndex = -1;
            if (right && itemList.Count >1){
                if (index+1 < itemList.Count){
                    nextIndex = index+1;
                }else if (index+1 == itemList.Count ){
                    nextIndex = 0;
                }
            }else if (!right && itemList.Count >1){
                if (index == 0){
                    nextIndex = itemList.Count -1;
                }else if (index > 0){
                    nextIndex = index-1;
                }
            }
            if (nextIndex != -1){
                itemImage.sprite = itemList[nextIndex].GetSprite();
                itemNumText.text = itemList[nextIndex].Num.ToString();
                if (isThrow){
                    throwType = itemList[nextIndex].itemType.ToString();
                    switchPlayerItem(throwType);
                }else{
                    dropType = itemList[nextIndex].itemType.ToString();
                    switchPlayerItem(dropType);
                }
            }
        }else{
            throwType = "";
            dropType = "";
            itemImage.sprite = null;
            itemNumText.text = "";
            switchPlayerItem("");
        }

        
    }

    public void refreshList(){
        inventory = player_script.inventory;
        throwList.Clear();
        dropList.Clear();
        string itemType;
        if (isThrow){
            if (throwList.Count == 0){
                throwType = "";
            }
            itemType = throwType;
        }else{ 
            if (dropList.Count == 0){
                dropType = "";
            }
            itemType = dropType;
        }
        bool gotItem = false;
        foreach (Item item in inventory.GetList()){
            if (item.itemType.ToString().Equals(itemType)){
                gotItem = true;
                itemNumText.text = item.Num.ToString();
            }
            if (isThrow){
                for (var i = 0; i < throw_types.Length; i++){
                    if (item.itemType.ToString().Equals(throw_types[i])){
                        throwList.Add(item);
                        if (throwType.Equals("")){
                            throwType = item.itemType.ToString();
                            itemImage.sprite = item.GetSprite();
                            itemNumText.text = item.Num.ToString();
                            gotItem = true;
                            switchPlayerItem(throwType);
                        }
                    }
                }
            }else{
                for (var i = 0; i < drop_types.Length; i++){
                    if (item.itemType.ToString().Equals(drop_types[i])){
                        dropList.Add(item);
                        if (dropType.Equals("")){
                            dropType = item.itemType.ToString();
                            itemImage.sprite = item.GetSprite();
                            itemNumText.text = item.Num.ToString();
                            gotItem = true;
                            switchPlayerItem(dropType);
                        }
                    }
                }
            }
        }
        if (!gotItem){
            itemImage.gameObject.SetActive(false);
            if (isThrow && throwList.Count >= 1){
                print(1);
                switchItem(false);
            }else if (!isThrow && dropList.Count >= 1){
                print(2);
                switchItem(false);
            }else{
                print(3);
                itemImage.sprite = null;
                itemNumText.text = "";
                switchPlayerItem("");
            }

        }
        else
            itemImage.gameObject.SetActive(true);
    }

    void switchPlayerItem(string _itemType){
        int index = Array.IndexOf(item_types, _itemType);
        if (isThrow){
            if (_itemType.Equals(""))
                player_script.projectile = null;
            else if (index != -1)
                player_script.projectile = item_prehabs[index];
        }else{
            if (_itemType.Equals(""))
                player_script.dropped_item = null;
            else if (index != -1)
                player_script.dropped_item = item_prehabs[index];
        }
        
    }

    public void itemUsed()
    {
        GameObject prehab;
        if (isThrow)
            prehab = player_script.projectile;
        else
            prehab = player_script.dropped_item;
        int index = item_prehabs.IndexOf(prehab);
        // item_types[index]
        Item item = new Item { itemType = Types[index]};
        player_script.inventory.DeleteItem(item);
    }
}
