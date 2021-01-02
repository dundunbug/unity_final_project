using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBar : MonoBehaviour
{
    public healthSystem healthSystem;
    int healthMax, health;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ChangeHealthStatus(){
        healthSystem = boss.GetComponent<enemyBasic>().healthSystem;
        healthMax = healthSystem.GetHealthMax();
        health = healthSystem.GetHealth();
        float percentage = (float) health / healthMax;
        transform.localScale = new Vector3(percentage,1,1);
        // healthText.text = health.ToString()+"/"+healthMax.ToString();
    }
}
