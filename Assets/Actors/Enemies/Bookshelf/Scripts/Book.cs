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
<<<<<<< HEAD
        Player player_coll = collision.gameObject.GetComponent<Player>();
=======
        Debug.Log("Trigger");
        Player player_coll = collision.gameObject.GetComponent<Player>();
        PlayerATK player_atk = collision.gameObject.GetComponent<PlayerATK>();
>>>>>>> e9060f2cafd8266c7768c6b4545aa34b15d53a17
        BottomWall bw = collision.gameObject.GetComponent<BottomWall>();
        TopWall tw = collision.gameObject.GetComponent<TopWall>();
        LeftWall lw = collision.gameObject.GetComponent<LeftWall>();
        RightWall rw = collision.gameObject.GetComponent<RightWall>();
<<<<<<< HEAD
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
=======
        if (player_coll != null && !PlayerStats.getInvincible() && !play.attacking)
        {
            PlayerStats.setDealtDmg(true);
            PlayerStats.setHealth(PlayerStats.getHealth() - 5);
        }
        if(player_atk != null)
        {
            Object.Destroy(gameObject);
        }
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

  /*  private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisao");
>>>>>>> e9060f2cafd8266c7768c6b4545aa34b15d53a17
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
<<<<<<< HEAD
    }
=======
    }*/
>>>>>>> e9060f2cafd8266c7768c6b4545aa34b15d53a17
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
