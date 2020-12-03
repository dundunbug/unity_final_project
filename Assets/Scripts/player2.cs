using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Vector2 jumpHeight = new Vector2(0f,12f);
    private Animator animator;
    public float runSpeed;

    Rigidbody2D rb;
    SpriteRenderer SpriteRenderer;
    bool canJump = true;
    bool movingRight = true;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // jump
        if (Input.GetButtonDown("Jump") && canJump){
            canJump = false;
            // print("jump");
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            // rb.gravityScale = naturalGravity;
            // animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.R)){
            print("reset");
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }

    private void FixedUpdate() {
        float inputX = Input.GetAxis("Horizontal");
        float inputX_Raw = Input.GetAxisRaw("Horizontal"); // record face direction
        float inputY = Input.GetAxis("Vertical");
        float posY = transform.position.y;
        float posX = transform.position.x;
        //move
        if(inputX != 0 && canMove){
            if (inputX > 0 && !movingRight)
                flip();
            else if (inputX < 0 && movingRight)
                flip();
            Vector3 move = new Vector3(runSpeed * inputX , posY, 0);
            move *= Time.deltaTime;
            transform.Translate(move);
            // animator.SetBool("run",true);
        }else{
            // animator.SetBool("run",false);
        }
    }
    void flip(){
        if(movingRight == true){
            movingRight = false;
            SpriteRenderer.flipX = true;
        }else{
            movingRight = true;
            SpriteRenderer.flipX = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Ground"){
            // print(other.gameObject.tag);
            canJump = true;
        }
    }
}
