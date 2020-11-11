﻿/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Story2 : MonoBehaviour
{
    public Text text1;
    public int speed=1;
    private string[] lines = new string[] { "到這裡了，", "哪裡才是出去的路?", "手環拔不掉?!", "怎麼又回到這?這不是．．．？！", 
    "重新的循環，但你還是只能逃，活下去是唯一能做的事"};
    private int i;   



    void Start()
    { 
        i = 0;
        InvokeRepeating("timer", 1, speed);
    }


    void timer()
    {
        text1.text = lines[i];
        i++;
        if (i >= 3)
        {
            i = 0;
            CancelInvoke("timer");
            SceneManager.LoadScene("SampleScene");

        }

    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Story2 : MonoBehaviour
{
    public Text text1;
    public int speed;
    private string[] lines = new string[] { "到這裡了，", "哪裡才是出去的路?", "手環拔不掉?!", "怎麼又回到這?這不是．．．？！", "重新的循環，但你還是只能逃，活下去是唯一能做的事" };

    public Sprite[] sprite = new Sprite[6];
    public Image image;
    private int i;

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
}
