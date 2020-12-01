using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoss : MonoBehaviour
{
    public GameObject player; 
    public GameObject grounds;
    public GameObject fireball;
    public float transportTime = 3f;
    public float fireballTime = 1f;
    float transportLastTime = 0f;
    float fireballLastTime = 0f;
    enemyBasic enemy_script;
    Transform[] groundsTS;
    // Start is called before the first frame update
    void Start()
    {
        enemy_script = GetComponent<enemyBasic>();
        groundsTS = grounds.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - transportLastTime >= transportTime){
            transportLastTime = Time.time;
            transport();
        }
        if (Time.time - fireballLastTime >= fireballTime){
            fireballLastTime = Time.time;
            fireballAttack();
        }
    }
    void transport(){
        int index = checkWhichPlatform();
        int changeIndex = index;
        while (changeIndex == index){
            changeIndex = Random.Range(1,groundsTS.Length);
        }
        Vector3 newPos = groundsTS[changeIndex].position + new Vector3(0,2f,0);
        transform.position = newPos;
    }

    int checkWhichPlatform(){
        // check if ground edge 
        int groundLayer = 1 << LayerMask.NameToLayer("ground");
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, 5, groundLayer);
        Transform currentPlatform;
        // if player is not near, patrol
        if (groundInfo.collider != false){
            currentPlatform = groundInfo.collider.gameObject.transform;
            for (var i = 0; i < groundsTS.Length; i++){
                if (currentPlatform == groundsTS[i]){
                    // print(i);
                    return i;
                }
            }
        }
        return 0;
    }
    void fireballAttack(){
        Vector2 playerPos = player.transform.position;
        GameObject obj = GameObject.Instantiate(fireball, transform.position, 
    Quaternion.identity) as GameObject;
        
        float angle = GetAngle(transform.position,playerPos);
        obj.transform.rotation = Quaternion.Euler(0,0,angle);

        enemyBossFireball fireball_script = obj.GetComponent<enemyBossFireball>();
        // fireball_script.target = playerPos;
        Vector3 vector = (player.transform.position-transform.position).normalized;
        fireball_script.target = vector;
        fireball_script.canMove = true;

    }
    float GetAngle(Vector2 start, Vector2 end){
        Vector2 v2 = end - start;
        float angle = Mathf.Atan2(v2.y, v2.x)*Mathf.Rad2Deg;
        return angle;
    }
}
