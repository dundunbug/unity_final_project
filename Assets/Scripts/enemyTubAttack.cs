using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTubAttack : MonoBehaviour
{
    public float findPlayerRadius = 5f;
    public float AttackBetweenTime = 0.8f;
    public GameObject bubble;
    public int damageAmount = 2;
    private float lastTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
        int playerLayer = 1 << LayerMask.NameToLayer("player");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findPlayerRadius, playerLayer);
        Collider2D collider;
        // check if it is player or attract item
        if (colliders.Length != 0){
            collider = checkPlayerOrItem(colliders);
            gameObject.GetComponent<enemyBasic>().canMove = false;
            if (Time.time - lastTime >= AttackBetweenTime){
                // throw water
                throwWater(collider);
                lastTime = Time.time;
            }
        }else{
            gameObject.GetComponent<enemyBasic>().canMove = true;
        }
    }
    void throwWater(Collider2D collider){
        print(collider.gameObject.name);
        float ranX = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        // get ground y pos
        int groundLayer = 1 << LayerMask.NameToLayer("ground");
        RaycastHit2D groundInfo = Physics2D.Raycast(new Vector2(ranX, collider.bounds.center.y), Vector2.down, 100f, groundLayer);
        if (groundInfo){
            print(groundInfo.point);
            Vector2 newPos = groundInfo.point;
            // initiate prehab
            GameObject obj = GameObject.Instantiate(bubble, newPos, 
                Quaternion.identity) as GameObject;
            obj.GetComponent<enemyTouchPlayer>().damageAmount = damageAmount;
        }


    }
    Collider2D checkPlayerOrItem(Collider2D[] colliders){
        float dis = 1000f;
        Collider2D collider = colliders[0];
        if (colliders.Length >= 1){
            foreach(Collider2D col in colliders){
                // chase item first
                if (col.gameObject.name != "player"){
                    Vector2 disBetweenEnemy = transform.position - col.transform.position;
                    if (disBetweenEnemy.magnitude < dis){
                        dis = disBetweenEnemy.magnitude;
                        collider = col;
                    }
                }
            }
        }
        return collider;
    }
}
