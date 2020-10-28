using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Upagradenum : MonoBehaviour
{
    // Start is called before the first frame update
    public int num = 10;
    public Text N;

    private void Awake()
    {
        N.text = num.ToString();
    }


    public void DecreaseNum()
    {
        num--;
        N.text = num + "";
    }
}
