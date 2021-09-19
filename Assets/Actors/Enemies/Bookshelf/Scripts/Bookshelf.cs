using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    Rigidbody2D rig;
    PolygonCollider2D col;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();
    Player play;
    public Animator anim;
    [SerializeField] int passo;
    int angle, lookAngle, action, health;
    bool isAttack,coolDown,mark,mark2;
    double clock,clock2,clock3;
    Vector2 dir,pos;
    private Book[] book;
    public GameObject bk;
    IEnumerator delay(float tempo)
    {
        yield return new WaitForSeconds(tempo);
    }
    public int getHealth()
    {
        return health;
    }
    int getDir(float angle)
    {
        int dir = 0;
        if (angle >= 67 && angle < 112)
            dir = 1;
        else if (angle >= 22 && angle < 67)
            dir = 2;
        else if ((angle >= 0 && angle < 22) || (angle >= 337 && angle <= 360))
            dir = 3;
        else if (angle >= 292 && angle < 337)
            dir = 4;
        else if (angle >= 247 && angle < 292)
            dir = 5;
        else if (angle >= 202 && angle < 247)
            dir = 6;
        else if (angle >= 157 && angle < 202)
            dir = 7;
        else if (angle >= 112 && angle < 157)
            dir = 8;

        return dir;
    }
    int getAngle(Vector2 origin, Vector2 dest)
    {
        Vector2 direcao = new Vector2(dest.x - origin.x, dest.y - origin.y);
        float angulo ;
        angulo = Mathf.Atan(direcao.y / direcao.x);
        angulo *= 180 / Mathf.PI;
        if (direcao.x < 0)
        {
            angulo += 180;
        }
        else if (direcao.x > 0 && direcao.y < 0)
        {
            angulo += 360;
        }
        return (int)angulo;
    }
    bool checkDamage()
    {
        bool damaged = false;
        if (play.getAttackDir() == 1 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 247 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 292)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 2 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 202 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 247)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 3 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 157 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 202)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 4 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 112 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 157)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 5 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 68 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 112)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 6 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 23 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 68)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 7 && ( getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 337 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 360) || ( getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 0 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 23))
        {
            damaged = true;
        }
        if (play.getAttackDir() == 8 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) > 292 &&  getAngle(pos, play.GetComponent<Rigidbody2D>().position) <= 337)
        {
            damaged = true;
        }
        return damaged;
    }
    private double getDistance()
    {
        double x, y, h;
        x = Math.Abs(play.GetComponent<Rigidbody2D>().position.x - transform.position.x);
        y = Math.Abs(play.GetComponent<Rigidbody2D>().position.y - transform.position.y);
        x *= x;
        y *= y;
        h = Math.Sqrt(x + y);
        return h;
    }
    private bool attack()
    {
        bool isAttack = false;
        if(getDir(lookAngle) == getDir(angle) && getDistance() < 4){
            isAttack = true;
        }
        return isAttack;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            if(mark2 && !Input.GetKey(KeyCode.Mouse0))
            {
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 15);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("Trigger2");
            if (collision.gameObject.CompareTag("Attack"))
            {
                Debug.Log("Trigger3");
                health -= 10;
            }
        }
    void Awake()
    {
        if (PlayerStats.getDefeated(2))
        {
            UnityEngine.Object.Destroy(gameObject);
            health = 0;
        }
    }
    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        play = FindObjectOfType<Player>();
        pos = rig.position;
        lookAngle = 270;
        health = 120;
        passo = 2;
        isAttack = false;
        mark = false;
        mark2 = false;
        coolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        book = FindObjectsOfType<Book>();
        angle = getAngle(pos, play.GetComponent<Rigidbody2D>().position);
        if (angle > lookAngle)
        {
            if (Math.Abs(angle - lookAngle) > lookAngle + (360 - angle))
            {
                lookAngle-=passo;
            }
            else if (Math.Abs(angle - lookAngle) < lookAngle + (360 - angle))
            {
                lookAngle+=passo;
            }
        }
        else if (angle < lookAngle)
        {
            if (Math.Abs(angle - lookAngle) > angle + (360 - lookAngle))
            {
                lookAngle+=passo;
            }
            else if (Math.Abs(angle - lookAngle) < angle + (360 - lookAngle))
            {
                lookAngle-=passo;
            }
        }
        if(lookAngle >= 360)
        {
            lookAngle -= 360;
        }
        if(lookAngle < 0)
        {
            lookAngle += 360;
        }

        if(book.Length < 20)
        {
            action = UnityEngine.Random.Range(0, 10000);
            if(action > 9800)
            {
                Instantiate(bk, new Vector3(UnityEngine.Random.Range(-9,4.5f), UnityEngine.Random.Range(-5, 4), 0), Quaternion.identity);
            }
        }

        if (health <= 0)
        {
            PlayerStats.setDefeated(2, true);
            UnityEngine.Object.Destroy(gameObject);
        }
        anim.SetInteger("Dir", getDir(lookAngle));
        isAttack = attack();
        if (coolDown)
        {
            anim.SetBool("attack", false);
            isAttack = false;
        }
       else if ((isAttack || mark) && !coolDown)
        {
            if (!mark)
            {
                mark = true;
                clock = Time.time;
            }
            if (mark && Time.time - clock > 1)
            {
                anim.SetBool("attack", true);
                mark = false;
                mark2 = true;
                clock2 = Time.time;
            }
        }
        if(mark2 && Time.time-clock2 > 1)
        {
            clock = Time.time;
            anim.SetBool("attack", false);
            mark2 = false;
            coolDown = true;
            clock3 = Time.time;
        }
        if (coolDown && Time.time - clock3 > 5)
        {
            anim.SetBool("attack", false);
            coolDown = false;
        }
        
    }
    void LateUpdate()
    {
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
