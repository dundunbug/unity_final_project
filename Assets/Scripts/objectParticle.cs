using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectParticle : MonoBehaviour
{
    public int damageAmount = 5;    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player"){
            player playerScript = other.gameObject.GetComponent<player>();
            playerScript.attacked(0,damageAmount);
        }else if ( other.gameObject.tag=="Enemy"){
            enemyBasic enemyBasic = other.gameObject.GetComponent<enemyBasic>();
            if (enemyBasic != null){
                enemyBasic.attacked(0,damageAmount);
            }
        }
    }
}
