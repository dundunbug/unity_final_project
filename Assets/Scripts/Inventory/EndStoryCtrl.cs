using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndStoryCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameData gameData;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private bool start = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        Debug.Log("cool");
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        Debug.Log("passedLel=" + gameData.LevelPassed);
        if(gameData.LevelPassed== GameData.GameLevel.Easy)
        {
            
            panel1.SetActive(true);
        }
        if (gameData.LevelPassed == GameData.GameLevel.Normal)
        {
            panel2.SetActive(true);
        }
        if (gameData.LevelPassed == GameData.GameLevel.Hard)
        {
            panel3.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
