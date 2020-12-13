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
         gameData = GameObject.Find("GameData").GetComponent<GameData>();
        Debug.Log("passedLel=" + gameData.LevelPassed);
       /* if(gameData.LevelPassed== GameData.GameLevel.Easy)
        {
            panel1.SetActive(true);
           // panel2.SetActive(false);
           // panel3.SetActive(false);
        }
        if (gameData.LevelPassed == GameData.GameLevel.Normal)
        {
            panel2.SetActive(true);
            //panel1.SetActive(false);
            //panel3.SetActive(false);
        }
        if (gameData.LevelPassed == GameData.GameLevel.Hard)
        {
            panel3.SetActive(true);
            //panel1.SetActive(false);
           // panel2.SetActive(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !start)
        {
            if (gameData.LevelPassed == GameData.GameLevel.Easy)
            {
                panel1.SetActive(true);
                // panel2.SetActive(false);
                // panel3.SetActive(false);
            }
            if (gameData.LevelPassed == GameData.GameLevel.Normal)
            {
                panel2.SetActive(true);
                //panel1.SetActive(false);
                //panel3.SetActive(false);
            }
            if (gameData.LevelPassed == GameData.GameLevel.Hard)
            {
                panel3.SetActive(true);
                //panel1.SetActive(false);
                // panel2.SetActive(false);
            }
            start = true;
        }
    }
}
