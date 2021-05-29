using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveHB : MonoBehaviour
{
    int health;
    SpriteRenderer spriteRenderer;
    Stove stv;
    public Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        stv = FindObjectOfType<Stove>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (int)stv.getHealth() / 10;
        if (health > 0)
        {
            health -= 1;
        }
        spriteRenderer.sprite = sprites[health];
    }
}
