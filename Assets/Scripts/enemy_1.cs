using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_1 : MonoBehaviour
{
    private player player_script;
    private float lastTime = 0f;
    private GameObject player;
    private healthSystem healthSystem = new healthSystem(50);
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
    }

    private void OnTriggerStay2D(Collider2D other) {
        print(other.gameObject.tag);
        if (other.gameObject.tag== "Player"){
            if (Time.time - lastTime >= 0.8f ){
                int direction;
                int damageAmount = 10;
                if (transform.position.x < other.gameObject.transform.position.x){
                    direction = 1;//The player is on the right side of the enemy
                }else{
                    direction = -1;//Players on the left side of the enemy
                }
                player_script.touchMonster(direction, damageAmount);
                lastTime = Time.time;
            }
        }
    }

    public void attackMonster(int direction, int damageAmount){
        healthSystem.Damage(damageAmount);
        if (healthSystem.GetHealth() == 0){
            Destroy(gameObject);
        }
    }
}
