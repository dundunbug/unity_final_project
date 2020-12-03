using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGen : MonoBehaviour
{
    public GameObject BossPosition;
    // Start is called before the first frame update
    public GameObject Boss;
    private GameObject cur_Boss;
    private int BossCount = 1;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if ((BossCount > 0) && other.gameObject.tag == "Player"){ 
            cur_Boss = Instantiate(Boss, BossPosition.transform.position, Quaternion.identity);
            BossCount -= 1;
            }
    }
}