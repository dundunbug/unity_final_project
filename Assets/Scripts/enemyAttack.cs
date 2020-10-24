using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    private float lastTime = 0f;
    public float time = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player"){
            if (Time.time - lastTime >= time ){
                int damageAmount = 10;
                int direction;
                if (gameObject.transform.position.x < other.gameObject.transform.position.x){
                    direction = 1;//The object is on the right side of the enemy
                }else{
                    direction = -1;//Object on the left side of the enemy
                }
                player player_script = other.gameObject.GetComponent<player>();
                player_script.attacked(direction, damageAmount);
                lastTime = Time.time;
            }
        }else if (other.gameObject.tag != "Ground"){
            int damageAmount = 10;
            objectStatus objectStatus = other.gameObject.GetComponent<objectStatus>();
            if (objectStatus != null){
                if (Time.time - lastTime >= time ){
                    objectStatus.attackObject(damageAmount);
                    lastTime = Time.time;
                }
            }
        }
    }
}
