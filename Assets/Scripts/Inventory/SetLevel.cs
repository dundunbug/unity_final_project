using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLevel : MonoBehaviour
{
    public GameData.GameLevel Level;
    public GameData.GameLevel LevelPassed;
    public GameData GameData;
    public GameObject panelEnterName;
    public GameObject panelSetLevel;
    public GameObject button_EASY;
    public GameObject button_NORMAL;
    public GameObject button_HARD;
    public GameObject Panel_Story;
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
        NothingYet,
    }
    // Start is called before the first frame update
    private void Start()
    {
        GameData = GameObject.Find("GameData").GetComponent<GameData>();
        if (GameData.LevelPassed == GameData.GameLevel.NothingYet) {
            button_NORMAL.SetActive(false);
            button_HARD.SetActive(false);
        }
        else if (GameData.Level == GameData.GameLevel.Easy)
            button_HARD.SetActive(false);
    }

    public void SetLevelEasy()
    {
        Level = GameData.GameLevel.Easy;
        if(GameData.LevelPassed == GameData.GameLevel.NothingYet)
            panelEnterName.SetActive(true);
        else 
            Panel_Story.SetActive(true);

        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }

    public void SetLevelNormal()
    {
        Level = GameData.GameLevel.Normal;
   
        Panel_Story.SetActive(true);

        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }

    public void SetLevelHard()
    {
        Level = GameData.GameLevel.Hard;

        Panel_Story.SetActive(true);

        panelSetLevel.SetActive(false);

        GameData.Level = Level;
    }
   
}