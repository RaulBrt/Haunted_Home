using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {

    [SerializeField] float passo = 25f;
    [SerializeField] string levelName;
    Rigidbody2D player_rig;
    BoxCollider2D player_coll;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();
    Vector3 change;
    public Animator anim;
    bool[] key = new bool[5] { false, false, false, false, false };
    bool[] attack = new bool[4] { false, false, false, false };
    char[] weaponType;
    public bool isAttacking,attacking, walking;
    int health,i,lastPos;

    IEnumerator Attack(float tempo){
        attacking = true;
        anim.SetBool("Attack", true);
        yield return null; 
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(tempo);
        attacking = false;
    }
    IEnumerator IFrames(float tempo){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(tempo);
        spriteRenderer.color = Color.white;
        PlayerStats.setInvincible(false);
    }
    public int getAttackDir() {
        int dir = 0;
        int i;
        bool btn = false;
        for(i = 0; i < 4; i++)
        {
            if (attack[i])
            {
                btn = true;
                break;
            }
        }
        if (btn){
            if (attack[0])
              dir = 1;
            else if (attack[2])
              dir = 5;
            else if (attack[1])
                dir = 7;
            else if (attack[3])
                dir = 3;
            return dir;
        }
        else
        {
            return lastPos;
        }
        
    }
    int getDir(){
        int dir = 0;
        if (key[0])
            dir = 1;
        else if (key[2])
            dir = 5;
        else if (key[1])
            dir = 7;
        else if (key[3])
            dir = 3;
        return dir;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Flame flame_coll = collision.gameObject.GetComponent<Flame>();
        if (!PlayerStats.getInvincible() && !attacking){ 
            if (PlayerStats.getDealtDmg() && !attacking && !PlayerStats.getInvincible())
            {
                if (flame_coll == null)
                {
                    PlayerStats.setInvincible(true);
                }
                health = PlayerStats.getHealth();
                PlayerStats.setDealtDmg(false);
                if (flame_coll == null)
                {
                    StartCoroutine(IFrames(0.75f));
                }

            }
        }
        //Debug.Log(PlayerStats.getHealth());
    }
    void die(){
        Debug.Log("Dead");
        PlayerStats.setHealth(100);
        SceneManager.LoadScene(levelName);
    }

    void initiateWeaponWheel()
    {
        char[] weaponOptions = new char[5] { 'n','f','a','p','v'};
        int lastIndex = 1;
        int i;
        for ( i = 0; i < 1 ; i++){
            if (PlayerStats.getPowerup(i))
            {
                weaponType[lastIndex] = weaponOptions[i + 1];
                lastIndex++;
            }
        }
    }
    void Awake(){
        player_rig = GetComponent<Rigidbody2D>();
        player_coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = PlayerStats.getHealth();
        isAttacking = false;
        attacking = false;
        walking = false;
        PlayerStats.setInvincible(false);
        lastPos = 0;
        weaponType = new char[5] { 'n', '\0', '\0', '\0', '\0' };
        initiateWeaponWheel();
        Debug.Log(weaponType[0]);
        Debug.Log(weaponType[1]);
    }
    void Update() {
        //Debug.Log(PlayerStats.getHealth());
        /*for(i = 0; i < 5; i++){
            key[i] = false;
        }
        for (i = 0; i < 4; i++){
            attack[i] = false;
        }
        attacking = false;
        PlayerStats.setInvincible(false);
        if (Input.GetKey(KeyCode.Mouse0) && !attacking){
            attacking = true;
            PlayerStats.setInvincible(true);
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
            for (i = 0; i < 4; i++){
                if (attack[i]){
                    StartCoroutine(Attack(0.1f));
                    break;
                }
            }
        }*/
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking)
        {
            StartCoroutine(Attack(0.18f));
        }
        else if (change != Vector3.zero)
        {
            player_rig.MovePosition(transform.position+change*passo*Time.deltaTime);
            anim.SetFloat("movimentoX", change.x);
            anim.SetFloat("movimentoY", change.y);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        anim.SetInteger("AtkDir", getAttackDir());
        if (walking)
        {
            anim.SetInteger("Dir", getDir());
        }
        else
        {
            anim.SetInteger("Dir", 0);
        }
        anim.SetInteger("LastPos", lastPos);
        anim.SetBool("Attack", attacking);
        if (PlayerStats.getHealth() <= 0){
            //die();
        }
    }
}
