using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    // Start is called before the first frame update
  
    private int energy = 1;
    public int total = 7;


    // Update is called once per frame
    public void RefreshBar()
    {
          
         for(int i = 1; i <= energy; i++)
         {
              GameObject ob = this.gameObject.transform.GetChild(i).gameObject;
            ob.SetActive(true);
             
         }
        
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
}
