using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class EnergyBar : MonoBehaviour
{

    // Start is called before the first frame update
  
    public int energy;
    public int total = 7;
    public int Num = 10;
    private Upagradenum upagradenum;
    public GameData gameData;

    public event EventHandler BarChanged;

    private void Awake()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();

        if (gameData.LoadedData != null)
        {
            switch (this.gameObject.name)
            {
                case "energyBar":
                    energy = gameData.LoadedData.strength;
                    break;
                case "energyBar(1)":
                    energy = gameData.LoadedData.speed;
                    break;
                case "energyBar(2)":
                        energy = gameData.LoadedData.vitality;
                    break;
            }
        }
        else
        energy = 1;
    }
    private void Start()
    {
        upagradenum = GameObject.Find("remainNum").GetComponent<Upagradenum>(); ;
    }


    // Update is called once per frame
    public void RefreshBar()
    {Debug.Log(this.gameObject.name+ energy);
        for(int i = 1; i <= energy; i++)
        {
            GameObject ob = this.gameObject.transform.GetChild(i).gameObject;
            ob.SetActive(true);
        }
    }

    public void SetBar_ADD()
    {
            /*SaveData*/
            gameData = GameObject.Find("GameData").GetComponent<GameData>();
            switch (this.gameObject.name) {
                case "energyBar":
                    {
                        gameData.strength = energy;
                    }
                    break;
                case "energyBar(1)":
                    {
                        gameData.speed = energy;
                    }
                    break;
                case "energyBar(2)":
                    {
                    gameData.vitality = energy;
                    }
                    break;
            }

        RefreshBar();    
    }

    public float GetEergyStat()
    {
        float result = (float)energy / total;
        return result;
    }
    public void changeNum()
    {
        if (upagradenum.num > 0 && energy < total)
        {
            upagradenum.DecreaseNum();
            energy++;
            SetBar_ADD();
        }
            
    }

}
