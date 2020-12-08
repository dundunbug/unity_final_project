using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadedDataOb : MonoBehaviour
{
    // Start is called before the first frame update
    public GameSave LoadedData;
    public LoadedDataOb ins;
    void Awake()
    {

        if (ins == null)
        {

            ins = this;
            GameObject.DontDestroyOnLoad(gameObject);

        }
        else if (ins != this)
        {

            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        LoadedData = GameObject.Find("GameData").GetComponent<GameData>().LoadedData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
