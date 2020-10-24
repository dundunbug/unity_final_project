using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBombTrigger : MonoBehaviour
{
    //trigger bomb after 3 secs
    public float timing = 3f;
    public float radius = 3f;
    private float countdown;
    private bool hasExploded = false;
    private objectScript objectScript;
    // Start is called before the first frame update
    void Start()
    {
        objectScript = new objectScript(gameObject);
        countdown = timing;
    }
    
    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded){
            objectScript.Explode(radius);
            hasExploded = true;
        }
    }
}
