using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBasic : MonoBehaviour
{
    //Basic enemy status
    //damaged being attacked
    public int moveForce = 5;
    public int jumpForce = 5;

    private healthSystem healthSystem = new healthSystem(50);
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 5;
    }

    public void attackMonster(int direction, int damageAmount){
        Vector2 layback = new Vector2(direction*moveForce,jumpForce);
        rb.AddForce(layback, ForceMode2D.Impulse);
        // rb.velocity = new Vector2(direction * moveForce, jumpForce);
        healthSystem.Damage(damageAmount);
        if (healthSystem.GetHealth() == 0){
            Destroy(gameObject);
        }
    }

}
