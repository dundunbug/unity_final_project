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
    private string[] lines = new string[] {  "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "而且這該死的手環跟頭盔肯定有問題，但竟然拔不掉？！", "（你看向手環的珠子，突然隱隱覺得有什麼不對，所以在珠子發出強光的前一刻撇開了頭）","（你看見了，在你身後的那些不該存在的）",
        "（你想要大喊出來，但沒有辦法，強光依舊奪走了你所有感知）","．．．" ,"『逃是唯一的出路』" };//[1,1,2,2,3,4,4,4]

    private string[] next_scene_lines = new string[] {  "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "不對！（你看向身後，突然隱隱覺得有什麼不對，所以急忙退開了所在的通道）", "（在你落下後，通道裡發出了強光，手環也亮了起來）","（剛剛才倒下的怪物又起來了，它跟手環一起劇烈顫抖著）",
        "（像是程式發生了錯誤一般，怪物的樣子極不自然，接著出乎了你的意料）","（怪物向上噴出了強力的火焰，整個洞穴都跟著搖動了起來）","（你狼狽地左右奔走著躲避落石，直到你被另一件更不自然的事情吸引住）" ,
        "（你發現被怪物打出的洞外，不是洞窟）" ,"（然後，整個頂部，像是拉門似地緩緩地）","（緩緩地）","（滑開了）" ,"『實驗體１０７４，第一階段成功。』" };//[1,1,1,2,2,2,3,3,3,4,5,6,7]

    public Sprite[] sprite = new Sprite[6];
    public Image image;
    private int i;

    private float letterPause = 0.2f;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private Text txtDisplay;
    private string words;

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
            SceneManager.LoadScene("Full_Cave");

        image.sprite = sprite[i];
        text1.text = lines[i];

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
            Debug.Log("typing" + word);
        }

    }
}
