using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    Rigidbody2D rig;
    PolygonCollider2D col;
    SpriteRenderer spriteRenderer;
    List<Vector2> physicsShape = new List<Vector2>();
    ToiletBrain brain;
    // Start is called before the first frame update

    IEnumerator IFrames(float time)
    {
        Debug.Log("Ouch");
        GetComponent<PolygonCollider2D>().enabled = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(time);
        GetComponent<PolygonCollider2D>().enabled = true;
        spriteRenderer.color = Color.white;
    }

    void Awake()
    {
        brain = FindObjectOfType<ToiletBrain>();
    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        float mod;
        mod = 1;
        if (PlayerStats.weaponWheel(0) == 3)
        {
            mod = 1.5f;
        }
        else if (PlayerStats.weaponWheel(0) == 1)
        {
            mod = 0.5f;
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            Debug.Log("Ouch");
            brain.setHealth(brain.getHealth() - Convert.ToInt32(10 * mod));
            Debug.Log(brain.getHealth());
            StartCoroutine(IFrames(0.5f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        spriteRenderer.sprite.GetPhysicsShape(0, physicsShape);
        col.SetPath(0, physicsShape);
    }
}
