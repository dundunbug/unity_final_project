using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectTeddy : MonoBehaviour
{
    public int damageAmount = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            print(other.gameObject.name);
            grabEnemy(other.gameObject);
        }
    }

    void grabEnemy(GameObject enemy){
        enemy.GetComponent<enemyBasic>().canMove = false;
        enemy.transform.position = transform.position+ new Vector3(0,1f,0);
        enemy.transform.eulerAngles = new Vector3(0,0,Random.Range(-30f,30f));
        enemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        enemy.GetComponent<enemyBasic>().attacked(0, damageAmount);
    }
}
