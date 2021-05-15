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
    int angle, lookAngle, angleDiff;
    Vector2 dir,pos;
    int action, health;
    private Book[] book;
    public GameObject bk;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            if (checkDamage() && play.attacking)
            {
                health -= 10;
            }
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
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        book = FindObjectsOfType<Book>();
        angle = getAngle(pos, play.GetComponent<Rigidbody2D>().position);
        if (Mathf.Tan(angle * (Mathf.PI /180)) > Mathf.Tan(lookAngle * (Mathf.PI / 180))){
            lookAngle++;
        }
        else /*if (Mathf.Tan(angle * (Mathf.PI / 180)) < Mathf.Tan(lookAngle * (Mathf.PI / 180)))*/
        {
            lookAngle--;
        }
        if (lookAngle > 360)
        {
            lookAngle-=360;
        }
        if (lookAngle < 0)
        {
            lookAngle += 360;
        }

        if(book.Length < 20)
        {
            action = UnityEngine.Random.Range(0, 10000);
            if(action > 9800)
            {
                Instantiate(bk, new Vector3(UnityEngine.Random.Range(-10,10), UnityEngine.Random.Range(-10, 10), 0), Quaternion.identity);
            }
        }
        Debug.Log("Angle: " + angle + " Look Angle: " + lookAngle);
        if (health <= 0)
        {
            PlayerStats.setDefeated(2, true);
            PlayerStats.saveGame();
            gameObject.SetActive(false);
        }

    }
    void LateUpdate()
    {
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
