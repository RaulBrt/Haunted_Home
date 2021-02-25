using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour{
    Rigidbody2D rig;
    Vector2 pos, playerPos, dir;
    Player play;
    [SerializeField] float Speed;
    void Start(){
        rig = GetComponent<Rigidbody2D>();
        play = FindObjectOfType<Player>();
        Speed = 0.03f;
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update(){
        pos = rig.position;
        playerPos = play.GetComponent<Rigidbody2D>().position;
        dir = playerPos - pos;
        dir.Normalize();
        pos += dir * Speed;
        rig.MovePosition(pos);
    }
}
