using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {

    [SerializeField] float passo = 10f;
    [SerializeField] string levelName;
    Rigidbody2D player_rig;
    PolygonCollider2D player_coll;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();
    Vector2 pos;
    public Animator anim;
    bool[] key = new bool[5] { false, false, false, false, false };
    bool[] attack = new bool[4] { false, false, false, false };
    public bool attacking;
    int health,i;

    IEnumerator Attack(float tempo){
        yield return new WaitForSeconds(tempo);
        attacking = false;
        PlayerStats.setInvincible(false);
    }
    IEnumerator IFrames(float tempo){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(tempo);
        spriteRenderer.color = Color.white;
        PlayerStats.setInvincible(false);
    }
    public int getAttackDir() {
        int dir = 0;
        if (attack[0]){
            if (attack[1])
                dir = 8;
            else if (attack[3])
                dir = 2;
            else
                dir = 1;
        }
        else if (attack[2]){
            if (attack[1])
                dir = 6;
            else if (attack[3])
                dir = 4;
            else
                dir = 5;
        }
        else if (attack[1])
            dir = 7;
        else if (attack[3])
            dir = 3;
        return dir;
    }
    int getDir(){
        int dir = 0;
        if (key[0]){
            if (key[1])
                dir = 8;
            else if (key[3])
                dir = 2;
            else
                dir = 1;
        }
        else if (key[2]){
            if (key[1])
                dir = 6;
            else if (key[3])
                dir = 4;
            else
                dir = 5;
        }
        else if (key[1])
            dir = 7;
        else if (key[3])
            dir = 3;
        return dir;
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (PlayerStats.getDealtDmg() && !attacking && !PlayerStats.getInvincible()) {
            PlayerStats.setInvincible(true);
            health = PlayerStats.getHealth();
            PlayerStats.setDealtDmg(false);
            StartCoroutine(IFrames(0.75f));
        }
        Debug.Log(PlayerStats.getHealth());
    }
    void die(){
        Debug.Log("Dead");
        PlayerStats.setHealth(100);
        SceneManager.LoadScene(levelName);
    }
    void Awake(){
        player_rig = GetComponent<Rigidbody2D>();
        player_coll = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = PlayerStats.getHealth();
        attacking = false;
        PlayerStats.setInvincible(false);

    }
    void Update() {
        pos.x = player_rig.position.x;
        pos.y = player_rig.position.y;
        for(i = 0; i < 5; i++){
            key[i] = false;
        }
        for (i = 0; i < 4; i++){
            attack[i] = false;
        }
        attacking = false;
        if (Input.GetKey(KeyCode.Mouse0) && !attacking){
            attacking = true;
            if (Input.GetKey(KeyCode.W)){
                attack[0] = true;
            }
            else if (Input.GetKey(KeyCode.S)){
                attack[2] = true;
            }
            if (Input.GetKey(KeyCode.A)){
                attack[1] = true;
            }
            else if (Input.GetKey(KeyCode.D)){
                attack[3] = true;
            }
            for (i = 4; i < 4; i++){
                if (attack[i]){
                    StartCoroutine(Attack(2));
                    break;
                }
            }
        }
        else if (!attacking){
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
        }
        player_rig.MovePosition(pos);
        anim.SetBool("Up", key[0]);
        anim.SetBool("Down", key[2]);
        anim.SetBool("Left", key[1]);
        anim.SetBool("Right", key[3]);
        anim.SetBool("AUp", attack[0]); 
        anim.SetBool("ADown", attack[2]);
        anim.SetBool("ALeft", attack[1]);
        anim.SetBool("ARight", attack[3]);
        if (PlayerStats.getHealth() <= 0){
            die();
        }
    }
    void LateUpdate(){
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        player_coll.SetPath(0, physicsShape);
    }
}
