using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public GameLevel Level;
    public Inventory inventory;
    public player player;
    public string Name;
    public GameSave gameSave;
    public int strength;
    public int speed;
    public int vitality;
    //public
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
    }
    // Start is called before the first frame update
    private void Start()
    {
        LoadGame();
        DontDestroyOnLoad(this);
    }
    
    public void LoadGame()
    {
        string str = PlayerPrefs.GetString("PlayerData");
        if(str!=null && str.Length > 0)
        {
            GameSave g = JsonUtility.FromJson<GameSave>(str);
            if (g != null)
            {
                Debug.Log("DataLoaded");
                Debug.Log("GameLevel:" + g.Level);
                Debug.Log("UserName:" + g.PlayerName);
                Debug.Log("Strength:" + g.strength);
                Debug.Log("Speed:" + g.speed);
                Debug.Log("vitality:" + g.vitality);
            }
        }
    }

    public void SaveGame()
    {
        /*GetData*/
        /*strength = GameObject.Find("enrergyBar").GetComponent<EnergyBar>().energy;
        speed = GameObject.Find("enrergyBar(1)").GetComponent<EnergyBar>().energy;
        vitality = GameObject.Find("enrergyBar(2)").GetComponent<EnergyBar>().energy;
        inventory = GameObject.Find("Panel_UIinventory").GetComponent<UI_Inventory>().inventory;*/

        /*assign*/
        gameSave = new GameSave();
        // gameSave.checkpoint = 
        gameSave.Level = Level;
        //gameSave.inventory = inventory;
        gameSave.strength = strength;
        gameSave.speed = speed;
        gameSave.vitality = vitality;
        //gameSave.playTime = 
        gameSave.PlayerName = Name;
        //gameSave.rank = 

        /*Json*/
        string json = JsonUtility.ToJson(gameSave);
        PlayerPrefs.SetString("PlayerData", json);
        Debug.Log(json);
    }
   
}

