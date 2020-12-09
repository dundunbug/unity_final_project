using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Player: " + collision.gameObject.name);
    }
}
