using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPainting : MonoBehaviour
{
    //bomb explodes when touching the enemy or ground
    public float radius = 3f;
    private objectScript objectScript;
    public GameObject teleporting_start;
    private GameObject cur_teleporting_start;
    private player player_script;
    private GameObject player;
    public static Vector3 TeleportingGate;
    // Start is called before the first frame update
    void Start()
    {
        objectScript = new objectScript(gameObject);
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag== "Ground"){
            cur_teleporting_start = Instantiate(teleporting_start, player.transform.position + new Vector3 (player_script.face_direction, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    void Update(){
        TeleportingGate = transform.position;
    }
}
