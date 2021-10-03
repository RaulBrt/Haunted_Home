using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    [SerializeField] float passo = 25f;
    [SerializeField] string levelName;
    Rigidbody2D player_rig;
    BoxCollider2D player_coll;
    SpriteRenderer spriteRenderer;
    Vector3 change;
    public Animator anim;
    bool[] key = new bool[5] { false, false, false, false, false };
    bool[] attack = new bool[4] { false, false, false, false };
    public bool isAttacking, attacking, walking, changingWeapon;
    int i, lastPos;

    IEnumerator Attack(float tempo)
    {
        attacking = true;
        anim.SetBool("Attack", true);
        yield return null;
        anim.SetBool("Attack", false);
        yield return new WaitForSeconds(tempo);
        attacking = false;
    }
    IEnumerator IFrames(float tempo)
    {
        PlayerStats.setInvincible(true);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(tempo);
        spriteRenderer.color = Color.white;
        PlayerStats.setInvincible(false);
    }
    IEnumerator Wait(float tempo)
    {
        changingWeapon = true;
        yield return new WaitForSeconds(tempo);
        changingWeapon = false;
    }
    public int getAttackDir()
    {
        int dir = 0;
        int i;
        bool btn = false;
        for (i = 0; i < 4; i++)
        {
            if (attack[i])
            {
                btn = true;
                break;
            }
        }
        if (btn)
        {
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
    int getDir()
    {
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
        if (!PlayerStats.getInvincible() && !attacking)
        {
            if (PlayerStats.getDealtDmg() && !attacking && !PlayerStats.getInvincible())
            {
                PlayerStats.setDealtDmg(false);
                if (flame_coll == null)
                {
                    StartCoroutine(IFrames(0.75f));
                }

            }
        }
        //Debug.Log(PlayerStats.getHealth());
    }
    void die()
    {
        Debug.Log("Dead");
        PlayerStats.setHealth(100);
        SceneManager.LoadScene(levelName);
    }

    void Awake()
    {
        PlayerStats.Awake();
        player_rig = GetComponent<Rigidbody2D>();
        player_coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isAttacking = false;
        attacking = false;
        walking = false;
        PlayerStats.setInvincible(false);
        lastPos = 0;
        PlayerStats.weaponWheel(0);
        changingWeapon = false;
    }
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0) && !attacking) {
            StartCoroutine(Attack(0.18f));
        }

        if (Input.GetKeyDown(KeyCode.Q) && !changingWeapon)
        {
            PlayerStats.weaponWheel(-1);
            StartCoroutine(Wait(0.15f));
            Debug.LogWarning(PlayerStats.weaponWheel(0));
        }


        else if (Input.GetKeyDown(KeyCode.E) && !changingWeapon)
        {
            PlayerStats.weaponWheel(1);
            StartCoroutine(Wait(0.15f));
            Debug.LogWarning(PlayerStats.weaponWheel(0));
        }
        
        else if (change != Vector3.zero)
        {
            player_rig.MovePosition(transform.position + change * passo * Time.deltaTime);
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
        if (PlayerStats.getHealth() <= 0)
        {
            die();
        }
    }
}
