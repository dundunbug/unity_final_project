using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateEnemy : MonoBehaviour
{
    public GameObject dog,mouse,feed,tub;
    public bool randomPosition = false;
    public float randomPositionRange = 2f;
    public int[] enemyCount = new int[4];
    private GameObject[] enemies = new GameObject[4];
    public float damageExpand = 1f;
    //private GameObject prehab;
    // Start is called before the first frame update
    void Start()
    {
        enemies[0] = dog;
        enemies[1] = mouse;
        enemies[2] = feed;
        enemies[3] = tub;
        GenerateEnemy();
    }

    void GenerateEnemy(){
        for(int i =0;i< enemyCount.Length; i++){
            for (int j=0; j< enemyCount[i]; j++){
                // GameObject prehab = new GameObject();
                if (!randomPosition){
                    StartCoroutine(Random_Delay(enemies[i],transform.position));
                    //prehab = Instantiate(enemies[i], transform.position, Quaternion.identity, transform);
                }else{
                    float ranX = Random.Range(transform.position.x-randomPositionRange,transform.position.x+randomPositionRange);
                    Vector3 ranPos = new Vector3(ranX,transform.position.y,transform.position.z);
                    StartCoroutine(Random_Delay(enemies[i],ranPos));
                    //prehab = Instantiate(enemies[i], ranPos, Quaternion.identity, transform);
                }
            }
        }
    }

    private IEnumerator Random_Delay(GameObject enemy, Vector3 generate_position)
    {
        float temptime = Random.Range (1.0f, 5.0f);
        yield return new WaitForSecondsRealtime(temptime);
        GameObject instantiate_assign = Instantiate(enemy, transform.position, Quaternion.identity, transform);
        if (instantiate_assign.GetComponent<enemyBasic>()!=null){
            // random flip
            int flipEnemy = Random.Range(0,2);
            if (flipEnemy == 1){
                instantiate_assign.GetComponent<enemyBasic>().flip();
            }
            // damageAmount
            if (damageExpand != 1f){
                instantiate_assign.GetComponent<enemyBasic>().damageAmount 
                    = (int)Mathf.Round(instantiate_assign.GetComponent<enemyBasic>().damageAmount*damageExpand);
                instantiate_assign.GetComponent<enemyBasic>().dropObjectNum 
                    = (int)Mathf.Round(instantiate_assign.GetComponent<enemyBasic>().dropObjectNum*damageExpand);
                instantiate_assign.GetComponent<enemyBasic>().speed += damageExpand/2;
            }
        }
        if (instantiate_assign.name.Contains("tub") && damageExpand != 1f){ // if it is tub
            instantiate_assign.GetComponent<enemyTubAttack>().damageAmount 
                = (int)Mathf.Round(instantiate_assign.GetComponent<enemyTubAttack>().damageAmount*damageExpand);
        }
    }
}
