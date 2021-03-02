using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour{

    [SerializeField] float passo = 10f;
    Rigidbody2D player_rig;
    public Animator anim;
    bool[] key = new bool[4] { false, false, false, false };

    void OnCollisionEnter2D(Collision2D collision){
        Vacuum vac = collision.gameObject.GetComponent<Vacuum>();
        DustCoff dust = collision.gameObject.GetComponent<DustCoff>();
        if (vac != null){
            Debug.Log("Hit Vaccum");
        }
        if (dust != null){
            Debug.Log("Hit Dust");
        }
    }
    void Awake(){
        player_rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update() {
        Vector2 pos;
        pos.x = player_rig.position.x;
        pos.y = player_rig.position.y;
        for(int i = 0; i < 4; i++){
            key[i] = false;
        }
        if (Input.GetKey(KeyCode.W)){
            pos.y += passo * Time.deltaTime;
            key[0] = true;
        }
        else if (Input.GetKey(KeyCode.S)){
            pos.y -= passo * Time.deltaTime;
            key[2] = true;
        }
        if (Input.GetKey(KeyCode.A)){
            pos.x -= passo * Time.deltaTime;
            key[1] = true;
        }
        else if (Input.GetKey(KeyCode.D)){
            pos.x += passo * Time.deltaTime;
            key[3] = true;
        }
        player_rig.MovePosition(pos);
        anim.SetBool("Up", key[0]);
        anim.SetBool("Down", key[2]);
        anim.SetBool("Left", key[1]);
        anim.SetBool("Right", key[3]);
    }
}
