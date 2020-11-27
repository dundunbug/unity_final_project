using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryStat : MonoBehaviour
{
    public GameObject inventoryCanvas;
    public GameObject Panel_droppedItem;
    public GameObject Panel_Setting;
    // Start is called before the first frame update
    void Start()
    {
        inventoryCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (inventoryCanvas.activeSelf == false)
                inventoryCanvas.SetActive(true);
            else { 
                inventoryCanvas.SetActive(false);
                Panel_droppedItem.SetActive(false);
                Panel_Setting.SetActive(false);
            }
                
        }

        if (inventoryCanvas.activeSelf == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
