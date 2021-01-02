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
    public int GameMode=0;
    // Start is called before the first frame update
    private void Start() {
        print("gamemode "+GameMode);
        ChangeMode();
    }
    void ChangeMode(){
        float expand = 1f;
        if(GameMode == 1){
            expand = 1.5f;
        }else if (GameMode == 2){
            expand = 3f;
        }
        if (GameMode != 0)
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
