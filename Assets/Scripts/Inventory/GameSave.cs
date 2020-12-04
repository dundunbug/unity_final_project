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
    //public Owned_Item items;
    public Inventory inventory;
    public int[] items = new int[11];
}

[System.Serializable]
public class UsedSave
{
    public bool[] usedSave;
}
/*
[System.Serializable]
public class Owned_Item
{
    public int Bomb_L;
    public int Bomb_S;
    public int Bomb_Timer;
    public int Teddy;
    public int TransferGate;
    public int Lego;
    public int CardBoard;
    public int Bottle;
    public int Carton;
    public int Pillow;
    public int DroppedItem;
}*/