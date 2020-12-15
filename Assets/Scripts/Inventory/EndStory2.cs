using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndStory2 : MonoBehaviour
{
    public Text text1;
    public int speed;
    private string[] lines = new string[] {  "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "而且這該死的手環跟頭盔肯定有問題，但竟然拔不掉？！", "（你看向手環的珠子，突然隱隱覺得有什麼不對，所以在珠子發出強光的前一刻撇開了頭）","（你看見了，在你身後的那些不該存在的）",
        "（你想要大喊出來，但沒有辦法，強光依舊奪走了你所有感知）","．．．" ,"『逃是唯一的出路』" };//[1,1,2,2,3,4,4,4]


    public Sprite[] sprite = new Sprite[8];
    public Image image;
    private int i;

    private float letterPause = 0.2f;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private Text txtDisplay;
    private string words;
    public AudioSource source;

    void Start()
    {
        i = 0;
        image.sprite = sprite[i];
        StartCoroutine(display(lines[i]));
    }

    private void Update()
    {
        if (i <= 4) text1.color = Color.white;
        else text1.color = Color.black;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
            if (i >= 7)
                SceneManager.LoadScene("MainMenu");

            source.Stop();
            StopAllCoroutines();
            txtDisplay.text = "";
            words = "";
            StartCoroutine(display(lines[i]));
            image.sprite = sprite[i];
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
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
        source.Play();
        foreach (var word in words)
        {
            txtDisplay.text += word;
            yield return new WaitForSeconds(letterPause);
        }
        source.Stop();
    }
}