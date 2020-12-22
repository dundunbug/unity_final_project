using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.ComponentModel;
using System.Data;

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
    public int Score;
}

[System.Serializable]
public class UsedSave
{
    public bool[] usedSave;
    public string[] Rank_name;// = new string[3];
    public int[] Rank_score;// = new int[3];
}

public class Member : IComparable<Member>
{
    public int score;
    public string name;
    public int CompareTo(Member x)
    {
        if (this.score < x.score)
            return 1;
        else if (this.score > x.score)
            return -1;
        else
            return 0;
    }
}


/*
public class RankSort : IComparer<int>
{
    public int Compare(int x, int y)
    {
        if (x < y) return 1;
        else if (x == y) return 0;
        else return -1;
    }
}*/
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
