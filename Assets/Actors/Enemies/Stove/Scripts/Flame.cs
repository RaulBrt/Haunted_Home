using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rig;
    Player play;
    Collider2D coll;
    Vector2 startPos, endPos;
    bool og;
    float angle;
    private Flame[] flame;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Flame flam = collision.gameObject.GetComponent<Flame>();
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (!og)
        {
            if (flam != null) { }
            else
            {
                if (player_coll != null && !PlayerStats.getInvincible())
                {
                    PlayerStats.setDealtDmg(true);
                    PlayerStats.setHealth(PlayerStats.getHealth() - 1);
                }
                Object.Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        flame = FindObjectsOfType<Flame>();
        if (flame.Length <= 1)
        {
            og = true;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(-10000, -10000));
        }
        else
        {
            og = false;
            rig = GetComponent<Rigidbody2D>();
            play = FindObjectOfType<Player>();
            startPos = rig.position;
            angle = Random.Range(175, 355);
            angle *= Mathf.PI / 180;
            if (speed == 0)
            {
                speed = 0.2f;
            }
        }
    }
    void Update()
    {
        if (!og)
        {
            startPos = rig.position;
            startPos.x += Mathf.Cos(angle) * speed;
            startPos.y += Mathf.Sin(angle) * speed;
            rig.MovePosition(startPos);
        }
    }
}
