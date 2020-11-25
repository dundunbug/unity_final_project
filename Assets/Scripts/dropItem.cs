﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropItem : MonoBehaviour
{
    int count = 1; // for pick item number workaround
    private player player_script;
    // Start is called before the first frame update
    void Start()
    {
        player_script = GameObject.Find("player").GetComponent<player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "player"){
            if (count == 1)
            {
                player_script.PickItem(Item.ItemType.DroppedItem);
            }
            count = count -1;
            // player_script.dropItemCount+= 1;
            Destroy(gameObject);
        }
    }
}
