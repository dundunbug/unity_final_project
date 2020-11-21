﻿using System.Collections;
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
    public UsedSave usedSave;
    public int strength;
    public int speed;
    public int vitality;
    public string[] SaveFileName;
    //[SerializeField] public bool[] usedSave;
    public int FileLimit = 3;
    public int FileNum;
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
        SaveFileName = new string[5] { "Save1", "Save2", "Save3", "Save4", "Save5" };
        /*處理現在是save幾，從來沒存過就新增一個usedSave*/
        string str = PlayerPrefs.GetString("usedSave");
        if (str != null && str.Length > 0)
        {
            usedSave = JsonUtility.FromJson<UsedSave>(str);
            Debug.Log("Not A New Game");
        }
        else
        {
            usedSave = new UsedSave();
            usedSave.usedSave = new bool[5];
            FileNum = 0;
            Debug.Log("Create NewGame");

            string jsonUsedSave = JsonUtility.ToJson(usedSave);
            PlayerPrefs.SetString("usedSave", jsonUsedSave);
        }
        
            DontDestroyOnLoad(this);
    }
    
    public void LoadGame(int n)
    {
        string str = PlayerPrefs.GetString(SaveFileName[n]);
        if (str != null && str.Length > 0)
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
            SceneManager.LoadScene("Cave");
        }
        else
        {
            Debug.Log("Nothing to Load");
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


        /*Save UsedSaveFile*/
        for(int i = 0; i < FileLimit; i++) 
        {
            if (!usedSave.usedSave[i])
            {
                usedSave.usedSave[i] = true;
                FileNum = i;
                Debug.Log("SaveFile[" + i + "]");
                break;
            }
        }
        
        /*Json*/
        string json = JsonUtility.ToJson(gameSave);
        
        PlayerPrefs.SetString(SaveFileName[FileNum], json);
        

        string jsonUsedSave = JsonUtility.ToJson(usedSave);
        PlayerPrefs.SetString("usedSave", jsonUsedSave);
    }
    
    public void DeleteGameSave(int n)
    {
        Debug.Log("DeleteFile:" + SaveFileName[n]);
        usedSave.usedSave[n] = false;
        PlayerPrefs.DeleteKey(SaveFileName[n]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteGameSave(0);

            Debug.Log("DeleteGameSave");
        }

        /*還原成沒開過的遊戲*/
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("DeleteUsedSave");
            PlayerPrefs.DeleteKey("usedSave");
        }

    }
}

