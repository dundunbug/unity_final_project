using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPillow : MonoBehaviour
{
    public GameObject feather;
    public int featherNum = 2;
    public float radius = 5;
    public float xRange = 5f;
    public float yRange = 4f;
    // Start is called before the first frame update
    
    private void Start() {
        
    }
    public void FlyFeathers(){
        for(int i =0; i<featherNum ;i++){
            GameObject obj = GameObject.Instantiate(feather, gameObject.transform.position, Quaternion.identity, gameObject.transform) as GameObject;
            Vector2 newPos = GetRandomPos();
            print(newPos);
            obj.GetComponent<objectFeather>().target = newPos;
            obj.GetComponent<objectFeather>().hasPos = true;
        }
    }

    private Vector2 GetRandomPos(){
        float x = Random.Range(transform.position.x - xRange, transform.position.x + xRange);
        float y = Random.Range(transform.position.y + 1.5f, transform.position.y + yRange);
        Vector2 newPos = new Vector2(x,y);
        // Vector2 newPos = Random.onUnitSphere*radius;
        // newPos.y = Mathf.Abs(newPos.y);
        Vector2 dis = newPos - new Vector2(transform.position.x,transform.position.y);
        if (dis.magnitude <= 0.3f){
            GetRandomPos();
        }else{
            return newPos;
        }
        return newPos;
    }
}
