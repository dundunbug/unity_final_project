using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject panel_Setting; 
   
    public void Quit_Game()
    {
        Application.Quit();
    }

    public void Open_Setting()
    {
        panel_Setting.SetActive(true);
    }

    public void Game_Save(int currentFile)
    {
        GameData gameData = GameObject.Find("GameData").GetComponent<GameData>();
        player Player = GameObject.Find("player").GetComponent<player>();
        gameData.inventory = Player.inventory;
        GameObject.Find("GameData").GetComponent<GameData>().SaveGame(currentFile);
    }

    
}

