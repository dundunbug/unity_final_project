using System;
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
    // touched by monster 
    public int moveForce = 5;
    public int jumpForce = 5;
    public float moveAfterSec = 0.5f;
    //
    public GameObject dropped_item; // prefab of dropped item
    private GameObject cur_dropped_item; //  current dropped item

    public GameObject projectile; // prefab of projectile
    private GameObject cur_projectile; // current projectile
    [HideInInspector] public rope rope;
    private Vector3 startPos;

    // private bool crouch = false;
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    private bar bar;
    private healthSystem healthSystem = new healthSystem(100);
    // Start is called before the first frame update
    [Header("Status_Bool")]
    private bool canJump = false;
    private bool canMove = true;
    private bool movingRight = true;
    [HideInInspector] public bool climb = false;

    [Header("Animator")]
    public Animator animator_player;

    [Header("Projectile")]
    public Vector2 projectile_force; // projectile (thrown item) force
    private float charged_time = 1.0f; // for accumulated projectile charged time. 0 will become "drop".
    private float face_direction = 1.0f; // record where player faces. because transform.forward doesn't work in 2D
    public LineRenderer projectile_line; // trajectory
    public int point_number; // trajectory's point number
    private Vector2 projectile_acc = new Vector2 (1.0f, 1.0f); // for newton formula
    private Vector2 projectile_velocity;
    public float ground_y;
    public float projectile_constant;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        naturalGravity = rb.gravityScale;
        bar = GameObject.Find("bar").GetComponent<bar>();
        startPos = transform.position;
        projectile_line.useWorldSpace = true;
        ground_y = -3.37f;
        projectile_constant = (projectile_constant < 4f)? 4f : projectile_constant;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        // jump
        if (Input.GetButtonDown("Jump") && canJump){
            canJump = false;
            // print("jump");
            rb.AddForce(jumpHeight, ForceMode2D.Impulse);
            rb.gravityScale = naturalGravity;
        }

        // if player jump too down or too high
        if (transform.position.y <= -10f || transform.position.y >= 100f){
            transform.position = startPos;
        }

        // MuseButton 0: left ; 1 : right.        
        // count accumlated charged time for projectile
        if (Input.GetMouseButton(0)){
            animator_player.SetBool("IsThrow", true);
            projectile_line.enabled = true;
            charged_time += Time.deltaTime;
            charged_time = (charged_time > 3f ? 3f : charged_time);
            // Input.mousePosition
            Vector2 temp_projectile_velocity = new Vector2(projectile_acc.x * face_direction, projectile_acc.y * (Input.mousePosition.y / Screen.height));
            projectile_velocity = projectile_constant * charged_time * temp_projectile_velocity / (temp_projectile_velocity.magnitude);
            // CoRoutine should be here
            StartCoroutine(DrawTrajectory(projectile_velocity));
        }

        if (Input.GetMouseButtonUp(0)){
            animator_player.SetBool("IsThrow", false);
            projectile_line.enabled = false;
            ProjectItem();
            charged_time = 1f;
        }
        
        if (Input.GetMouseButtonUp(1)){
            canMove = false;
            DropItem();
            StartCoroutine(Settle_Delay(1.0f));          
        }


    }
    void FixedUpdate ()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputX_Raw = Input.GetAxisRaw("Horizontal"); // record face direction
        float inputY = Input.GetAxis("Vertical");
        float posY = transform.position.y;
        float posX = transform.position.x;

        // for animator
        animator_player.SetFloat("Player_RunSpeed", Mathf.Abs(inputX));

        // detect where player faces
        if (inputX_Raw != 0f){
            face_direction = inputX_Raw;
        }

        //climb  
        if (climb && inputY != 0){
            animator_player.SetBool("IsClimb", true);
            float ropeX = rope.transform.position.x;
            if (Mathf.Abs(posX - ropeX) <= 0.3f){
                // gravity set to zero when climb
                rb.gravityScale =0;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
                    RigidbodyConstraints2D.FreezeRotation;
                transform.position = new Vector3(ropeX, rb.position.y);
                rb.velocity = new Vector2(0f, inputY*climbSpeed);
            }
        }
        if(!climb){
            animator_player.SetBool("IsClimb", false);
            rb.gravityScale = naturalGravity;
        }
        //move
        if(inputX != 0 && canMove){
            if (inputX > 0 && !movingRight)
                flip();
            else if (inputX < 0 && movingRight)
                flip();
            Vector3 move = new Vector3(runSpeed * inputX , posY, 0);
            move *= Time.deltaTime;
            transform.Translate(move);
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
    // project prefab from (player's coordinate + some hight)
    private void ProjectItem(){
        cur_projectile = Instantiate(projectile, transform.position + new Vector3 (0.0f,0.5f,0.0f), Quaternion.identity);
        Rigidbody2D cur_projectile_rb = cur_projectile.GetComponent<Rigidbody2D>();
        cur_projectile_rb.angularVelocity = 0f;
        cur_projectile_rb.velocity += projectile_velocity;
    }
    
    private IEnumerator DrawTrajectory(Vector2 prefab_velocity){
        projectile_line.positionCount = point_number;
        projectile_line.SetPositions(TrajectoryGenerator(prefab_velocity));
        yield return null;
    }

    private Vector3[] TrajectoryGenerator(Vector2 prefab_velocity){
        Vector3[] Generated_points = new Vector3[point_number];
        point_number = (point_number == 0)? 200 : point_number; // avoid divided by 0
        float point_density = Newton_Trajectory_HitGround_Time(ground_y, prefab_velocity).x / point_number;
        for (int i = 0; i < point_number; ++i){
            //float time_in_Newton = (float)(i / point_number); // input t for Newton Vo*t + 0.5*a*t^2
            float time_in_Newton = point_density * i;
            Generated_points[i] = Newton_Trajectory_Coordinate(time_in_Newton, prefab_velocity);
        }
        //print("last_point"+Generated_points[(int)((point_density * point_number)-2)]);
        return  Generated_points;
    }

    // classic Newton formula x = Vo*t + 0.5*a*t^2 
    private Vector2 Newton_Trajectory_Coordinate(float time_in_Newton, Vector2 prefab_velocity){
        float X = transform.position.x + prefab_velocity.x * time_in_Newton ;
        float Y = transform.position.y + prefab_velocity.y * time_in_Newton + 0.5f * Physics2D.gravity.y * time_in_Newton * time_in_Newton; // gravityScale can be added, g = -9.8f
        return new Vector2 (X, Y);
    }

    // calculate projectile destination (when hit ground).
    private Vector2 Newton_Trajectory_HitGround_Time(float ground_y, Vector2 prefab_velocity){
        // Y = V0(y) * t + 0.5 * a * t^2 ; (V0, Y, a) are all constants
        // Y is ground_y - projectile_ini_y, so use  -(b / 2*a) +/- (b^2-4ac)^0.5 to solve
        // temp_1 is (-b / 2*a); temp_2 is (b^2-4ac)^0.5 >>> temp_1 is highest point time; ans is hit ground time.
        float Y = ground_y - transform.position.y;
        float temp_1 = -prefab_velocity.y / (Physics2D.gravity.y); // can add gravity scale
        float temp_2 = Mathf.Pow(Mathf.Pow(prefab_velocity.y, 2) + 2 * Physics2D.gravity.y * Y ,0.5f);
        float ans = (temp_2 < 0)? (temp_1 - temp_2) : (temp_1 + temp_2);
        return new Vector2(ans, temp_1);
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
            // print(other.gameObject.tag);
            canJump = true;
        }
    }

    public void attacked(int direction, int damageAmount){
        // stop move while being attacked
        canMove = false;
        Vector2 layback = new Vector2(direction*moveForce,jumpForce);
        rb.AddForce(layback, ForceMode2D.Impulse);
        canJump = false;

        // rb.velocity = new Vector2(direction * moveForce, jumpForce);

        healthSystem.Damage(damageAmount);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
        
        if (healthSystem.GetHealth() == 0){
            Restart();
        }
        // can move after n sec later
        StartCoroutine(canMoveAfterSec(0.7f));

    }

    IEnumerator canMoveAfterSec(float time){
        animator_player.SetBool("IsHurt", true);
        yield return new WaitForSeconds (time);
        canMove = true;
        animator_player.SetBool("IsHurt", false);
    }

    IEnumerator Settle_Delay(float time){
        animator_player.SetBool("IsSettle", true);
        yield return new WaitForSeconds (time);
        animator_player.SetBool("IsSettle", false);
        canMove = true;
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
