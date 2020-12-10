using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
炸彈(大、小、定時)ˊ
泰迪熊(惡靈)
畫框(傳送)
積木(障礙、平台)
紙板(障礙、平台)
泡沫瓶(漫出、持續傷害)
紙箱(吸引怪物)
枕頭(塵暴 二次點燃)
*/

/*
basic
    every object:   attack when being touched(in certain range)
    timing: 定時trigger 炸彈(大、小、定時)ˊ
    can step on:    障礙物/平台
    吸引enemy:      紙箱(吸引怪物)
*/
/*
Edvance
    ---
    泰迪熊(惡靈)???    
    portal:     畫框(傳送)
    大範圍攻擊:     枕頭(塵暴 二次點燃) 泡沫瓶(漫出、持續傷害)
*/
public class objectStatus : MonoBehaviour
{
    public static float timer;
    public bool isBomb = false;
    [Header("IF is Bomb")]
    public float radius = 4f;
    public int explodeAmount = 10;
    [Header("status")]
    public int healthMax = 40;
    public float dieAfterSec = 3f;
    public float explodeAfterSec = 0f;
    private healthSystem healthSystem;
    private objectScript objectScript;
    private Animator animator_object;
    // Start is called before the first frame update
    void Start()
    {
        objectScript = new objectScript(gameObject);
        healthSystem = new healthSystem(healthMax);
        animator_object = GetComponent<Animator>();
    }
    public void attackObject(int damageAmount){
        if (healthSystem.GetHealth() != 0){
            healthSystem.Damage(damageAmount);
            // print(healthSystem.GetHealth());
            if (healthSystem.GetHealth() == 0){
                if (isBomb){
                    StartCoroutine(explodeAfter(explodeAfterSec));
                }
                else{
                    if (gameObject.name.Contains("pillow")){
                        // feather effect
                        // GameObject child = gameObject.transform.GetChild(0).gameObject;
                        // child.SetActive(true);
                        gameObject.GetComponent<objectPillow>().FlyFeathers();
                        Destroy(gameObject,dieAfterSec);
                    }else if (gameObject.name.Contains("teddy")){
                        // grab?
                        GameObject child = gameObject.transform.GetChild(0).gameObject;
                        child.SetActive(true);
                    }else if (gameObject.name.Contains("lavaFloor")){
                        gameObject.GetComponent<objectLava>().dropLava(dieAfterSec);
                        Destroy(gameObject,dieAfterSec);
                    }else{
                        if (animator_object != null)
                            animator_object.SetTrigger("isDestroy");
                        Destroy(gameObject,dieAfterSec);
                    }
                }
            }
        }
    }
    IEnumerator explodeAfter(float time){
        yield return new WaitForSeconds (time);
        objectScript.Explode(radius, explodeAmount);
    }
   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.name)
        if(other.gameObject.name == "player")
        {
            other.gameObject.GetComponent<player>().inventory.AddItem(new Item { itemType = Item.ItemType.Lego });
        }
    }*/
}
