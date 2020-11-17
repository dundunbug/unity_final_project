using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTubBubble : MonoBehaviour
{
    // dies after n sec
    public float dieAfterSec = 3f; 
    public GameObject explodeItem;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destoryObject(dieAfterSec));
        
    }
    IEnumerator destoryObject(float dieAfterSec){
        yield return new WaitForSeconds (dieAfterSec);
        if (gameObject.name.Contains("lava")){
            GameObject expObj = GameObject.Instantiate(explodeItem, gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }else{
            Destroy(gameObject);
        }
    }


}
