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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PauseMenu.getPaused())
        {
            Player player_coll = collision.gameObject.GetComponent<Player>();
            BottomWall bw = collision.gameObject.GetComponent<BottomWall>();
            TopWall tw = collision.gameObject.GetComponent<TopWall>();
            LeftWall lw = collision.gameObject.GetComponent<LeftWall>();
            RightWall rw = collision.gameObject.GetComponent<RightWall>();
            if (player_coll != null)
            {
                if (play.attacking && !og)
                {
                    Object.Destroy(gameObject);
                }
                else if (!PlayerStats.getInvincible() && !play.attacking)
                {
                    PlayerStats.setDealtDmg(true);
                    PlayerStats.setHealth(PlayerStats.getHealth() - 5);
                }
            }
            else if (lw != null || rw != null || tw != null || bw != null)
            {
                Object.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PauseMenu.getPaused())
        {
            Player player_coll = collision.gameObject.GetComponent<Player>();
            BottomWall bw = collision.gameObject.GetComponent<BottomWall>();
            TopWall tw = collision.gameObject.GetComponent<TopWall>();
            LeftWall lw = collision.gameObject.GetComponent<LeftWall>();
            RightWall rw = collision.gameObject.GetComponent<RightWall>();
            if (player_coll != null)
            {

                if (!PlayerStats.getInvincible() && !play.attacking)
                {
                    PlayerStats.setDealtDmg(true);
                    PlayerStats.setHealth(PlayerStats.getHealth() - 3);
                }
            }
            else if (lw != null || rw != null || tw != null || bw != null)
            {
                Object.Destroy(gameObject);
            }
        }
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
            GetComponent<Rigidbody2D>().MovePosition(new Vector2(200, 200));
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
                speed = 0.02f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.getPaused())
        {
            if (BS.getHealth() <= 0 || !BS.enabled)
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
