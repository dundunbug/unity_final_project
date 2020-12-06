using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectShampoo : MonoBehaviour
{
    private bool emmision = false;
    private objectScript objectScript;
    public GameObject bubble;
    private GameObject cur_bubble;
    private player player_script;
    private GameObject player;
    private Rigidbody2D rb;
    private int count = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectScript = new objectScript(gameObject);
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag== "Ground" && !emmision){
            emmision = true;
        }
    }

    void Update()
    {
        if (emmision && rb.velocity.magnitude == 0 && count == 1)
        {
            count = count -1 ;
            Destroy(gameObject, 7.0f);
            cur_bubble = Instantiate(bubble, transform.position, Quaternion.identity);
            Destroy(cur_bubble, 7.0f);
        }
    }
}
