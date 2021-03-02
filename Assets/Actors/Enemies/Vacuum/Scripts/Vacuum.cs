using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour{
    Rigidbody2D vac_rig;
    Vector2 vac_pos, playerPos;
    Player vac_play;
    public GameObject Dust;
    public Animator vac_anim;
    [SerializeField] float vac_speed;
    int action;
    bool coffing, rushing;
    public double angle;
    IEnumerator delay(float seconds){
        yield return new WaitForSeconds(seconds);
        coffing = false;
    }
    void Walk(float spd){;
        vac_rig.AddForce(getDir() * spd);
        //vac_pos += vac_dir * vac_speed;
        //vac_rig.MovePosition(vac_pos);
    }
    void Coff(){
        Debug.Log("Coff");
        coffing = true;
        vac_rig.velocity = Vector3.zero;
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
    }
    void Start(){
        vac_rig = GetComponent<Rigidbody2D>();
        vac_anim = GetComponent<Animator>();
        vac_play = FindObjectOfType<Player>();
        vac_speed = 3000;
        coffing = false;
    }

    // Update is called once per frame
    void Update(){
        action = Random.Range(0, 10000);
        vac_anim.SetFloat("Walking Angle", getWalkingAngle(getDir()));
        //Debug.Log(getWalkingAngle(getDir()));
        if (action <= 9920 && coffing == false){
            Walk(vac_speed);
        }

        else if (action > 9920 && action <= 9960){
            Debug.Log("Rush");
            vac_rig.velocity = Vector2.zero;
            delay(0.5f);
            Walk(vac_speed * 10);
        }
        else if(action > 9960){
            Coff();
        }
    }
}
