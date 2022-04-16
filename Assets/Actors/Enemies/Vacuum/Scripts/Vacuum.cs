using System;
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
    bool coffing, rushing, invincible;
    double mod;
    // public double angle;
    IEnumerator IFrames(float tempo)
    {
        invincible = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(tempo);
        invincible = false;
        spriteRenderer.color = Color.white;
    }
    IEnumerator delay(float seconds){
        yield return new WaitForSeconds(seconds);
    }
    IEnumerator Coff(float seconds){
        yield return new WaitForSeconds(seconds);
        coffing = false;
    }
    IEnumerator Rush(float tempo){
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

        coffing = true;
        //vac_rig.velocity = Vector3.zero;
        Instantiate(Dust, vac_rig.position, Quaternion.identity);
        StartCoroutine(Coff(0.5f));
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
    public int getHealth()
    {
        return health;
    }
    int getWalkingDir(float angulo){
        int dir = 0;
        if (angulo > 247 && angulo <= 292){
            dir = 5;
        }
        if (angulo > 202 && angulo <= 247){
            dir = 6;
        }
        if (angulo > 157 && angulo <= 202){
            dir = 7;
        }
        if (angulo > 112 && angulo <= 157){
            dir = 8;
        }
        if (angulo > 68 && angulo <= 112){
            dir = 1;
        }
        if (angulo > 23 && angulo <= 68){
            dir = 2;
        }
        if ((angulo > 337 && angulo <= 360) || (angulo > 0 && angulo <= 23)){
            dir = 3;
        }
        if (angulo > 292 && angulo <= 337){
            dir = 4;
        }
        return dir;
    }
    Vector2 getDir(){
        Vector2 dir;
        vac_pos = vac_rig.position;
        playerPos = vac_play.GetComponent<Rigidbody2D>().position;
        dir = playerPos - vac_pos;
        dir.Normalize();
        return dir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        vac_rig.velocity = Vector2.zero;
        vac_rig.mass = 999999;
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if(player_coll != null){
            if (vac_play.attacking || Input.GetKey(KeyCode.Mouse0))
            {
   
                if ((!PlayerStats.getInvincible() && !vac_play.attacking)) {
                    PlayerStats.setDealtDmg(true);
                    PlayerStats.setHealth(PlayerStats.getHealth() - 10);
                }
            }
            else if ((!PlayerStats.getInvincible() && !vac_play.attacking))
            {
                Debug.Log(PlayerStats.getInvincible() + "," + vac_play.attacking);
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 10);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mod = 1;
        if(PlayerStats.weaponWheel(0) == 2)
        {
            mod = 1.5f;
        }
        else if(PlayerStats.weaponWheel(0) == 1)
        {
            mod = 0.5f;
        }
        PlayerATK player_coll = collision.gameObject.GetComponent<PlayerATK>();
        if (player_coll != null && !invincible)
        {
            //Debug.Log(health);
            health -= Convert.ToInt32(10*mod);
            Walk(-100*vac_speed );
            StartCoroutine(IFrames(0.1f));
        }
    }

    void Awake(){
        if (PlayerStats.getDefeated(0) == true){
            UnityEngine.Object.Destroy(gameObject);
            health = 0;
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
        health = 300;
        mod = 1;
        StartCoroutine(delay(1.5f));
    }

    // Update is called once per frame
    void Update(){
        vac_rig.mass = 1;
        action = UnityEngine.Random.Range(0, 1000);
        vac_anim.SetInteger("Walking Angle", getWalkingDir(getWalkingAngle(getDir())));
        if (action <= 975 && !coffing && !rushing){
            Walk(vac_speed);
        }
        else if ((action > 975 && action <= 980 && !coffing) || rushing) {
            if (!rushing){
                StartCoroutine(Rush(0.5f));
            }
            vac_pos += rush_dir * 0.25f;
            vac_rig.MovePosition(vac_pos);
        }
        if(action > 980 && !rushing){
            Coff();
            coffing = false;
        }
        if(health < 0){
            PlayerStats.setDefeated(0, true);
            VacuumPUp vacuumPup = FindObjectOfType<VacuumPUp>();
            vacuumPup.GetComponent<Transform>().position = new Vector3(0, 1, 0);
            PlayerStats.setDefeated(0, true);
            //PlayerStats.saveGame();
            gameObject.SetActive(false);
        }
    }
    void LateUpdate(){
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
