using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel_Setting; 
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Open_Setting()
    {
        panel_Setting.SetActive(true);
    }

    public void Game_Save()
    {
        /*public GameData gameData = */
        GameObject.Find("GameData").GetComponent<GameData>().SaveGame();
        // gameData.SaveGame();
    }
}

