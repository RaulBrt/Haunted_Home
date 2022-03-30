using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nota : MonoBehaviour
{
    [SerializeField] float speed;
    float angle;
    Rigidbody2D rig;
    Player play;
    Collider2D coll;
    Vector2 startPos, endPos;
    bool og;
    private Nota[] nota;

    public void setAngle(int angulo)
    {
        angle = (float)angulo;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Nota note = collision.gameObject.GetComponent<Nota>();
        Piano piano = collision.gameObject.GetComponent<Piano>();
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (!og)
        {
            if (note != null || piano != null) { }
            else
            {
                if (player_coll != null && !PlayerStats.getInvincible())
                {
                   PlayerStats.setDealtDmg(true);
                   PlayerStats.setHealth(PlayerStats.getHealth() - 15);
                }
                Object.Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        nota = FindObjectsOfType<Nota>();
        if (nota.Length <= 1)
        {
            og = true;
            speed = 0;
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(100, 200));
        }
        else
        {
            og = false;
            rig = GetComponent<Rigidbody2D>();
            play = FindObjectOfType<Player>();
            startPos = rig.position;
            angle *= Mathf.PI / 180;
            if (speed == 0)
            {
                speed = 0.1f;
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
