
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour{
    Player play;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col;
    List<Vector2> physicsShape = new List<Vector2>();
    public GameObject Fire, Flame;
    public Animator anim;
    int action, health, i;
    bool coff, marked, inhaling, blow;
    double clock;

    IEnumerator IFrames(float time)
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        yield return new WaitForSeconds(time);
        GetComponent<PolygonCollider2D>().enabled = true;
    }
    IEnumerator Shoot(float time) {
        coff = true;
        anim.SetBool("Coff", coff);
        yield return new WaitForSeconds(time);
        Instantiate(Fire, new Vector3(0, 6, 0), Quaternion.identity);
    }
    IEnumerator Inhale(){
        Debug.Log("Inhale");
        yield return new WaitForSeconds(2);
        blow = true;
        yield return new WaitForSeconds(5);
        blow = false;
        inhaling = false;
        marked = false;
    }
    public int getHealth()
    {
        return health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            health -= 10;
            Debug.Log("Ouch");
            StartCoroutine(IFrames(0.2f)) ;

        }
    }
    double getDistance(){
        double x, y, h;
        x = Math.Abs(play.GetComponent<Rigidbody2D>().position.x - transform.position.x);
        y = Math.Abs(play.GetComponent<Rigidbody2D>().position.y - transform.position.y);
        x *= x;
        y *= y;
        h = Math.Sqrt(x + y);
        return h;
    }
    void Awake(){
        if (PlayerStats.getDefeated(1)){
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        health = 100;
        play = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        marked = false;

    }

    // Update is called once per frame
    void Update()
    {
        coff = false;
        action = UnityEngine.Random.Range(0, 10000);
        if (action >= 9900 && Time.time > 3)
        {
            StartCoroutine(Shoot(0.2f));
        }
        if(getDistance() <= 4 && !marked){
            clock = Time.time;
            marked = true;
        }
        if (marked && Time.time - clock >= 3 && !inhaling){
            inhaling = true;
            StartCoroutine(Inhale());
        }
        if (blow){
            for(i = 0; i < 7; i++){
                Instantiate(Flame, new Vector3(UnityEngine.Random.Range(-0.5f,0.5f), UnityEngine.Random.Range(5.8f, 6.8f) , 0), Quaternion.identity);
            }
        }
        if (health <= 0)
        {
            PlayerStats.setDefeated(1, true);
            FirePUp firePup = FindObjectOfType<FirePUp>();
            firePup.GetComponent<Transform>().position = new Vector3(0, 1, 0);
            //PlayerStats.saveGame();
            gameObject.SetActive(false);
        }
        anim.SetBool("Coff", coff);
        anim.SetBool("Inhaling", inhaling);
    }
}
