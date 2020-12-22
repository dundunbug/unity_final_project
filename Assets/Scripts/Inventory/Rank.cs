using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    // Start is called before the first frame update
    public GameData gameData;
    public Text T1;
    public Text T2;
    public Text T3;

    void Start()
    {
        gameData = GameObject.Find("GameData").GetComponent<GameData>();
        T1.text = "---";
        T2.text = "---";
        T3.text = "---";
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;

        if (gameData.Rank != null)
            foreach (var player in gameData.Rank)
            {
                if (i == 0) T1.text = player.name + "  " + player.score;
                if (i == 1) T2.text = player.name + "  " + player.score;
                if (i == 2) T3.text = player.name + "  " + player.score;
                i++;
            }
    }
}
