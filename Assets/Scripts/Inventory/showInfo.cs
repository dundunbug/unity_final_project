using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShowInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public bool MouseOn;
    public Item.ItemType Type;
    public Image itemImage;
    public Image image;
    public Text T;
    public Text T2;

    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)    //滑鼠移入
    {
        T.gameObject.SetActive(true);
        T2.gameObject.SetActive(true);
        itemImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)    //滑鼠移出
    {
        itemImage.gameObject.SetActive(false);
        T.gameObject.SetActive(false);
        T2.gameObject.SetActive(false);
    }
    public void SetType(Item.ItemType type)
    {
        /*image*/
        image = itemImage.GetComponent<Image>();
        image.transform.position = new Vector2(740f, 440f);
        itemImage.gameObject.SetActive(false);

        /*text*/
        T.transform.position = new Vector2(715f, 310f);
        T2.transform.position = new Vector2(725f, 270f);
        T.gameObject.SetActive(false);
        T2.gameObject.SetActive(false);

        switch (type)
        {
            default:
            case Item.ItemType.Bomb_L:
                {
                    image.sprite = ItemAssets.Instance.Bomb_LSprite;
                    T.text = "Bomb_L";
                    T2.text = "將炸彈放置，你不喜歡的東西就會被炸掉喔~";
                }
                break;
            case Item.ItemType.Bomb_S:
                {
                    image.sprite = ItemAssets.Instance.Bomb_SSprite;
                    T.text = "Bomb_S";
                    T2.text = "將炸彈放置，你不喜歡的東西就會被炸掉喔~";
                }
                break;
            case Item.ItemType.Bomb_Timer:
                {
                    image.sprite = ItemAssets.Instance.Bomb_TimerSprite;
                    T.text = "Bomb_Timer";
                    T2.text = "此炸彈竟然還可以定時！";
                }
                break;
            case Item.ItemType.Teddy:
                {
                    image.sprite = ItemAssets.Instance.TeddySprite;
                    T.text = "Teddy";
                    T2.text = "可愛的泰迪熊~";
                }
                break;
            case Item.ItemType.TransferGate:
                {
                    image.sprite = ItemAssets.Instance.TransferGateSprite;
                    T.text = "TransferGate";
                    T2.text = "藝術這種東西，不是誰都可以欣賞的，至少你就不行。";
                }
                break;
            case Item.ItemType.Lego:
                {
                    image.sprite = ItemAssets.Instance.LegoSprite;
                    T.text = "Lego";
                    T2.text = "樂高的堆疊或許可以幫你的人生更上一層樓";
                }
                break;
            case Item.ItemType.CardBoard:
                {
                    image.sprite = ItemAssets.Instance.CardBoardSprite;
                    T.text = "CardBoard";
                    T2.text = "怎麼會有紙板，誰知道呢，或許怪物會被擋住";
                }
                break;
            case Item.ItemType.Bottle:
                {
                    image.sprite = ItemAssets.Instance.BottleSprite;
                    T.text = "Bottle";
                    T2.text = "泡沫，誰不愛，有毒的泡沫，更棒了!";
                }
                break;
            case Item.ItemType.Carton:
                {
                    image.sprite = ItemAssets.Instance.CartonSprite;
                    T.text = "Carton";
                    T2.text = "紙箱，貓喜歡，怪物不知道如何";
                }
                break;
            case Item.ItemType.Pillow:
                {
                    image.sprite = ItemAssets.Instance.PillowSprite;
                    T.text = "Pillow";
                    T2.text = "一顆平凡的枕頭，什麼都做不了乾脆睡覺。";
                }
                break;
            case Item.ItemType.DroppedItem:
                {
                    image.sprite = ItemAssets.Instance.DroppedItemSprite;
                    T.text = "Energy Orbs";
                    T2.text = "怪物掉落的能量球，你可以選擇要將他轉成能量點，或是轉換成任何物品(十分划算)";

                }
                break;

        }
    }

}