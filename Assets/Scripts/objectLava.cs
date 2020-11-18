using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectLava : MonoBehaviour
{
    public GameObject lava;
    public float dieAfterSec = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void dropLava(float sec){
        StartCoroutine(dropLavaObject(sec-0.1f));
    }

    IEnumerator dropLavaObject(float time){
        yield return new WaitForSeconds (time);
        GameObject obj = GameObject.Instantiate(lava, gameObject.transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<enemyTubBubble>().dieAfterSec = dieAfterSec;
    }
}
