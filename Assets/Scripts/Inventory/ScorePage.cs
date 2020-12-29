using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class ScorePage : MonoBehaviour
{
    /* public TextMeshProUGUI T1;
     public TextMeshProUGUI T2;
     public TextMeshProUGUI T3;
     public TextMeshProUGUI T4;*/
    public Text T1;
    public Text T2;
    public Text T3;
    public Text T4;
    public GameData gameData;
    public player player;
    public int Round = 0;
    int del_key;
    // Start is called before the first frame update
    void Start()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        player = GameObject.Find("player").GetComponent<player>();
        switch (gameData.Level)
        {
            case GameData.GameLevel.Easy: Round = 0; break;
            case GameData.GameLevel.Normal: Round = 1; break;
            case GameData.GameLevel.Hard: Round = 2; break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        T1.text = gameData.Name;
        T2.text = player.DefeatedNum + "";
        T3.text = gameData.min + " min " + gameData.PlayTime + " sec";
        //Debug.Log("gameData.Score[Round]=" + gameData.Score[Round]);
        if (gameData.PlayTime < 20)
            gameData.Score[Round] = player.DefeatedNum * 100 + 300;
        else
            gameData.Score[Round] = player.DefeatedNum * 100;

        T4.text = gameData.Score[Round] + "";
    }

    public void Continue()
    {
        if (gameData.LevelPassed != gameData.Level)
            gameData.totalScore += gameData.Score[Round];
        Debug.Log("gameData.usedSave.Rank=" + gameData.Rank != null);
        gameData.Rank.Add(new Member { name = gameData.Name, score = gameData.totalScore });
        // if (gameData.usedSave.Rank.Any()) 
        //   Debug.Log("gameData.usedSave.Rank has sth");
        int i = 0;
        if (gameData.Rank.Any())
        {
            foreach (var player in gameData.Rank)
            {
                Debug.Log("gameData.Rank[]=" + player.name);
                i++;
            }
            if (i > 3)
            {
                gameData.Rank.RemoveAt(3);
            }
            gameData.Rank.Sort();
        }

        gameData.LevelPassed = gameData.Level;

        /*save record*/
        if (gameData.Rank != null && gameData.Rank.Any())
        {
            gameData.Rank.Sort();
            int j = 0;
            if (j < 3)
                foreach (var player in gameData.Rank)
                {
                    gameData.usedSave.Rank_name[j] = player.name;
                    gameData.usedSave.Rank_score[j] = player.score;
                    Debug.Log("SaveRank:" + player.name);
                    j++;
                }
        }

        string jsonUsedSave = JsonUtility.ToJson(gameData.usedSave);
        PlayerPrefs.SetString("usedSave", jsonUsedSave);
        /*done saving*/

        SceneManager.LoadScene("EndStories");
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    public void Lose()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
