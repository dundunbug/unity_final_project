using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShampoo : MonoBehaviour
{
    private objectScript objectScript;
    public GameObject bubble;
    private GameObject cur_bubble;
    private player player_script;
    private GameObject player;
    void Start()
    {
        objectScript = new objectScript(gameObject);
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag== "Ground"){
            cur_bubble = Instantiate(bubble, transform.position, Quaternion.identity);
        }
    }
}
