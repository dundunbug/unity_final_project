using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMode : MonoBehaviour
{
    public enum PROPERTY
    {
        EASY=0,
        NORMAL=1,
        HARD=2
    }
    public PROPERTY GameMode;
    // Start is called before the first frame update
    private void Awake() {
        ChangeMode();
    }
    void ChangeMode(){
        float expand = 1f;
        if(GameMode == PROPERTY.NORMAL){
            expand = 1.5f;
        }else if (GameMode == PROPERTY.HARD){
            expand = 2f;
        }
        if (GameMode != PROPERTY.EASY)
            foreach(Transform child in transform){
                generateEnemy script = child.gameObject.GetComponent<generateEnemy>();
                // print(script.enemyCount);
                script.damageExpand = expand;
                int[] enemyCount = script.enemyCount;
                for (int i =0; i<script.enemyCount.Length; i++){
                    script.enemyCount[i] = (int)Mathf.Round(script.enemyCount[i]*expand);
                }
            }
    }

}
