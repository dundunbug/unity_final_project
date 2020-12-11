/*using System.Collections;
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
    private string[] lines = new string[] { "呼，到這裡應該不會再有怪物了吧。但是哪裡才是出去的路？", "（你環顧四周，卻看不見像是出口的地方）",
        "而且這該死的手環跟頭盔肯定有問題，但竟然拔不掉？！","（不管你怎麼嘗試，都無法將手環脫下。而就在此時．．．）", "（手環上的珠子突然發出了強光，讓你不得不閉上眼睛）",
        "（當你再張開眼睛，一股違和感襲來，你好像忘了什麼，但有個聲音．．．）" ,"怎麼了？突然什麼都看不見","7-11停電了嗎？" ,"．．．" ,"『逃是唯一的出路』"};//[1,1,2,2,3,3,4,,4,4,4,4]

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
            SceneManager.LoadScene("Full_Cave");

        image.sprite = sprite[i];
        text1.text = lines[i];

    }

    public void Skip()
    {
        SceneManager.LoadScene("Full_Cave");        
    }

}
