using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour{
    Player play;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D col;
    List<Vector2> physicsShape = new List<Vector2>();
    public GameObject Fire;
    public Animator anim;
    int action, health;
    bool coff;
    IEnumerator Shoot(float time) {
        coff = true;
        anim.SetBool("Coff", coff);
        yield return new WaitForSeconds(time);
        Instantiate(Fire, new Vector3(0, 7.25f, 0), Quaternion.identity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            if (play.attacking)
            {
                health -= 5;
                Debug.Log("Ouch");
            }
        }
    }
    void Start()
    {
        health = 100;
        play = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        coff = false;
        action = Random.Range(0, 10000);
        if (action >= 9800 && Time.time > 5)
        {
            StartCoroutine(Shoot(0.2f));
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        anim.SetBool("Coff", coff);
    }
}
