using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class showInfo : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider2D Obcollider;
    public GameObject text1;
    public bool MouseOn;
    public Item.ItemType Type;

    void Start()
    {
        text1.SetActive(false);
        MouseOn = false;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private void OnMouseOver()
    {
        MouseOn = true;
    }

    public void SetType(Item.ItemType type)
    {
      /*   Type = type;
        switch (type)
        {
            default:
            case Item.ItemType.Bomb_L:
                {
                    //text1.GetComponent<Text>() = "r";
                } */
         /*   case Item.ItemType.Bomb_S: ItemAssets.Instance.Bomb_SSprite;
            case Item.ItemType.Bomb_Timer:ItemAssets.Instance.Bomb_TimerSprite;
            case Item.ItemType.Teddy:
            case Item.ItemType.TransferGate: ItemAssets.Instance.TransferGateSprite;
            case Item.ItemType.Lego: ItemAssets.Instance.LegoSprite;
            case Item.ItemType.CardBoard:
            case Item.ItemType.Bottle: ItemAssets.Instance.BottleSprite;
            case Item.ItemType.Carton: ItemAssets.Instance.CartonSprite;
            case Item.ItemType.Pillow:
                {
                    Debug.Log("found"); ItemAssets.Instance.PillowSprite;
                }

        }*/
    }
}