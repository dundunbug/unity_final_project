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
                Debug.Log("add bomb1_0");
                break;
            case "bomb2_0":
                Player.PickItem(Item.ItemType.Bomb_Timer);
                Debug.Log("add bomb2_0");
                break;
            case "bomb3_0":
                Player.PickItem(Item.ItemType.Bomb_S);
                Debug.Log("add bomb3_0");
                break;
            case "box_0":
                Player.PickItem(Item.ItemType.Carton);
                Debug.Log("add box");
                break;
            case "gimu_0":
                Player.PickItem(Item.ItemType.Lego);
                Debug.Log("addgimu");
                break;
            case "painting_0":
                Player.PickItem(Item.ItemType.TransferGate);
                Debug.Log("addpainting");
                break;
            case "paper_0":
                Player.PickItem(Item.ItemType.CardBoard);
                Debug.Log("addpaper");
                break;
            case "pillow_0":
                Player.PickItem(Item.ItemType.Pillow);
                Debug.Log("addpillow");
                break;
            case "shampoo_0":
                Player.PickItem(Item.ItemType.Bottle);
                Debug.Log("addshampoo");
                break;
            case "teddy_0":
                Player.PickItem(Item.ItemType.Teddy);
                Debug.Log("addteddy");
                break;
            }
            Destroy(this.gameObject);
            picked = true;
        }
    }


}
