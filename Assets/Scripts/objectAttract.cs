using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectAttract : MonoBehaviour
{
    // Attracts monster in a certain range
    public float radius = 5f;
    public int time = 5;
    public float speed = 5f;
    public float posRange = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        // remove bomb after n sec
        StartCoroutine(DestroyGameObject(time));
    }

    void Update(){
        // Get nearby objects
        int enemyLayer = 1 << LayerMask.NameToLayer("enemyOnGround");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);
        foreach(Collider2D nearbyObject in colliders){
            GameObject enemy = nearbyObject.gameObject;
            if (nearbyObject.gameObject.tag == "Detector"){
                continue;
            }
            if (Mathf.Abs(enemy.transform.position.x - transform.position.x) >= posRange){
                Vector2 pos = new Vector2(transform.position.x, enemy.transform.position.y);
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, pos, speed * Time.deltaTime);
            }
        }
    }

    IEnumerator DestroyGameObject(int time){
        yield return new WaitForSeconds (time);
        Destroy(gameObject);
    }
}
