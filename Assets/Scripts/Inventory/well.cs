using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class well : MonoBehaviour
{
    public GameObject gameData;

    /* private void Awake()
     {
         if (gameData == null)
         {
             gameData = this;
             GameObject.DontDestroyOnLoad(gameObject);
         }
         else if (gameData != this)
         {
             Destroy(gameObject);
         }
     }*/

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Global").Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
