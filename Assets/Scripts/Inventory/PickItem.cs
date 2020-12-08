using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    public player Player;
    private bool picked = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && picked == false)
        {
            switch (this.gameObject.tag){
            case "bomb1_0":
                Player.PickItem(Item.ItemType.Bomb_L);
                break;
            case "bomb2_0":
                Player.PickItem(Item.ItemType.Bomb_Timer);
                break;
            case "bomb3_0":
                Player.PickItem(Item.ItemType.Bomb_S);
                break;
            case "box_0":
                Player.PickItem(Item.ItemType.Carton);
                break;
            case "gimu_0":
                Player.PickItem(Item.ItemType.Lego);
                break;
            case "painting_0":
                Player.PickItem(Item.ItemType.TransferGate);
                break;
            case "paper_0":
                Player.PickItem(Item.ItemType.CardBoard);
                break;
            case "pillow_0":
                Player.PickItem(Item.ItemType.Pillow);
                break;
            case "shampoo_0":
                Player.PickItem(Item.ItemType.Bottle);
                break;
            case "teddy_0":
                Player.PickItem(Item.ItemType.Teddy);
                break;
            }
            Destroy(this.gameObject);
            picked = true;
        }
    }


}
