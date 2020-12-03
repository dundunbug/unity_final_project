using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    private GameData gameData;
    public GameObject PanelDel;

    void Start()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOpNum(int n)
    {
        gameData.targetNum = n;
        PanelDel.SetActive(true);
    }

    public void LoadGame()
    {
        gameData.LoadGame();
    }

    public void DeleteGame()
    {
        gameData.DeleteGameSave();
    }
}
