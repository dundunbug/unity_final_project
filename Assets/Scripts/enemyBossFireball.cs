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
    public audioController audioController;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        camera = GameObject.Find("Main Camera").GetComponent<smoothCameraFollow>();
        audioController = GameObject.Find("audioController").GetComponent<audioController>();

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
                print(other.gameObject.name);
                other.gameObject.GetComponent<objectStatus>().attackObject(damageAmount);
                gameObjectStatus();
            }
        }else{
            print(other.gameObject.name);
            fireBallExplode(3f,damageAmount);
            
        }
    }
    void gameObjectStatus(){
        audioController.playExplosionBigSFX();
        camera.enableShake = true;
        canMove = false;
        hasAttacked = true;
        if (animator != null)
            print("destory animate");
            animator.SetTrigger("isDestroy");
        Destroy(gameObject,dieAfterSec);

    }
    //fireball attack
    public void fireBallExplode(float radius, int explodeAmount){
        //stop object
        // gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        // gameObject.GetComponent<Rigidbody2D>().gravityScale =0;

        // Get nearby objects
        int enemyLayer = 1 << LayerMask.NameToLayer("enemyOnGround");
        int itemLayer = 1 << LayerMask.NameToLayer("item");
        int itemBrickLayer = 1 << LayerMask.NameToLayer("itemBrick");
        int finalmask = enemyLayer | itemLayer | itemBrickLayer;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius, finalmask);
        foreach(Collider2D nearbyObject in colliders){
            // damage
            int damageAmount = explodeAmount;
            objectStatus objectStatus = nearbyObject.gameObject.GetComponent<objectStatus>();
            if (objectStatus != null){
                objectStatus.attackObject(damageAmount);
            }
        }
        gameObjectStatus();
    }
}
