using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTubBubble : MonoBehaviour
{
    // dies after n sec
    public float dieAfterSec = 3f; 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, dieAfterSec);
    }


}
