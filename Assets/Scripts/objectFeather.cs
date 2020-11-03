using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectFeather : MonoBehaviour
{
    public Vector2 target;
    public bool hasPos = false;
    public float speed = 10f;
    private Rigidbody2D rb;
    private bool fallDown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPos){
            Vector2 dis = target - new Vector2(transform.position.x,transform.position.y);
            if (dis.magnitude > 0.1f && !fallDown){
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }else{
                fallDown = true;
            }
            if (fallDown)
                rb.gravityScale = 0.3f;
            
        }

    }
    
}
