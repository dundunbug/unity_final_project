using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBomb : MonoBehaviour
{
    //bomb explodes when touching the enemy or ground
    public float radius = 3f;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Ground"){
            Explode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy"){
            Explode();
        }
    }

    void Explode(){
        // Show effect

        //stop object
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale =0;

        // Get nearby objects
        int enemyLayer = 1 << LayerMask.NameToLayer("enemyOnGround");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach(Collider2D nearbyObject in colliders){
            // damage
            int direction;
            int damageAmount = 10;
            if (transform.position.x < nearbyObject.gameObject.transform.position.x){
                direction = 1;//The object is on the right side of the enemy
            }else{
                direction = -1;//Object on the left side of the enemy
            }
            enemyBasic enemyBasic = nearbyObject.gameObject.GetComponent<enemyBasic>();
            enemyBasic.attackMonster(direction,damageAmount);
        }
        Destroy(gameObject);
        // remove bomb after 1 sec
        // StartCoroutine( DestroyGameObject(1));
    }

    IEnumerator DestroyGameObject(int time){
        yield return new WaitForSeconds (time);
        Destroy(gameObject);
    }
}
