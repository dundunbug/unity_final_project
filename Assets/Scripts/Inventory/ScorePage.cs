using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScorePage : MonoBehaviour
{
    public TextMeshProUGUI T1;
    public TextMeshProUGUI T2;
    public TextMeshProUGUI T3;
    public GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        T1.text = gameData.Name;
        //T2.text = gameData.Name;
        T3.text = gameData.min+" min "+gameData.PlayTime+" sec";
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
