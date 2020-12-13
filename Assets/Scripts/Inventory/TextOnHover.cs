using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TextOnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject T;
    void Start()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)    //滑鼠移入
    {
        T.SetActive(true);
        Debug.Log("cool");
    }
    public void OnPointerExit(PointerEventData eventData)    //滑鼠移入
    {
        T.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
