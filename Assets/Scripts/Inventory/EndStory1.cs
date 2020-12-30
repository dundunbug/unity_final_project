using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndStory1 : MonoBehaviour
{
    public int speed;
    private string[] lines = new string[] { "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "而且這該死的手環跟頭盔肯定有問題，但竟然拔不掉？！","（不管你怎麼嘗試，都無法將手環脫下。而就在此時．．．）", "（手環上的珠子突然發出了強光，讓你不得不閉上眼睛）",
        "（當你再張開眼睛，一股違和感襲來，你好像忘了什麼，但有個聲音．．．）" ,"怎麼了？突然什麼都看不見","7-11停電了嗎？" ,"．．．" ,"『逃是唯一的出路』"};//[1,1,2,2,3,3,4,,4,4,4,4]
    public Sprite[] sprite = new Sprite[12];
    public Image image;
    private int i = -1;


    private float letterPause = 0.2f;
    public AudioClip typeSound;
    public AudioSource source;
    /*[SerializeField] private */
    public Text txtDisplay;
    private string words;
    int num = 0;
    public Text text1;
    private bool Done;

    void Start()
    {
        Done = false;
        i = 0;
        image.sprite = sprite[i];
        StartCoroutine(display(lines[i]));

    }

    private void Update()
    {
        if(i<=3) text1.color = Color.white;
        else if (i <= 5) text1.color = Color.black;
        else text1.color = Color.white;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.Stop();
            StopAllCoroutines();
            txtDisplay.text = lines[i];
            words = "";

            if (Done)
            {
                i++;
                Done = false;
                StartCoroutine(display(lines[i]));
                image.sprite = sprite[i];
            }
            else
                Done = true;
        }

        if (i >= 10)
            SceneManager.LoadScene("MainMenu");
    }

    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public IEnumerator display(string displayStr)
    {
        Debug.Log("startTyping:"+ displayStr);
        words = displayStr;
        yield return new WaitForSeconds(1f);
        txtDisplay.text = lines[i];
        StartCoroutine(TypeText());
    }

    // 開啟打字效果
    private IEnumerator TypeText()
    {
        num++;
        txtDisplay.text = "";
        source.Play();
        foreach (var word in words)
        {
            txtDisplay.text += word;
            yield return new WaitForSeconds(letterPause);
        }
        source.Stop();
        Done = true;
    }
}
