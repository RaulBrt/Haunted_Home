using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour {
    int action;
    public GameObject Fire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        action = Random.Range(0, 10000);
        Debug.Log(action);
        if(action >= 9600 && Time.time > 5){
            Instantiate(Fire, new Vector3(0,6.7f,0), Quaternion.identity);
        }
    }
}
