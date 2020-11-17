using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectParticle : MonoBehaviour
{
    public int damageAmount = 1;    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    private player player_script;
    private GameObject player;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        player = GameObject.Find("player");
        player_script = player.GetComponent<player>();
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player"){
            player_script.attacked(0,damageAmount);
        }
        else if ( other.gameObject.tag=="Enemy"){
            enemyBasic enemyBasic = other.gameObject.GetComponent<enemyBasic>();
            if (enemyBasic != null){
                enemyBasic.attacked(0,damageAmount);
            }
        }
        else if ( other.gameObject.tag=="Brick"){
            enemyBasic enemyBasic = other.gameObject.GetComponent<enemyBasic>();
            if (enemyBasic != null){
                enemyBasic.attacked(0,damageAmount);
            }
        }
    }
}
