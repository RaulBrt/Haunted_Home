using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCoff : MonoBehaviour{
    Rigidbody2D dust_rig;
    [SerializeField] float speed;
    public Animator dust_anim;
    Player dust_play;
    Vacuum dust_vac;
    private DustCoff[] _coff;
    Collider2D vac_collider;
    Vector2 dust_startPos, dust_endPos, dust_dir;
    bool moving, og;
    void Awake(){
        _coff = FindObjectsOfType<DustCoff>();
        if (PlayerStats.getDefeated(0))
        {
            gameObject.SetActive(false);
        }
        else{
            if(_coff.Length <= 1){
                og = true;
                dust_startPos = new Vector2(-100000, -10000000);
            }
            else
            {
                og = false;
            }
        }
    }
    void Start(){
        if (!og)
        {
            dust_anim = GetComponent<Animator>();
            dust_rig = GetComponent<Rigidbody2D>();
            dust_play = FindObjectOfType<Player>();
            dust_vac = FindObjectOfType<Vacuum>();
            dust_startPos = dust_vac.GetComponent<Rigidbody2D>().position;
            dust_endPos = dust_play.GetComponent<Rigidbody2D>().position;
            dust_dir = dust_endPos - dust_startPos;
            dust_dir.Normalize();
            dust_rig.position = dust_startPos + (dust_dir);
            moving = true;
        }
    }

    // Update is called once per frame
    void Update(){
        if (moving && !og){
            _coff = FindObjectsOfType<DustCoff>();
            dust_endPos = dust_rig.position;
            dust_endPos += dust_dir * speed;
            dust_rig.MovePosition(dust_endPos);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        dust_vac = collision.gameObject.GetComponent<Vacuum>(); 
        DustCoff dust = collision.gameObject.GetComponent<DustCoff>();
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (dust_vac != null || dust != null) { }
        else if (!og){
            if (player_coll != null && !PlayerStats.getInvincible()){
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 10);
            }
            Object.Destroy(gameObject);
        }
    }
}
