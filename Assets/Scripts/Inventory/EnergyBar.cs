using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    // Start is called before the first frame update
  
    private int energy = 1;
    public int total = 7;
    public int Num = 10;
    private Upagradenum upagradenum;


    private void Start()
    {
        upagradenum = GameObject.Find("remainNum").GetComponent<Upagradenum>(); ;
    }

    // Update is called once per frame
    public void RefreshBar()
    {
        if (upagradenum.num <= 0) return;

        GameObject ob = this.gameObject.transform.GetChild(energy).gameObject;
        ob.SetActive(true);
    }
    
    

    public void SetBar_ADD()
    {
        if (energy < total)
        {
            energy++;
            
        }
            
        Debug.Log("energy="+energy);
        RefreshBar();
    }

    public float GetEergyStat()
    {
        float result = (float)energy / total;
        return result;
    }
    public void changeNum()
    {
        if (upagradenum.num > 0 && energy<total)
            upagradenum.DecreaseNum();
    }

}
