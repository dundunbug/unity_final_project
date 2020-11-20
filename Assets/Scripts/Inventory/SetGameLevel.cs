using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameLevel : MonoBehaviour
{
    public GameLevel Level;
    public GameObject panelEnterName;
    public GameObject panelNow;
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard,
    }
    // Start is called before the first frame update
    public void SetLevelEasy()
    {
        Level = GameLevel.Easy; 
        panelEnterName.SetActive(true);
        panelNow.SetActive(false);
    }

    public void SetLevelNormal()
    {
        Level = GameLevel.Normal;
        panelEnterName.SetActive(true);
        panelNow.SetActive(false);
    }

    public void SetLevelHard()
    {
        Level = GameLevel.Hard;
        panelEnterName.SetActive(true);
        panelNow.SetActive(false);
    }
}
