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
    public TextMeshProUGUI T4;
    public GameData gameData;
    public player player;
    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        player = GameObject.Find("player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        T1.text = gameData.Name;
        T2.text = player.DefeatedNum + "";
        T3.text = gameData.min+" min "+gameData.PlayTime+" sec";
        if(gameData.PlayTime < 20)
            gameData.Score = player.DefeatedNum * 100 + 300;
        else
            gameData.Score = player.DefeatedNum * 100;
        T4.text = gameData.Score + "";
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
