using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upagradenum : MonoBehaviour
{
    // Start is called before the first frame update
    public int num = 10;
    public Text N;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void changeNum()
    {
        if (num > 0)
        {
        num--;
        N.text = num+"";
        }
        
    }
}
