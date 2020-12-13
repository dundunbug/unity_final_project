using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTeddy : MonoBehaviour
{
    public int damageAmount = 10;
    enemyTubBubble script;
    float dieAfterSec;
    List<GameObject> enemies = new List<GameObject>();
    List<float> enemy_gscale = new List<float>();
    public float AttackBetweenTime = 0.5f;
    float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        script = transform.parent.gameObject.GetComponent<enemyTubBubble>();
        dieAfterSec = script.dieAfterSec;
        StartCoroutine(stopGrabEnemy(dieAfterSec-0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime >= AttackBetweenTime){
            grabEnemy();
            lastTime = Time.time;
        }   
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            print(other.gameObject.name);
            enemies.Add(other.gameObject);
            enemy_gscale.Add(other.gameObject.GetComponent<Rigidbody2D>().gravityScale);
        }
    }

    void grabEnemy(){
        if (enemies.Count >= 1){
        foreach(GameObject enemy in enemies){
            if (enemy){
                enemy.GetComponent<enemyBasic>().canMove = false;
                enemy.transform.position = transform.position+ new Vector3(0,1f,0);
                enemy.transform.eulerAngles = new Vector3(0,0,Random.Range(-30f,30f));
                enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
                enemy.GetComponent<enemyBasic>().attacked(0, damageAmount);
            }

        }
        }
    }
    IEnumerator stopGrabEnemy(float dieAfterSec){
        yield return new WaitForSeconds (dieAfterSec);
        for(int i =0; i<enemies.Count; i++){
            GameObject enemy= enemies[i];
            if (enemy){
                enemy.GetComponent<enemyBasic>().canMove = true;
                enemy.GetComponent<Rigidbody2D>().gravityScale = enemy_gscale[i];
                enemy.transform.eulerAngles = Vector3.zero;
            }

        }
    }
}
