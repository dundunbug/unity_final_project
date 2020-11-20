using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterName : MonoBehaviour
{
    public Text NameDisplay;
    public GameObject panelStory;
    public GameObject buttonStart;
    public GameObject panelEnterName;

    // Start is called before the first frame update
    void Start()
    {
        //panelStory.SetActive(false);
        //buttonStart.SetActive(false);
    }

    // Update is called once per frame
   public void DisplayName(Text name)
    {
        buttonStart.SetActive(true);
        NameDisplay.text = "Welcome, " + name.text + ", We shall start our journey";
    }

   public void StartStory()
    {
        if (NameDisplay.text != "") { 
            panelStory.SetActive(true);
            panelEnterName.SetActive(false);
        }
    }
}
