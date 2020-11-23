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
                GameObject prehab;
                if (!randomPosition){
                    prehab = Instantiate(enemies[i], transform.position, Quaternion.identity, transform);
                }else{
                    float ranX = Random.Range(transform.position.x-randomPositionRange,transform.position.x+randomPositionRange);
                    Vector3 ranPos = new Vector3(ranX,transform.position.y,transform.position.z);
                    prehab = Instantiate(enemies[i], ranPos, Quaternion.identity, transform);
                }
                if (prehab.GetComponent<enemyBasic>()!=null){
                    // random flip
                    int flipEnemy = Random.Range(0,2);
                    if (flipEnemy == 1){
                        prehab.GetComponent<enemyBasic>().flip();
                    }
                    // damageAmount
                    if (damageExpand != 1f){
                        prehab.GetComponent<enemyBasic>().damageAmount 
                            = (int)Mathf.Round(prehab.GetComponent<enemyBasic>().damageAmount*damageExpand);
                        prehab.GetComponent<enemyBasic>().dropObjectNum 
                            = (int)Mathf.Round(prehab.GetComponent<enemyBasic>().dropObjectNum*damageExpand);
                        prehab.GetComponent<enemyBasic>().speed += damageExpand;
                    }
                }
                if (i ==3 && damageExpand != 1f){ // if it is tub
                    prehab.GetComponent<enemyTubAttack>().damageAmount 
                        = (int)Mathf.Round(prehab.GetComponent<enemyTubAttack>().damageAmount*damageExpand);
                }
            }
        }
    }
}
