using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBossFireball : MonoBehaviour
{
    public float speed = 3f;
    public int damageAmount = 10;
    public bool canMove = false;
    public Vector3 target;
    public float dieAfterSec = 0.5f;
    bool hasAttacked = false;
    Animator animator;
    public smoothCameraFollow camera;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        camera = GameObject.Find("Main Camera").GetComponent<smoothCameraFollow>();

    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.position += target * speed * Time.deltaTime;
        }   
    }
    // private void OnTriggerStay2D(Collider2D other) {
    //     if (other.gameObject.tag != "Ground"){
    //         attack(other);
    //     }else{
    //         if (other.gameObject.name =="floor"){
    //             Destroy(gameObject,dieAfterSec);
    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Ground"){
            attack(other);
        }else{
            if (other.gameObject.name !="floor" && other.gameObject.tag=="Ground"){
                gameObjectStatus();
            }
        }
    }
    private void attack(Collider2D other){
        if (other.gameObject.tag == "Player" && !hasAttacked){
            if (other.gameObject.name == "player"){
                int direction;
                if (gameObject.transform.position.x < other.gameObject.transform.position.x){
                    direction = 1;//The object is on the right side of the enemy
                }else{
                    direction = -1;//Object on the left side of the enemy
                }
                player player_script = other.gameObject.GetComponent<player>();
                player_script.attacked(direction, damageAmount);
                gameObjectStatus();
            }else{
                other.gameObject.GetComponent<objectStatus>().attackObject(damageAmount);
                gameObjectStatus();
            }
        }else{
            objectStatus objectStatus = other.gameObject.GetComponent<objectStatus>();
            if (objectStatus != null){
                    objectStatus.attackObject(damageAmount);
                    gameObjectStatus();
            }
            
        }
    }
    void gameObjectStatus(){
        camera.enableShake = true;
        canMove = false;
        hasAttacked = true;
        if (animator != null)
            print("destory animate");
            animator.SetTrigger("isDestroy");
        Destroy(gameObject,dieAfterSec);
    }
    
}
