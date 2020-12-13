using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class TypeMachine : MonoBehaviour
{

    private float letterPause = 0.2f;
    [SerializeField] private AudioClip typeSound;
    [SerializeField] private Text txtDisplay;
    private string words;
    private string text = "Welcome to summerRift!!";


    public IEnumerator display(string displayStr)
    {
        words = displayStr;
        text = "";
        yield return new WaitForSeconds(2f);
        StartCoroutine(TypeText());
    }

    void Start()
    {
        StartCoroutine(display(text));

    }

    // 開啟打字效果
    private IEnumerator TypeText()
    {
        foreach (var word in words)
        {
            txtDisplay.text += word;
            yield return new WaitForSeconds(letterPause);
        }

    }
}