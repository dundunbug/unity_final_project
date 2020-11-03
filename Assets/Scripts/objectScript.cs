using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectScript
{
    GameObject gameObject;

    public objectScript(GameObject gameObject){
        this.gameObject = gameObject;
    }

    // bomb
    public void Explode(float radius){
        // Show effect
        Animator animator = gameObject.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("isDestroy");
        //stop object
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale =0;

        // Get nearby objects
        int enemyLayer = 1 << LayerMask.NameToLayer("enemyOnGround");
        int playerLayer = 1 << LayerMask.NameToLayer("player");
        int itemLayer = 1 << LayerMask.NameToLayer("item");
        int itemBrickLayer = 1 << LayerMask.NameToLayer("itemBrick");
        int finalmask = enemyLayer | playerLayer | itemLayer | itemBrickLayer;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, finalmask);
        foreach(Collider2D nearbyObject in colliders){
            // damage
            int direction;
            int damageAmount = 10;
            if (gameObject.transform.position.x < nearbyObject.gameObject.transform.position.x){
                direction = 1;//The object is on the right side of the enemy
            }else{
                direction = -1;//Object on the left side of the enemy
            }
            if (nearbyObject.gameObject.tag == "Enemy"){
                enemyBasic enemyBasic = nearbyObject.gameObject.GetComponent<enemyBasic>();
                if (enemyBasic != null){
                    enemyBasic.attacked(direction,damageAmount);
                }
            }else if (nearbyObject.gameObject.tag == "Player"){
                player playerScript = nearbyObject.gameObject.GetComponent<player>();
                playerScript.attacked(direction,damageAmount);
            }else{
                objectStatus objectStatus = nearbyObject.gameObject.GetComponent<objectStatus>();
                if (objectStatus != null){
                    objectStatus.attackObject(damageAmount);
                }
            }
        }
        // remove bomb after 0.6 sec
        Object.Destroy(gameObject,0.6f);
    }

    //teddy attack
    public void TeddyAttack(){

    }
}