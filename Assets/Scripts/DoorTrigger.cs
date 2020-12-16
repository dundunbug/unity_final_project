using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door;
    private bool down = false;
    private Animation door_animation;
    private Animation m_animation;
    private int done = 0;
    private bool DestroyFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        //if(door != null)
        //{
        //    door_animation = door.GetComponent<Animation>();
        //}
        //m_animation = gameObject.GetComponent<Animation>();    
    }

    void OnTriggerStay2D (Collider2D col)
    {
        if (done == 0 && col.gameObject.tag == "Player")
        {
            /*if (door_animation != null && (col.gameObject.transform.position.x - transform.position.x) < 0.1)
            {
                m_animation.Play("ButtonDown");
                door_animation.Play("OpenDoor");
                done = 1;
            }*/
            if ((col.gameObject.transform.position.x - transform.position.x) < 0.1)
            down = true;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (down == true)
        {
            OpenDoor();
        }
        
    }
    void OpenDoor()
    {
        if(DestroyFlag)
        {
            Destroy(gameObject, 5.0f);
            Destroy(door, 5.0f);
            DestroyFlag = false;
        }
        gameObject.transform.position -= new Vector3 (0.0f, 0.01f, 0.0f);
        door.transform.position -= new Vector3 (0.0f, 0.03f, 0.0f);
    }
}
