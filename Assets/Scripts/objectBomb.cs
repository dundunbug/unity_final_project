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
    audioController audioController;
    // Start is called before the first frame update
    void Awake()
    {
        objectScript = new objectScript(gameObject);
        audioController = GameObject.Find("audioController").GetComponent<audioController>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            if (!hasExploded)
            {
                objectScript.Explode(radius, explodeAmount);
                audioController.playExplosionSmallSFX();
                hasExploded = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy"){
            if (!hasExploded)
            {
                objectScript.Explode(radius, explodeAmount);
                audioController.playExplosionSmallSFX();
                hasExploded = true;
            }
        }
    }

}
