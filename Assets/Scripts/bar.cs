using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    private Text healthText;
    public int num = 0;
    int health = 100;
    int healthMax = 100;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("value").GetComponent<Text>();
        transform.localScale = new Vector3(1,1,1);
        healthText.text = health.ToString()+"/"+healthMax.ToString();
    }

    public void ChangeHealthStatus(int health){
        float percentage = (float) health / healthMax;
        transform.localScale = new Vector3(percentage,1,1);
        healthText.text = health.ToString()+"/"+healthMax.ToString();
    }

}
