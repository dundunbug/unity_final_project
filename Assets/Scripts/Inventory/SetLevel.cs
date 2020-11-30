using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevel : MonoBehaviour
{
    public GameData.GameLevel Level;
    public GameData GameData;
    public GameObject panelEnterName;
    public GameObject panelSetLevel;
    public GameObject button_EASY;
    public GameObject button_NORMAL;
    public GameObject button_HARD;
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
    }
    // Start is called before the first frame update
    private void Start()
    {
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        if (GameData.Level == GameData.GameLevel.Easy) {
            button_NORMAL.SetActive(false);
            button_HARD.SetActive(false);
        }
        else if (GameData.Level == GameData.GameLevel.Normal)
            button_HARD.SetActive(false);
    }

    public void SetLevelEasy()
    {
        Level = GameData.GameLevel.Easy;
        panelEnterName.SetActive(true);
        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }

    public void SetLevelNormal()
    {
        Level = GameData.GameLevel.Normal;
        panelEnterName.SetActive(true);
        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }

    public void SetLevelHard()
    {
        Level = GameData.GameLevel.Hard;
        panelEnterName.SetActive(true);
        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }
   
}