using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player_ability : MonoBehaviour
{
    public int healthAdd = 10;
    public float speedAdd = 0.3f;
    public int damageAdd = 2;
    public List<GameObject> item_prehabs_hasDamage = new List<GameObject>();
    player player_script;
    healthSystem healthSystem;
    bar bar;
    int damageAddTotal = 0;
    // Start is called before the first frame update
    void Start()
    {
        player_script = GetComponent<player>();
        bar = GameObject.Find("bar").GetComponent<bar>();
    }

    public void speedChange(){
        player_script.runSpeed += speedAdd;
    }
    public void healthChange(){
        healthSystem = player_script.healthSystem;
        healthSystem.changeMax(healthAdd);
        bar.ChangeHealthStatus(healthSystem.GetHealth());
    }
    public void itemStatChange(){
        damageAddTotal += damageAdd;
    }
    public void itemDamageAdd(GameObject prehab, GameObject item){
        if (damageAddTotal!=0){
            if (item_prehabs_hasDamage.Contains(prehab)){
                print("add damage");
                if (item.name.Contains("teddy")){
                    //two hands
                    GameObject hand1 = item.transform.GetChild(0).GetChild(1).gameObject;
                    GameObject hand2 = item.transform.GetChild(0).GetChild(2).gameObject;

                    int damageAmount = hand1.GetComponent<objectTeddy>().damageAmount;
                    hand1.GetComponent<objectTeddy>().damageAmount = newDamage(damageAmount);

                    int damageAmount2 = hand2.GetComponent<objectTeddy>().damageAmount;
                    hand2.GetComponent<objectTeddy>().damageAmount = newDamage(damageAmount2);
                }else if (item.name.Contains("Bubble")){
                    int damageAmount = item.GetComponent<objectParticle>().damageAmount;
                    item.GetComponent<objectParticle>().damageAmount = newDamage(damageAmount);
                }else if (item.tag=="bomb"){
                    int explodeAmount = item.GetComponent<objectBomb>().explodeAmount;
                    item.GetComponent<objectBomb>().explodeAmount = newDamage(explodeAmount);
                }else if (item.tag=="triggerBomb"){
                    int explodeAmount = item.GetComponent<objectBombTrigger>().explodeAmount;
                    item.GetComponent<objectBombTrigger>().explodeAmount = newDamage(explodeAmount);
                }else{
                    // feather BigBomb
                    int explodeAmount = item.GetComponent<objectStatus>().explodeAmount;
                    item.GetComponent<objectStatus>().explodeAmount = newDamage(explodeAmount);
                }
            }else{
                print("not int ");
            }
        }
    }

    public int newDamage(int damageAmount){
        // damageAmount += (int)Mathf.Round(damageAmount*damageAdd);
        damageAmount += damageAddTotal;
        return damageAmount;
    }

}
