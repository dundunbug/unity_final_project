using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTouchPlayer : MonoBehaviour
{
    //damage player when touch
    private float lastTime = 0f;
    private player player_script;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.name== "player"){
            if (Time.time - lastTime >= 0.8f ){
                int direction;
                int damageAmount = 5;
                if (transform.position.x < other.gameObject.transform.position.x){
                    direction = 1;//The player is on the right side of the enemy
                }else{
                    direction = -1;//Players on the left side of the enemy
                }
                player_script.attacked(direction, damageAmount);
                lastTime = Time.time;
            }
        }
    }
}
