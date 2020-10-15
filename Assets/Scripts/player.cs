using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    // public CharacterController2D controller;
    public float runSpeed = 10f;
    public float climbSpeed = 1f;
    public Vector2 jumpHeight = new Vector2(0f,12f);
    public Vector2 projectile_force = new Vector2(10f,10f);
    private float face_direction; // record where player faces. because transform.forward doesn't work in 2D
    public float naturalGravity;
    public int jumpback;
    public GameObject dropped_item; // prefab of dropped item
    private GameObject cur_dropped_item; //  current dropped item

    public GameObject projectile;
    private GameObject cur_projectile;
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
        face_direction = 1.0f;
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
        float inputX_Raw = Input.GetAxisRaw("Horizontal"); // record face direction
        float posY = transform.position.y;
        float posX = transform.position.x;

        // detect where player faces
        if (inputX_Raw != 0f){
            face_direction = inputX_Raw;
        }

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
        
        // call drop and project here because we may use inputX 
        if (Input.GetButtonDown("Fire1")){
            DropItem();
        }

        if (Input.GetButtonDown("Fire2")){
            ProjectItem(face_direction);
        }

        // if (Input.GetButtonDown("Crouch")){
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch")){
        //     crouch = false;
        // }
    }

    private void DropItem(){
        cur_dropped_item = Instantiate(dropped_item, transform.position, Quaternion.identity);
        Rigidbody2D cur_dropped_rb = cur_dropped_item.GetComponent<Rigidbody2D>();
        cur_dropped_rb.angularVelocity = 0f;
    }

    private void ProjectItem(float face_direction){
        // need last moving direction to determine project
        cur_projectile = Instantiate(projectile, transform.position + new Vector3 (0.0f,0.5f,0.0f), Quaternion.identity);
        Rigidbody2D cur_projectile_rb = cur_projectile.GetComponent<Rigidbody2D>();
        cur_projectile_rb.angularVelocity = 0f;
        cur_projectile_rb.AddForce(new Vector2(projectile_force.x *face_direction,projectile_force.y));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Ground"){
            jump = true;
        }
    }

    private void dropItem(){

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
