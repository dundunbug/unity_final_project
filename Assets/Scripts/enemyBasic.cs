using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBasic : MonoBehaviour
{
    //Basic enemy status
    //damaged being attacked
    //movement attack player
    public int moveForce = 5;
    public int jumpForce = 5;
    public float speed = 3.5f;
    public Vector2 jumpHeight = new Vector2(0f,12f);
    public float distance = 10f;
    public float findPlayerRadius = 5f;
    public bool advanceTrack = false;
    public float yrange = 1.5f;
    public bool canAttack = false;
    public float wallDis = 2f;
    public float xDisPlayer = 1f;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform wallDetection;
    private healthSystem healthSystem = new healthSystem(20);
    private Rigidbody2D rb;
    public bool canMove = true;
    private bool nearWall = false;
    private bool canJump = true;
    private int countSamePlace = 0;
    private Vector2 lastPos;
    private bool isSamePlace = false;
    private Vector2 playerLastPos;
    private float height;
    private SpriteRenderer SpriteRenderer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.flipX = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        if (canMove){
            // check if player is near
            int playerLayer = 1 << LayerMask.NameToLayer("player");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findPlayerRadius, playerLayer);
            Collider2D collider;
            // check if it is player or attract item
            if (colliders.Length != 0)
                collider = checkPlayerOrItem(colliders);
            else{
                collider = null;
            }
            // check if ground edge 
            int groundLayer = 1 << LayerMask.NameToLayer("ground");
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, groundLayer);
            // if player is not near, patrol
            if (groundInfo.collider == false && collider==null){
                flip();
            }

            // check if bumping into wall 
            Vector2 direction;
            if (movingRight){
                direction = Vector2.right;
            }else{
                direction = Vector2.left;
            }
            
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, direction, wallDis, groundLayer);
            if (wallInfo.collider == true){
                nearWall = true;
                // StartCoroutine(notNearWallAfterSec(0.5f));
                if (isSamePlace){
                    flip();
                    StartCoroutine(notNearWallAfterSec(0.5f));
                }else if (!advanceTrack || collider == null){
                    flip();
                    // if ground dis too low, must do something
                    // climb up 
                }
            }else{
                nearWall = false;
            }

            // advance
            if (collider != null && advanceTrack){
                // print(collider.gameObject.tag);
                animator.SetBool("isAttacking",false);
                GameObject player = collider.gameObject;
                checkPlayerPos(player.transform.position);
                float range = player.transform.position.y- (transform.position.y - height*1/2);
                // print(canJump);
                if (range >= yrange && nearWall && canJump){
                    if(!isSamePlace){
                        int dir;
                        // print(transform.position.x - player.transform.position.x );
                        if (transform.position.x - player.transform.position.x <= -0.5f){
                            dir = 1;
                        }else if (transform.position.x - player.transform.position.x >= 0.5f){
                            dir = -1;
                        }else{
                            dir = 0;
                        }
                        jump(player,dir);
                    }
                }

                if (Mathf.Abs(transform.position.x- player.transform.position.x) <= xDisPlayer){
                    // move or attack 
                    // print("move or attack");
                    if (!canAttack){
                        // check which direction facing 
                        if (transform.position.x - player.transform.position.x <= -0.5f){
                            transform.eulerAngles = new Vector3(0, 0, 0);
                            movingRight = true;
                        }else if (transform.position.x - player.transform.position.x >= 0.5f){
                            transform.eulerAngles = new Vector3(0, -180, 0);
                            movingRight = false;
                        }
                        transform.Translate(Vector2.right * speed * Time.deltaTime); // move
                    }else{
                        print("attack");
                        attack(); // attack
                    }
                }else if (!isSamePlace){
                    // print("track");
                    gameObject.transform.Find("attackDetector").gameObject.SetActive(false);
                    track(collider.gameObject);
                }else{
                    gameObject.transform.Find("attackDetector").gameObject.SetActive(false);
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
            }else{
                animator.SetBool("isAttacking",false);
                gameObject.transform.Find("attackDetector").gameObject.SetActive(false);
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            // basic
            // if near wall than don't track player 
            // nearWall == false
            // if (collider != null && advanceTrack){
            //     if (Mathf.Abs(transform.position.x- collider.gameObject.transform.position.x) <= 3.5f){
            //         // move or attack 
            //         if (!canAttack){
            //             transform.Translate(Vector2.right * speed * Time.deltaTime); // move
            //         }else{
            //             attack(); // attack
            //         }
            //     }else{
            //         track(collider.gameObject);
            //     }
            // }else{
            //     transform.Translate(Vector2.right * speed * Time.deltaTime);
            // }
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
    IEnumerator notNearWallAfterSec(float time){
        yield return new WaitForSeconds (time);
        nearWall = false;
    }
    void flip(){
        if(movingRight == true){
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }else{
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }
    void jump(GameObject player, int dir){
        canJump = false;
        canMove = false;
        rb.AddForce(new Vector2(jumpHeight.x*dir,jumpHeight.y), ForceMode2D.Impulse);
        StartCoroutine(canMoveAfterSec(0.5f));
        if (Vector2.Distance(lastPos, transform.position) <= 1f){
            print("same place");
            countSamePlace += 1;
            if ( countSamePlace >= 7 && nearWall){
                flip();
                playerLastPos = player.transform.position;
                countSamePlace = 0;
                isSamePlace = true;
            }
        }else{
            countSamePlace = 0;
        }
        lastPos = transform.position;
    }
    void checkPlayerPos(Vector2 pos){
        if (isSamePlace && Vector2.Distance(playerLastPos, pos) >= 10f){
            isSamePlace = false;
        }
    }
    public void track(GameObject player){
        // check which direction facing 
        if (transform.position.x < player.transform.position.x){
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }else{
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        Vector2 pos = new Vector2(player.transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
    void attack(){
        canMove = false;
        animator.SetBool("isAttacking",true);
        // print("attack");
        // transform.position += Vector3.left * 0.1f;
        // transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left*10, 2 * speed * Time.deltaTime);
        gameObject.transform.Find("attackDetector").gameObject.SetActive(true);
        StartCoroutine(canMoveAfterSec(0.5f));
    }
    IEnumerator deactiveAttackDetection(float time){
        yield return new WaitForSeconds (time);
        // print("stop attack");
        gameObject.transform.Find("attackDetector").gameObject.SetActive(false);
    }
    public void attacked(int direction, int damageAmount){
        canMove = false;
        if (direction != 0){
            Vector2 layback = new Vector2(direction*moveForce,jumpForce);
            rb.AddForce(layback, ForceMode2D.Impulse);
        }
        healthSystem.Damage(damageAmount);
        if (healthSystem.GetHealth() == 0){
            animator.SetTrigger("isDead");
            Destroy(gameObject,0.4f);
        }
        // can move after n sec later
        StartCoroutine(canMoveAfterSec(1f));
    }
    IEnumerator canMoveAfterSec(float time){
        yield return new WaitForSeconds (time);
        canMove = true;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            canJump = true;
        }
        if (other.gameObject.tag == "Enemy"){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }else{
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            canJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            canJump = false;
        }  
    }


}
