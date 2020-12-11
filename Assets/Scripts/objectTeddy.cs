using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTeddy : MonoBehaviour
{
    public int damageAmount = 10;
    enemyTubBubble script;
    float dieAfterSec;
    List<GameObject> enemies;
    public float AttackBetweenTime = 0.5f;
    float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        script = transform.parent.gameObject.GetComponent<enemyTubBubble>();
        dieAfterSec = script.dieAfterSec;
        // StartCoroutine(stopGrabEnemy(dieAfterSec-0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            print(other.gameObject.name);
            enemies.Add(other.gameObject);
            if (Time.time - lastTime >= AttackBetweenTime){
                grabEnemy(other.gameObject);
                lastTime = Time.time;
            }
        }
    }

    void grabEnemy(GameObject enemy){
        enemy.GetComponent<enemyBasic>().canMove = false;
        enemy.transform.position = transform.position+ new Vector3(0,1f,0);
        enemy.transform.eulerAngles = new Vector3(0,0,Random.Range(-30f,30f));
        enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        enemy.GetComponent<enemyBasic>().attacked(0, damageAmount);
    }
    IEnumerator stopGrabEnemy(float dieAfterSec){
        yield return new WaitForSeconds (dieAfterSec);
        foreach(GameObject enemy in enemies){
            enemy.GetComponent<enemyBasic>().canMove = true;
            enemy.GetComponent<Rigidbody2D>().gravityScale = 5;
            enemy.transform.eulerAngles = Vector3.zero;
        }
    }
}
