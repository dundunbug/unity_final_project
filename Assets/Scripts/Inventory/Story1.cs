using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Story1 : MonoBehaviour
{
    public Text text1;
    public int speed;
    private string[] lines = new string[] { "便利商店停電??", "怎麼…這是?!", "我在哪", "你來到這裡，卻還不知道，前方，要面對的是什麼", "逃是唯一的出路" };
    public Sprite[] sprite = new Sprite[6];
    public Image image;
    private int i;

    /*timer版本*/
    /* void Start()
     {
         i = 0;
         InvokeRepeating("timer", 1, speed);


     }

     private void Update()
     {
         if (Input.GetKeyDown(KeyCode.Space))
         {

         }
     }

     void timer()
     {
         text1.text = lines[i];
         i++;
         if (i >= 5)
         {
             i = 0;
             CancelInvoke("timer");
             SceneManager.LoadScene("SampleScene");

         }

     }*/

    void Start()
    {
        i = 0;
        image.sprite = sprite[i];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
        }

        if (i >= 5)
            SceneManager.LoadScene("Cave");

        image.sprite = sprite[i];
        text1.text = lines[i];

    }

    public void Skip()
    {
        SceneManager.LoadScene("Cave");
    } 
}
