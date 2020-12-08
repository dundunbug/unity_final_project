using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    string[] item_types = new string[] {"Bomb_L","Bomb_Timer","Bomb_S","Carton",
    "Lego","TransferGate","CardBoard","Pillow","Bottle","Teddy"};
    public string[] throw_types = new string[] {"Bomb_L","Bomb_Timer","Bomb_S","Carton"
    ,"TransferGate","Pillow","Bottle","Teddy"};
    public string[] drop_types = new string[] {"Bomb_L","Bomb_Timer","Carton",
    "Lego","TransferGate","CardBoard","Pillow"};
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player>();
        inventory = player_script.inventory;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (isThrow)
                throwType = itemList[nextIndex].itemType.ToString();
            else
                dropType = itemList[nextIndex].itemType.ToString();
        }
        
    }

    public void refreshList(){
        inventory = player_script.inventory;
        throwList.Clear();
        dropList.Clear();
        string itemType;
        if (isThrow){
            itemType = throwType;
        }else{ 
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
                        }
                    }
                }
            }
        }
        if (!gotItem){
            itemImage.sprite = null;
        }
    }



    // void switchItem(){
        

    //     case "bomb1_0":
        
    //         Player.PickItem(Item.ItemType.Bomb_L);
    //         break;
    //     case "bomb2_0":
    //         Player.PickItem(Item.ItemType.Bomb_Timer);
    //         break;
    //     case "bomb3_0":
    //         Player.PickItem(Item.ItemType.Bomb_S);
    //         break;
    //     case "box_0":
    //         Player.PickItem(Item.ItemType.Carton);
    //         break;
    //     case "gimu_0":
    //         Player.PickItem(Item.ItemType.Lego);
    //         break;
    //     case "painting_0":
    //         Player.PickItem(Item.ItemType.TransferGate);
    //         break;
    //     case "paper_0":
    //         Player.PickItem(Item.ItemType.CardBoard);
    //         break;
    //     case "pillow_0":
    //         Player.PickItem(Item.ItemType.Pillow);
    //         break;
    //     case "shampoo_0":
    //         Player.PickItem(Item.ItemType.Bottle);
    //         break;
    //     case "teddy_0":
    //         Player.PickItem(Item.ItemType.Teddy);
    //         break;
    // }


}
