using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTouchPlayer : MonoBehaviour
{
    //damage player when touch
    private float lastTime = 0f;
    private player player_script;
    private GameObject player;
    public int damageAmount = 5;
    public bool canAttackItem = false;
    public float AttackBetweenTime = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag== "Teleport"){
            // print(other.gameObject.tag);
            if (objectPainting.onGround){
                transform.parent.position = objectPainting.TeleportingGate;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.name== "player"){
            if (Time.time - lastTime >= AttackBetweenTime){
                int direction;
                if (transform.position.x < other.gameObject.transform.position.x){
                    direction = 1;//The player is on the right side of the enemy
                }else{
                    direction = -1;//Players on the left side of the enemy
                }
                player_script.attacked(direction, damageAmount);
                lastTime = Time.time;
            }
        }else if (canAttackItem && other.gameObject.tag == "Player"){
            if (Time.time - lastTime >= AttackBetweenTime ){
                objectStatus objectStatus = other.gameObject.GetComponent<objectStatus>();
                if (objectStatus)
                    objectStatus.attackObject(damageAmount);
                lastTime = Time.time;
            }
        }
    }
}
