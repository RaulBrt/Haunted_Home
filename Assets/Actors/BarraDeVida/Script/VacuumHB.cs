using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumHB : MonoBehaviour
{
    int health;
    SpriteRenderer spriteRenderer;
    Vacuum vac;
    public Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        vac = FindObjectOfType<Vacuum>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (int)vac.getHealth() / 10;
        if (health > 0)
        {
            health -= 1;
        }
        if(health < 0)
        {
            health = 0;
        }
        spriteRenderer.sprite = sprites[health];
    }
}

