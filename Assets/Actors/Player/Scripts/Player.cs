using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    float passo = 20f;
    [SerializeField] string levelName;
    [SerializeField] bool somPasso;
    bool jaTocando;
    Rigidbody2D player_rig;
    BoxCollider2D player_coll;
    SpriteRenderer spriteRenderer;
    AudioManager audioManager;
    Vector3 change,direcao;
    public Animator anim;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PlayerStats.getInvincible() && !attacking)
        {
            if (PlayerStats.getDealtDmg() && !attacking && !PlayerStats.getInvincible())
            {
                PlayerStats.setDealtDmg(false);
                audioManager.Play("Damage");
                StartCoroutine(IFrames(0.5f));
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerStats.getInvincible() && !attacking)
        {
            if (PlayerStats.getDealtDmg() && !attacking && !PlayerStats.getInvincible())
            {
                PlayerStats.setDealtDmg(false);
                audioManager.Play("Damage");
                StartCoroutine(IFrames(0.5f));
            }
        }
    }
    void die()
    { 
        Debug.Log("Dead");
        PlayerStats.setHealth(100);
        SceneManager.LoadScene(levelName);
    }

    bool GetFaceButtonDown()
    {
        for(int i = 0; i < 4; i++)
        {
            if(Input.GetKeyDown("joystick 1 button " + i))
            {
                return true;
            }
        }
        return false;
    }


    /*IEnumerator Slide (int dir)
    {
        float slideVel = 0.1f;
        Vector3 vel = Vector3.zero;
        
        if(dir == 1)
        {
            vel.y = slideVel;
        }
        else if(dir == 3)
        {
            vel.y = -1*slideVel;
        }
        else if (dir == 2)
        {
            vel.x = slideVel;
        }
        else if (dir == 4)
        {
            vel.x = -1 * slideVel;
        }

        if (PlayerStats.getSliding() != 0)
        {
            player_rig.transform.position += vel;
        }

        yield return null;
    }*/
    /*public Vector3 getDirecao()
    {
        return direcao;
    }*/

    void playPasso()
    {
        int index = UnityEngine.Random.Range(1, 10);
        Debug.Log("Passo" + index);
        audioManager.Play("Passo" + index);
    }

    void Awake()
    {
        PlayerStats.Awake();
        player_rig = GetComponent<Rigidbody2D>();
        player_coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        isAttacking = false;
        attacking = false;
        walking = false;
        PlayerStats.setInvincible(false);
        lastPos = 0;
        PlayerStats.weaponWheel(0);
        changingWeapon = false;
        direcao = Vector3.zero;
        somPasso = false;
    }

    void Update()
    {
        if (!PauseMenu.getPaused())
        {
            if (somPasso)
            {
                playPasso();
            }
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if(Math.Abs(change.x) < 0.39f)
            {
                change.x = 0;
            }
            if (Math.Abs(change.y) < 0.39f)
            {
                change.y = 0;
            }
            direcao = change;

            if ((Input.GetKeyDown(KeyCode.Mouse0) || GetFaceButtonDown()) && !attacking && PlayerStats.getSliding() == 0)
            {
                audioManager.Play("Ataque");
                StartCoroutine(Attack(0.18f));
            }

            if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown("joystick 1 button 4")) && !changingWeapon)
            {
                PlayerStats.weaponWheel(-1);
                StartCoroutine(Wait(0.15f));
                Debug.LogWarning(PlayerStats.weaponWheel(0));
            }


            else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown("joystick 1 button 5")) && !changingWeapon)
            {
                PlayerStats.weaponWheel(1);
                StartCoroutine(Wait(0.15f));
                Debug.LogWarning(PlayerStats.weaponWheel(0));
            }

            else if (change != Vector3.zero && PlayerStats.getSliding() == 0)
            {
                player_rig.MovePosition(transform.position + change * passo * Time.deltaTime);
                anim.SetFloat("movimentoX", change.x);
                anim.SetFloat("movimentoY", change.y);
                anim.SetBool("Walk", true);
            }
            /*else if (PlayerStats.getSliding() != 0)
            {
                Debug.Log("Slide: " + PlayerStats.getSliding());
                StartCoroutine(Slide(PlayerStats.getSliding()));
            }*/
            else
            {
                anim.SetBool("Walk", false);
            }
            anim.SetInteger("LastPos", lastPos);
            anim.SetBool("Attack", attacking);
            if (PlayerStats.getHealth() <= 0)
            {
                die();
            }
        }
    }
}
