using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour{
    Rigidbody2D rig;
    [SerializeField] float speed;
    Player play;
    Collider2D coll;
    Vector2 startPos, endPos, dir;
    bool og;
    private Fire[] fire;
    void OnTriggerEnter2D(Collider2D collision){
        Fire fir = collision.gameObject.GetComponent<Fire>();
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (!og){
            if(fir != null) { }
            else{
                if (player_coll != null && !PlayerStats.getInvincible()){
                    PlayerStats.setDealtDmg(true);
                    PlayerStats.setHealth(PlayerStats.getHealth() - 10);
                }
                Object.Destroy(gameObject);
            }            
        }
    }
    void Start(){
        fire = FindObjectsOfType<Fire>();
        if(fire.Length <= 1){
            og = true;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(-10000, -10000));
        }
        else{
            og = false;
            rig = GetComponent<Rigidbody2D>();
            play = FindObjectOfType<Player>();
            startPos = rig.position;
            endPos = play.GetComponent<Rigidbody2D>().position;
            dir = endPos - startPos;
            dir.Normalize();
            if(speed == 0){
                speed = 0.08f;
            }
        }
    }
    void Update(){
        if (!og){
            endPos = rig.position;
            endPos += dir * speed;
            rig.MovePosition(endPos);
        }
    }
}
