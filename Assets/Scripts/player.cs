using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class player : MonoBehaviour
{
    public int HealthMax=100;
    public int DeathCount;
    [Header("movement")]
    // controller;
    public float runSpeed = 10f;
    public float climbSpeed = 1f;
    public Vector2 jumpHeight = new Vector2(0f,12f);
    public float naturalGravity;
    // touched by monster 
    [Header("hurt force")]
    public int moveForce = 5;
    public int jumpForce = 5;
    public float moveAfterSec = 0.5f;
    [Header("throw item")]
    public GameObject dropped_item; // prefab of dropped item
    private GameObject cur_dropped_item; //  current dropped item

    public GameObject projectile; // prefab of projectile
    private GameObject cur_projectile; // current projectile
    [HideInInspector] public rope rope;
    private Vector3 startPos;
    // private bool crouch = false;
    private Rigidbody2D rb;
    private SpriteRenderer SpriteRenderer;
    [HideInInspector] public bar bar;
    [HideInInspector] public healthSystem healthSystem;
    // Start is called before the first frame update
    [Header("Status_Bool")]
    private bool canJump = false;
    private bool canMove = true;
    private bool movingRight = true;
    [HideInInspector] public bool climb = false;
    
    [Header("Animator")]
    public Animator animator_player;

    [Header("Projectile")]
    public Vector3 throwStart;
    private float charged_time = 1.0f; // for accumulated projectile charged time. 0 will become "drop".
    public float face_direction = 1.0f; // record where player faces. because transform.forward doesn't work in 2D
    public LineRenderer projectile_line; // trajectory
    public int point_number; // trajectory's point number
    private Vector2 projectile_velocity;
    public float ProjectileSampleRate;
    public LayerMask canHit;
    private Vector3 mousePositionInput;
    private Vector3 mousePositionWorld;
    //public float ground_y;
    //public float projectile_constant;
    //public Vector2 projectile_acc; // for newton formula
    //public Vector2 projectile_force; // projectile (thrown item) force

    [Header("Inventory")]
    /*Inventory 各種*/
    public Inventory inventory;
    public EnergySystem energySystem;
    [SerializeField] public UI_Inventory uiInventory;
    public GameObject inventoryCanvas;
    public int dropItemCount = 0;

    [Header("GameSave")]
    GameData gameData;

    [Header("ScorePage")]
    public GameObject Panel_Lose;
    public UIchangeItem changeThrowItem;
    public UIchangeItem changeDropItem;
    public int DefeatedNum = 0;

    private player_ability player_Ability;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        naturalGravity = rb.gravityScale;
        bar = GameObject.Find("bar").GetComponent<bar>();
        startPos = transform.position;
        projectile_line.useWorldSpace = true;
        SpriteRenderer = GetComponent<SpriteRenderer>();
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        inventory = new Inventory();
        if (gameData.LoadedData != null) { Debug.Log("inventoryloaded"); LoadInventory(); }
        uiInventory.SetInventory(inventory);
        energySystem.SetInventory(inventory);
        DeathCount = (DeathCount < 1) ? 1: DeathCount;
        healthSystem = new healthSystem(HealthMax);
        player_Ability = GetComponent<player_ability>();

    }

    // Update is called once per frame
    void Update () {
        mousePositionInput = Input.mousePosition;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionInput);

        // if player jump too down or too high
        // if (transform.position.y <= -10f || transform.position.y >= 100f){
        //     transform.position = startPos;
        // }

        // MuseButton 0: left ; 1 : right.        
        // count accumlated charged time for projectile
        if (Input.GetMouseButton(0) && !inventoryCanvas.active && !EventSystem.current.IsPointerOverGameObject()){
            
            animator_player.SetBool("IsThrow", true);
            projectile_line.enabled = true;
            charged_time += Time.deltaTime;
            charged_time = (charged_time > 3f ? 3f : charged_time);
            // Input.mousePosition
            // Vector2 temp_projectile_velocity = new Vector2(projectile_acc.x * face_direction, projectile_acc.y);
            projectile_velocity = ProjectileSampleRate * charged_time * (mousePositionWorld - transform.position).normalized; 
            // CoRoutine should be here
            // StartCoroutine(DrawMousePoint(mousePositionWorld));
            StartCoroutine(DrawTrajectory(projectile_velocity));
        }

        if (Input.GetMouseButtonUp(0) && !inventoryCanvas.active && !EventSystem.current.IsPointerOverGameObject()){
            throwStart = transform.position;
            animator_player.SetBool("IsThrow", false);
            projectile_line.enabled = false;
            ProjectItem();
            charged_time = 1f;
            changeThrowItem.itemUsed();
            changeThrowItem.refreshList();
            changeDropItem.refreshList();
        }
        
        if (Input.GetMouseButtonUp(1) && !inventoryCanvas.active){
            canMove = false;
            DropItem();
            StartCoroutine(Settle_Delay(1.0f));
            changeDropItem.itemUsed();
            changeDropItem.refreshList();
            changeThrowItem.refreshList();
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputX_Raw = Input.GetAxisRaw("Horizontal"); // record face direction
        float inputY = Input.GetAxis("Vertical");
        float posY = transform.position.y;
        float posX = transform.position.x;

        // for animator
        animator_player.SetFloat("Player_RunSpeed", Mathf.Abs(inputX));

        // jump
        if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")){
            if ( canJump){
                canJump = false;
                // print("jump");
                if (inputX >= 0.2f || inputX <= -0.2f){
                    print(jumpHeight+ new Vector2(0f,8f));
                    rb.AddForce(jumpHeight+ new Vector2(0f,8f), ForceMode2D.Impulse);
                }else{
                    rb.AddForce(jumpHeight, ForceMode2D.Impulse);
                }
                rb.gravityScale = naturalGravity;
            }

        }
        // detect where player faces
        if (inputX_Raw != 0f){
            face_direction = inputX_Raw;
        }

        //climb  
        // if (climb && inputY != 0){
        //     animator_player.SetBool("IsClimb", true);
        //     float ropeX = rope.transform.position.x;
        //     if (Mathf.Abs(posX - ropeX) <= 0.3f){
        //         // gravity set to zero when climb
        //         rb.gravityScale =0;
        //         rb.constraints = RigidbodyConstraints2D.FreezePositionX | 
        //             RigidbodyConstraints2D.FreezeRotation;
        //         transform.position = new Vector3(ropeX, rb.position.y);
        //         rb.velocity = new Vector2(0f, inputY*climbSpeed);
        //     }
        // }
        // if(!climb){
        //     animator_player.SetBool("IsClimb", false);
        //     rb.gravityScale = naturalGravity;
        // }
        //move
        if(inputX != 0 && canMove){
            if (inputX > 0 && !movingRight)
                flip();
            else if (inputX < 0 && movingRight)
                flip();
            Vector3 move = new Vector3(runSpeed * inputX, 0, 0);
            move *= Time.deltaTime;
            transform.Translate(move);
        }
    }
    void FixedUpdate ()
    {

        
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
        if (projectile != null){
            cur_projectile = Instantiate(projectile, transform.position + new Vector3 (0.0f,0.5f,0.0f), Quaternion.identity);
            Rigidbody2D cur_projectile_rb = cur_projectile.GetComponent<Rigidbody2D>();
            cur_projectile_rb.angularVelocity = 0f;
            cur_projectile_rb.velocity += projectile_velocity;
            player_Ability.itemDamageAdd(projectile, cur_projectile);
        }
    }
    
    private IEnumerator DrawTrajectory(Vector2 prefab_velocity){
        projectile_line.positionCount = point_number;
        projectile_line.SetPositions(TrajectoryGenerator(prefab_velocity));
        yield return null;
    }

    private Vector3[] TrajectoryGenerator(Vector2 prefab_velocity){
        Vector3[] Generated_points = new Vector3[point_number];
        point_number = (point_number == 0)? 50 : point_number; // avoid divided by 0
        // TrajectoryTouchGround(prefab_velocity).y 下列function第一個arg代此 為觸地
        // Newton_Trajectory_HitGround_Time(transform.position.y, prefab_velocity).y 為print到最高處
        // 目前預期會print到腳色y軸 但會print到地下
        float point_density = Newton_Trajectory_HitGround_Time(transform.position.y, prefab_velocity).x / point_number;
        // float point_density = Newton_Trajectory_HitGround_Time(prefab_velocity).x / point_number;
        for (int i = 0; i < point_number; ++i){
            //float time_in_Newton = (float)(i / point_number); // input t for Newton Vo*t + 0.5*a*t^2
            float time_in_Newton = point_density * i;
            Generated_points[i] = Newton_Trajectory_Coordinate(time_in_Newton, prefab_velocity);
        }
        //print("last_point"+Generated_points[(int)((point_density * point_number)-2)]);
        return  Generated_points;
    }
    /*private IEnumerator DrawMousePoint(Vector3 mousePositionWorld){
        projectile_line.positionCount = point_number;
        projectile_line.SetPositions(MousePointGenerator(mousePositionWorld));
        yield return null;
    }*/
    /*private Vector3[] MousePointGenerator(Vector3 mousePositionWorld){
        Vector3[] Generated_points = new Vector3[point_number];
        point_number = (point_number == 0)? 50 : point_number; // avoid divided by 0
        Vector3 point_density = (mousePositionWorld - transform.position) / point_number;
        for (int i = 0; i < point_number; ++i){
            //float time_in_Newton = (float)(i / point_number); // input t for Newton Vo*t + 0.5*a*t^2
            Vector3 NextPoint = point_density * i;
            Generated_points[i] = transform.position + NextPoint;
        }
        //print("last_point"+Generated_points[(int)((point_density * point_number)-2)]);
        return  Generated_points;
    }*/
    // classic Newton formula x = Vo*t + 0.5*a*t^2 
    private Vector2 Newton_Trajectory_Coordinate(float time_in_Newton, Vector2 prefab_velocity){
        float X = transform.position.x + prefab_velocity.x * time_in_Newton ;
        float Y = transform.position.y + prefab_velocity.y * time_in_Newton + 0.5f * Physics2D.gravity.y * time_in_Newton * time_in_Newton; // gravityScale can be added, g = -9.8f
        return new Vector2 (X, Y);
    }

    // calculate projectile destination (when hit ground).
    private Vector2 Newton_Trajectory_HitGround_Time(float ground, Vector2 prefab_velocity){
        // Y = V0(y) * t + 0.5 * a * t^2 ; (V0, Y, a) are all constants
        // Y is ground_y - projectile_ini_y, so use  -(b / 2*a) +/- (b^2-4ac)^0.5 to solve
        // temp_1 is (-b / 2*a); temp_2 is (b^2-4ac)^0.5 >>> temp_1 is highest point time; ans is hit ground time.
        float Y = ground - transform.position.y;
        float temp_1 = -prefab_velocity.y / (Physics2D.gravity.y); // can add gravity scale
        float temp_2 = Mathf.Pow(Mathf.Pow(prefab_velocity.y, 2) + 2 * Physics2D.gravity.y * (ground - transform.position.y) ,0.5f);
        float ans = (temp_2 < 0)? (temp_1 - temp_2) : (temp_1 + temp_2);
        return new Vector2(ans, temp_1);
    }

    private Vector2 TrajectoryTouchGround(Vector2 prefab_velocity)
    {
        // 如果沒觸地print到很下面
        float MaxTime = Newton_Trajectory_HitGround_Time(-10, prefab_velocity).x;
        for (int i = 0; i < point_number; ++i)
        {
            float temp_time = (MaxTime / point_number)*i;
            float temp_time_plus = (MaxTime / point_number)*(i+1);
            var hitGround = Physics2D.Linecast(Newton_Trajectory_Coordinate(temp_time, prefab_velocity), Newton_Trajectory_Coordinate(temp_time, prefab_velocity), canHit);
            if (hitGround)
            {
                return hitGround.point;
            }
        }
        return 
        Newton_Trajectory_Coordinate(MaxTime, prefab_velocity);
    }

    // drop prefab from player's coordinate
    private void DropItem(){
        if (dropped_item != null){
            cur_dropped_item = Instantiate(dropped_item, transform.position, Quaternion.identity);
            Rigidbody2D cur_dropped_rb = cur_dropped_item.GetComponent<Rigidbody2D>();
            cur_dropped_rb.angularVelocity = 0f;
            player_Ability.itemDamageAdd(dropped_item, cur_dropped_item);
        }
    }

    public void PickItem(Item.ItemType type)
    {
        inventory.AddItem(new Item { itemType = type});
        changeThrowItem.refreshList();
        changeDropItem.refreshList();
    }
   
    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Ground"){
            // print(other.gameObject.tag);
            canJump = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag== "Teleport"){
            // print(other.gameObject.tag);
            if (objectPainting.onGround){
                transform.position = objectPainting.TeleportingGate;
            }
        }else if (other.gameObject.tag== "Ground"){
            canJump = true;
        }
    }
    public void attacked(int direction, int damageAmount){
        // stop move while being attacked
        canMove = false;
        if (direction != 0){
            Vector2 layback = new Vector2(direction*moveForce,jumpForce);
            rb.AddForce(layback, ForceMode2D.Impulse);
        }
        canJump = false;

        // rb.velocity = new Vector2(direction * moveForce, jumpForce);
        healthSystem.Damage(damageAmount);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
        
        if (healthSystem.GetHealth() == 0){
            Restart();
            rb.velocity = Vector3.zero;
        }
        // can move after n sec later
        StartCoroutine(canMoveAfterSec(0.5f));

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
        if (DeathCount <= 0)
        {
            Panel_Lose.SetActive(true);
            //gameData.LevelPassed = gameData.Level;
        }
        transform.position = startPos;
        healthSystem.Reset();
        bar.ChangeHealthStatus(healthSystem.GetHealth());
        DeathCount -= 1;
    }

    public void LoadInventory()
    {
        for (int i = 0; i < 11; i++)
        {
            if (gameData.LoadedData.items[i] <= 0) continue;
            if (i == 0) inventory.AddItem(new Item { itemType = Item.ItemType.Bomb_L, Num = gameData.LoadedData.items[i] });
            if (i == 1) inventory.AddItem(new Item { itemType = Item.ItemType.Bomb_S, Num = gameData.LoadedData.items[i] });
            if (i == 2) inventory.AddItem(new Item { itemType = Item.ItemType.Bomb_Timer, Num = gameData.LoadedData.items[i] });
            if (i == 3) inventory.AddItem(new Item { itemType = Item.ItemType.Teddy, Num = gameData.LoadedData.items[i] });
            if (i == 4) inventory.AddItem(new Item { itemType = Item.ItemType.TransferGate, Num = gameData.LoadedData.items[i] });
            if (i == 5) inventory.AddItem(new Item { itemType = Item.ItemType.Lego, Num = gameData.LoadedData.items[i] });
            if (i == 6) inventory.AddItem(new Item { itemType = Item.ItemType.CardBoard, Num = gameData.LoadedData.items[i] });
            if (i == 7) inventory.AddItem(new Item { itemType = Item.ItemType.Bottle, Num = gameData.LoadedData.items[i] });
            if (i == 8) inventory.AddItem(new Item { itemType = Item.ItemType.Carton, Num = gameData.LoadedData.items[i] });
            if (i == 9) inventory.AddItem(new Item { itemType = Item.ItemType.Pillow, Num = gameData.LoadedData.items[i] });
            if (i == 10) inventory.AddItem(new Item { itemType = Item.ItemType.DroppedItem, Num = gameData.LoadedData.items[i] });
        }

    }

}
