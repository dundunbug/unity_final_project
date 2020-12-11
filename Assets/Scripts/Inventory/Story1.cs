using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Story1 : MonoBehaviour
{
    public Text text1;
    public int speed;
    private string[] lines = new string[] { "怎麼了？突然什麼都看不見","7-11停電了嗎？", "不對，地面是...石頭？！", "（什麼東西、好刺眼！）",
        "（前方像是有什麼打開了。突如其來的光線讓你閉上了眼睛）","（雖然充滿著對未知的恐懼，但心裡有個聲音不斷提醒你）" ,"『逃是唯一的出路』" };//[1,1,2,2,3,3,3]
    public Sprite[] sprite = new Sprite[6];
    public Image image;
    private int i;


    private float letterPause = 0.2f;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private Text txtDisplay;
    private string words;
    //private string text = "Welcome to summerRift!!";
    void Start()
    {
        i = 0;
        image.sprite = sprite[i];
        StartCoroutine(display(lines[i]));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
            StartCoroutine(display(lines[i]));
           // display(lines[i]);

            image.sprite = sprite[i];
           // text1.text = lines[i];
        }

        if (i >= 5)
            SceneManager.LoadScene("Full_Cave");

    /*    image.sprite = sprite[i];
        text1.text = lines[i];*/

    }

    public void Skip()
    {
        SceneManager.LoadScene("Full_Cave");
    }


    public IEnumerator display(string displayStr)
    {
        words = displayStr;
        lines[i] = "";
        yield return new WaitForSeconds(2f);
        StartCoroutine(TypeText());
    }

    // 開啟打字效果
    private IEnumerator TypeText()
    {
        txtDisplay.text = "";
        foreach (var word in words)
        {

            txtDisplay.text += word;
            yield return new WaitForSeconds(letterPause);
            Debug.Log("typing"+ word);
        }

    }
}



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