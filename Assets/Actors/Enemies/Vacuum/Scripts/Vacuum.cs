using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour{
    Rigidbody2D vac_rig;
    PolygonCollider2D col;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();
    Vector2 vac_pos, playerPos, rush_dir;
    Player vac_play;
    public GameObject Dust;
    public Animator vac_anim;
    [SerializeField] float vac_speed;
    int action,health;
    bool coffing, rushing;
   // public double angle;
    IEnumerator delay(float seconds){
        yield return new WaitForSeconds(seconds);
        coffing = false;
    }
    IEnumerator Rush(float tempo){
        Debug.Log("Rush");;
        rushing = true;
        vac_rig.velocity = Vector2.zero;
        vac_rig.angularVelocity = 0;
        rush_dir = getDir();
        yield return new WaitForSeconds(tempo);
        rushing = false;
    }
    void Walk(float spd){;
        vac_rig.AddForce(getDir() * spd);
        //vac_pos += vac_dir * vac_speed;
        //vac_rig.MovePosition(vac_pos);
    }
    void Coff(){
        Debug.Log("Coff");
        coffing = true;
        //vac_rig.velocity = Vector3.zero;
        Instantiate(Dust, vac_rig.position, Quaternion.identity); ;
        StartCoroutine(delay(0.5f));
    }
    float getWalkingAngle(Vector2 direcao){
        float angulo;
        angulo = Mathf.Atan(direcao.y / direcao.x);
        angulo *= 180 / Mathf.PI;
        if(direcao.x < 0){
            angulo += 180;
        }
        else if(direcao.x > 0 && direcao.y < 0){
            angulo += 360;
        }
        return angulo;
    }
    bool checkDamage(){
        bool damaged = false;
        if(vac_play.getAttackDir() == 1 && getWalkingAngle(getDir()) > 247 && getWalkingAngle(getDir()) <= 292){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 2 && getWalkingAngle(getDir()) > 202 && getWalkingAngle(getDir()) <= 247){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 3 && getWalkingAngle(getDir()) > 157 && getWalkingAngle(getDir()) <= 202){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 4 && getWalkingAngle(getDir()) > 112 && getWalkingAngle(getDir()) <= 157){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 5 && getWalkingAngle(getDir()) > 68 && getWalkingAngle(getDir()) <= 112){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 6 && getWalkingAngle(getDir()) > 23 && getWalkingAngle(getDir()) <= 68){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 7 && (getWalkingAngle(getDir()) > 337 && getWalkingAngle(getDir()) <= 360) || (getWalkingAngle(getDir()) > 0 && getWalkingAngle(getDir()) <= 23)){
            damaged = true;
        }
        if (vac_play.getAttackDir() == 8 && getWalkingAngle(getDir()) > 292 && getWalkingAngle(getDir()) <= 337){
            damaged = true;
        }
        return damaged;
    }
    Vector2 getDir(){
        Vector2 dir;
        vac_pos = vac_rig.position;
        playerPos = vac_play.GetComponent<Rigidbody2D>().position;
        dir = playerPos - vac_pos;
        dir.Normalize();
        return dir;
    }
    private void OnCollisionEnter2D(Collision2D collision){
        vac_rig.velocity = Vector2.zero;
        vac_rig.mass = 999999;
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if(player_coll != null){
            if (vac_play.attacking){
                if (checkDamage()){
                    health -= 5;
                }
            }
        }
    }
    void Start(){
        vac_rig = GetComponent<Rigidbody2D>();
        vac_anim = GetComponent<Animator>();
        vac_play = FindObjectOfType<Player>();
        col = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        vac_speed = 2;
        coffing = false;
        rushing = false;
        health = 100;
    }

    // Update is called once per frame
    void Update(){
        vac_rig.mass = 1;
        action = Random.Range(0, 10000);
        vac_anim.SetFloat("Walking Angle", getWalkingAngle(getDir()));
        //Debug.Log(getWalkingAngle(getDir()));
        if (action <= 9920 && !coffing && !rushing){
            Walk(vac_speed);
        }
        else if ((action > 9920 && action <= 9960 && !coffing) || rushing) {
            if (!rushing){
                StartCoroutine(Rush(1.5f));
            }
            vac_pos += rush_dir * 0.1f;
            vac_rig.MovePosition(vac_pos);
        }
        else if(action > 9960 && !rushing && Time.time < 5){
            Coff();
            coffing = false;
        }
        if(health < 0){
            Debug.Log("Morri");
        }
    }
    void LateUpdate(){
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
