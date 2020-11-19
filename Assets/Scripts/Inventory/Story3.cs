using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*public class Story3 : MonoBehaviour
{
    public Text text1;
    public int speed=1;
    private string[] lines = new string[] { "真的逃出來了吧?", "到底我為什麼在這裡？", "到底該，", "怎麼辦！",
    "為什麼一直回起點阿啊啊！", "你決定再逃最後一次，這一次，要有什麼不一樣。"}; 
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
}
*/

public class Story3 : MonoBehaviour
{
    public Text text1;
    public int speed;
    private string[] lines = new string[] { "真的逃出來了吧?", "到底我為什麼在這裡？", "到底該，", "怎麼辦！",
    "為什麼一直回起點阿啊啊！", "你決定再逃最後一次，這一次，要有什麼不一樣。"};

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
            SceneManager.LoadScene("SampleScene");

        image.sprite = sprite[i];
        text1.text = lines[i];

    }

    public void Skip()
    {
        SceneManager.LoadScene("Cave");
    }
}
