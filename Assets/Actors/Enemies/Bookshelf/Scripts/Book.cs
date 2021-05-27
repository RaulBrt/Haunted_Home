using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    Rigidbody2D rig;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D col;
    List<Vector2> physicsShape = new List<Vector2>();
    public Animator anim;
    [SerializeField] float speed;
    float angle;
    Player play;
    Vector2 pos, dir;
    bool og;
    int action;
    private Book[] book;
    private Bookshelf BS;
    Vector2 getDir()
    {
        Vector2 dir, playerPos;
        pos = rig.position;
        playerPos = play.GetComponent<Rigidbody2D>().position;
        dir = playerPos - pos;
        dir.Normalize();
        return dir;
    }
    float getWalkingAngle(Vector2 direcao)
    {
        float angulo;
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
        return angulo;
    }
    bool checkDamage()
    {
        bool damaged = false;
        if (play.getAttackDir() == 1 && getWalkingAngle(getDir()) > 247 && getWalkingAngle(getDir()) <= 292)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 2 && getWalkingAngle(getDir()) > 202 && getWalkingAngle(getDir()) <= 247)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 3 && getWalkingAngle(getDir()) > 157 && getWalkingAngle(getDir()) <= 202)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 4 && getWalkingAngle(getDir()) > 112 && getWalkingAngle(getDir()) <= 157)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 5 && getWalkingAngle(getDir()) > 68 && getWalkingAngle(getDir()) <= 112)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 6 && getWalkingAngle(getDir()) > 23 && getWalkingAngle(getDir()) <= 68)
        {
            damaged = true;
        }
        if (play.getAttackDir() == 7 && (getWalkingAngle(getDir()) > 337 && getWalkingAngle(getDir()) <= 360) || (getWalkingAngle(getDir()) > 0 && getWalkingAngle(getDir()) <= 23))
        {
            damaged = true;
        }
        if (play.getAttackDir() == 8 && getWalkingAngle(getDir()) > 292 && getWalkingAngle(getDir()) <= 337)
        {
            damaged = true;
        }
        return damaged;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        Book book_coll = collision.gameObject.GetComponent<Book>();
        BottomWall bw = collision.gameObject.GetComponent<BottomWall>();
        TopWall tw = collision.gameObject.GetComponent<TopWall>();
        LeftWall lw = collision.gameObject.GetComponent<LeftWall>();
        RightWall rw = collision.gameObject.GetComponent<RightWall>();
        if (player_coll != null)
        {
            if (checkDamage() && !og)
            {
                Object.Destroy(gameObject);
            }
            else if (!PlayerStats.getInvincible() && !play.attacking)
            {
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 2);
            }
        }
        else if(book_coll != null)
        {
            Physics2D.IgnoreCollision(book_coll.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            if (lw != null)
            {
                angle = 0;
            }
            else if (rw != null)
            {
                angle = 180;
            }
            else if (tw != null)
            {
                angle = 270;
            }
            else if (bw != null)
            {
                angle = 90;
            }
        }
        angle *= Mathf.PI / 180;
    }
    void Start()
    {
        if (PlayerStats.getDefeated(2))
        {
            
        }
        anim = GetComponent<Animator>();
        book = FindObjectsOfType<Book>();
        BS = FindObjectOfType<Bookshelf>();
        if (book.Length <= 1)
        {
            og = true;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(-10000, -10000));
        }
        else
        {
            og = false;
            rig = GetComponent<Rigidbody2D>();
            col = GetComponent<PolygonCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            pos = rig.position;
            play = FindObjectOfType<Player>();
            angle = Random.Range(0, 360);
            angle *= Mathf.PI / 180;
            if (speed == 0)
            {
                speed = 0.09f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(BS.getHealth() <= 0 || !BS.enabled)
        {
            Object.Destroy(gameObject);
        }
        if (!og)
        {
            pos.x += Mathf.Cos(angle) * speed;
            pos.y += Mathf.Sin(angle) * speed;
            action = Random.Range(0, 100);
            if (action >= 98)
            {
                angle = Random.Range(0, 360);
                angle *= Mathf.PI / 180;
            }
            rig.MovePosition(pos);
        }
        anim.SetFloat("Tan", Mathf.Tan(angle));
    }
    void LateUpdate()
    {
        if (!og)
        {
            spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
            col.SetPath(0, physicsShape);
        }
    }
}
