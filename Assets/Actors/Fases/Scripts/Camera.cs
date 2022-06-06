using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour{
    Player play;
    void Start(){
        play = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update(){
        transform.position = new Vector3(0,play.transform.position.y,-10);
    }
}
