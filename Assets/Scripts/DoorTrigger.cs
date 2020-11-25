using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    private Animation door_animation;
    private Animation m_animation;
    private int done = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(door != null)
        {
            door_animation = door.GetComponent<Animation>();
        }
        m_animation = gameObject.GetComponent<Animation>();    
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if (done == 0 && col.gameObject.tag == "Player")
        {
            if (door_animation != null && (col.gameObject.transform.position.x - transform.position.x) < 0.02)
            {
                m_animation.Play("ButtonDown");
                door_animation.Play("OpenDoor");
                done = 1;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
