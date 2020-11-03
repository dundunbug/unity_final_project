﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectBomb : MonoBehaviour
{
    //bomb explodes when touching the enemy or ground
    public float radius = 3f;
    private objectScript objectScript;
    // Start is called before the first frame update
    void Start()
    {
        objectScript = new objectScript(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Ground"){
            objectScript.Explode(radius);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // print(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy"){
            objectScript.Explode(radius);
        }
    }
}