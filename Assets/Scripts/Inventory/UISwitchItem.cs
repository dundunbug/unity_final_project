using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitchItem : MonoBehaviour
{
    public GameObject ThrowItem;
    public GameObject DropItem;
    bool isOnThrowItemUI = true;
    UIchangeItem changeThrowItem,changeDropItem;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = ThrowItem.transform.position;
        changeThrowItem = ThrowItem.GetComponent<UIchangeItem>();
        changeDropItem = DropItem.GetComponent<UIchangeItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)){
            switchChangeItem();
        }

        if (Input.GetKeyUp(KeyCode.E)){
            if (isOnThrowItemUI){
                changeThrowItem.switchItem(true);
            }else{
                changeDropItem.switchItem(true);
            }
        }else if (Input.GetKeyUp(KeyCode.Q)){
            if (isOnThrowItemUI){
                changeThrowItem.switchItem(false);
            }else{
                changeDropItem.switchItem(false);
            }
        }
    }
    void switchChangeItem(){
        if (isOnThrowItemUI){
            transform.position = DropItem.transform.position;
            isOnThrowItemUI = false;
        }else{
            transform.position = ThrowItem.transform.position;
            isOnThrowItemUI = true;
        }    
    }
}
