using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndStory3 : MonoBehaviour
{
    public Text text1;
    public int speed;
  
    private string[] lines = new string[] {  "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "不對！（你看向身後，突然隱隱覺得有什麼不對，所以急忙退開了所在的通道）", "（在你落下後，通道裡發出了強光，手環也亮了起來）","（剛剛才倒下的怪物又起來了，它跟手環一起劇烈顫抖著）",
        "（像是程式發生了錯誤一般，怪物的樣子極不自然，接著出乎了你的意料）","（怪物向上噴出了強力的火焰，整個洞穴都跟著搖動了起來）","（你狼狽地左右奔走著躲避落石，直到你被另一件更不自然的事情吸引住）" ,
        "（你發現被怪物打出的洞外，不是洞窟）" ,"（然後，整個頂部，像是拉門似地緩緩地）","（緩緩地）","（滑開了）" ,"『實驗體１０７４，第一階段成功。』" };//[1,1,1,2,2,2,3,3,3,4,5,6,7]

    public Sprite[] sprite = new Sprite[13];
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            i++;
            if (i >= 12)
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
            Debug.Log("typing" + word);
        }
        source.Stop();
    }
}