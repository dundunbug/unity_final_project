using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBomb : MonoBehaviour
{
    //bomb explodes when touching the enemy or ground
    public float radius = 3f;
    public int explodeAmount = 10;
    private objectScript objectScript;
    private bool hasExploded = false;
    // Start is called before the first frame update
    void Awake()
    {
        objectScript = new objectScript(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            if (!hasExploded)
                objectScript.Explode(radius, explodeAmount);
                hasExploded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            if(!hasExploded)
                objectScript.Explode(radius, explodeAmount);
                hasExploded = true;
        }
    }

}
