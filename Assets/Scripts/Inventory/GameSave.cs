using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSave
{
    public int checkpoint;
    public GameData.GameLevel Level;
    //public Inventory inventory;
    public int strength;
    public int speed;
    public int vitality;
    public float playTime;
    public string PlayerName;
    public int rank;
}