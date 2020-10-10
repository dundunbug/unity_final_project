using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    // public CharacterController2D controller;
    public float runSpeed = 10f;
    public float climbSpeed = 1f;
    public Vector2 jumpHeight = new Vector2(0f,12f);
    public float naturalGravity;
    public int jumpback;
    [HideInInspector] public rope rope;
    [HideInInspector] public bool climb = false;

    private Vector3 startPos;
    private bool jump = true;
    private bool crouch = false;
    private Rigidbody2D rb;
    private bar bar;
    private healthSystem healthSystem = new healthSystem(100);
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        naturalGravity = rb.gravityScale;
        bar = GameObject.Find("bar").GetComponent<bar>();
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Jump") && jump){
            jump = false;
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            rb.gravityScale = naturalGravity;
        }

        if (transform.position.y <= -10f){
            Restart();
        }


    }
    
    void FixedUpdate ()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        float posY = transform.position.y;
        float posX = transform.position.x;
        //jump
        if (climb && inputY != 0){
            float ropeX = rope.transform.position.x;
            if (Mathf.Abs(posX - ropeX) <= 0.3f){
                rb.gravityScale =0;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                    RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ropeX, rb.position.y);
                rb.velocity = new Vector2(0f, inputY*climbSpeed);
            }
        }
        if(!climb){
            rb.gravityScale = naturalGravity;
        }
        //move
        if(inputX != 0){
            Vector3 move = new Vector3(runSpeed * inputX , posY, 0);     
            move *= Time.deltaTime;
            transform.Translate(move);   
        }

        // if (Input.GetButtonDown("Crouch")){
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch")){
        //     crouch = false;
        // }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Ground"){
            jump = true;
        }
    }

    public void touchMonster(int direction){
        Vector2 layback;
        layback = new Vector2(direction*jumpback,8);
        // print(layback);
        
        // rb.AddForce(layback, ForceMode2D.Impulse);
        int moveForce = 20;
        int jumpForce = 15;
        rb.velocity = new Vector2(direction * moveForce , jumpForce);

        healthSystem.Damage(10);
        bar.ChangeHealthStatus(healthSystem.GetHealth());

        if (healthSystem.GetHealth() == 0){
            Restart();
        }
    }

    public void healItem(){
        healthSystem.Heal(10);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
    }

    public void Restart(){
        transform.position = startPos;
    }

}
