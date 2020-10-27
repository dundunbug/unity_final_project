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
    private int i;


    void Start()
    {
       
        /*lastTime = 0f;
        timepassed = 0;
        draw = false;*/
        i = 0;
        InvokeRepeating("timer", 1, speed);


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

    }
}
