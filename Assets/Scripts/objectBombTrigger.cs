using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBombTrigger : MonoBehaviour
{
    //trigger bomb after 3 secs
    public float timing = 3f;
    public float radius = 3f;
    private float countdown;
    private bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = timing ;
    }
    
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded){
            Explode();
            hasExploded = true;
        }
    }

    void Explode(){
        // Show effect

        //stop object
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale =0;

        int enemyLayer = 1 << LayerMask.NameToLayer("enemyOnGround");
        // Get nearby objects
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
