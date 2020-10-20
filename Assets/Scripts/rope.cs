using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    // Start is called before the first frame update
    private player player;
    void Start()
    {
        player = GameObject.Find("player").GetComponent<player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Player"){ 
            player.climb = true;
            player.rope = this;
        }
    }
    // private void OnTriggerStay2D(Collider2D other) {
    //     if (other.gameObject.tag == "Player"){
    //         // other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    //         player.climb = true;
    //     }
    // }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            player.climb = false;
            // other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
        }        
    }
}
