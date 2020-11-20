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
    public string UserName;

    // Start is called before the first frame update
    void Start()
    {
        //panelStory.SetActive(false);
        //buttonStart.SetActive(false);
    }

    // Update is called once per frame
   public void DisplayName(Text name)
    {
        /*Display*/
        buttonStart.SetActive(true);
        NameDisplay.text = "Welcome, " + name.text + ", We shall start our journey";

        /*SaveName*/
        GameObject.Find("GameData").GetComponent<GameData>().Name = name.text;
    }

   public void StartStory()
    {
        if (NameDisplay.text != "") { 
            panelStory.SetActive(true);
            panelEnterName.SetActive(false);
        }
    }
}
