using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    // public CharacterController2D controller;
    public float runSpeed = 10f;
    public float climbSpeed = 1f;
    public Vector2 jumpHeight = new Vector2(0f,12f);
    public Vector2 projectile_force = new Vector2(1.0f,1.0f); // projectile (thrown item) force
    private float charged_time = 1.0f; // for accumulated projectile charged time. 0 will become "drop".
    private float face_direction = 1.0f; // record where player faces. because transform.forward doesn't work in 2D
    public float naturalGravity;
    public int jumpback;
    public GameObject dropped_item; // prefab of dropped item
    private GameObject cur_dropped_item; //  current dropped item

    public GameObject projectile; // prefab of projectile
    private GameObject cur_projectile; // current projectile
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
        // jump
        if (Input.GetButtonDown("Jump") && jump){
            jump = false;
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            rb.gravityScale = naturalGravity;
        }
        // if player jump too down  
        if (transform.position.y <= -10f){
            Restart();
        }

        // MuseButton 0: left ; 1 : right.        
        // count accumlated charged time for projectile
        if (Input.GetMouseButton(0)){
            charged_time += Time.deltaTime;
            charged_time = (charged_time > 3f ? 3f : charged_time);
        }

        if (Input.GetMouseButtonUp(0)){
            Vector2 charged_projectile_force = charged_time * projectile_force;
            ProjectItem(face_direction, charged_projectile_force);
            charged_time = 1f;
        }
        
        if (Input.GetMouseButtonUp(1)){
            DropItem();
        }
    }
    void FixedUpdate ()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputX_Raw = Input.GetAxisRaw("Horizontal"); // record face direction
        float inputY = Input.GetAxis("Vertical");
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
                // gravity set to zero when jump
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
    }
    
    // project prefab from (player's coordinate + some hight)
    private void ProjectItem(float face_direction, Vector2 charged_projectile_force){
        // need last moving direction to determine project
        cur_projectile = Instantiate(projectile, transform.position + new Vector3 (0.0f,0.5f,0.0f), Quaternion.identity);
        Rigidbody2D cur_projectile_rb = cur_projectile.GetComponent<Rigidbody2D>();
        cur_projectile_rb.angularVelocity = 0f;
        cur_projectile_rb.AddForce(new Vector2(charged_projectile_force.x * face_direction, charged_projectile_force.y));
    }
    
    // drop prefab from player's coordinate
    private void DropItem(){
        cur_dropped_item = Instantiate(dropped_item, transform.position, Quaternion.identity);
        Rigidbody2D cur_dropped_rb = cur_dropped_item.GetComponent<Rigidbody2D>();
        cur_dropped_rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Ground"){
            jump = true;
        }
    }

    private void dropItem(){

    }
    public void touchMonster(int direction, int damageAmount){
        Vector2 layback;
        layback = new Vector2(direction*jumpback,8);
        // print(layback);
        
        // rb.AddForce(layback, ForceMode2D.Impulse);
        int moveForce = 20;
        int jumpForce = 15;
        rb.velocity = new Vector2(direction * moveForce, jumpForce);

        healthSystem.Damage(damageAmount);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
        
        if (healthSystem.GetHealth() == 0){
            Restart();
        }
    }

    public void healItem(){
        healthSystem.Heal(10);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
    }

    //Restart when health equal 0
    public void Restart(){
        transform.position = startPos;
        healthSystem.Reset();
        bar.ChangeHealthStatus(healthSystem.GetHealth());
    }

}
