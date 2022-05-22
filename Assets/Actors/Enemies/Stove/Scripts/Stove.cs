
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour{
    Player play;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col;
    AudioManager audioManager;
    List<Vector2> physicsShape = new List<Vector2>();
    public GameObject Fire, Flame;
    public Animator anim;
    int action, health, i;
    bool coff, marked, inhaling, blow;
    double clock,mod;

    IEnumerator IFrames(float time)
    {
        Debug.Log("Ouch");
        GetComponent<BoxCollider2D>().enabled = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        GetComponent<BoxCollider2D>().enabled = true;
        spriteRenderer.color = Color.white;
    }
    IEnumerator Shoot(float time) {
        coff = true;
        anim.SetBool("Coff", coff);
        yield return new WaitForSeconds(time);
        Instantiate(Fire, new Vector3(0, 6, 0), Quaternion.identity);
    }
    IEnumerator Inhale(){
        audioManager.Play("Inhale");
        yield return new WaitForSeconds(3);
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

        mod = 1;
        if(PlayerStats.weaponWheel(0) == 2)
        {
            mod = 1.5f;
        }
        else if(PlayerStats.weaponWheel(0) == 3)
        {
            mod = 0.5f;
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            health -= Convert.ToInt32(10*mod);
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
        health = 350;
        play = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        marked = false;
        mod = 1;
    }

    // Update is called once per frame
    void Update()
    {
        coff = false;
        action = UnityEngine.Random.Range(0, 1000);
        if (action >= 995 && !inhaling)
        {
            StartCoroutine(Shoot(0.2f));
        }
        if(getDistance() <= 4 && !marked){
            clock = Time.time;
            marked = true;
        }
        if ((marked && Time.time - clock >= 3 && !inhaling)){
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
