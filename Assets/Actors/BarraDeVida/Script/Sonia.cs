using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonia : MonoBehaviour
{
    int health;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    void Start()
    {
        health = (int)PlayerStats.getHealth() / 10;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (int)PlayerStats.getHealth() / 10;
        if (health > 0)
        {
            health -= 1;
        }
        spriteRenderer.sprite = sprites[health];
    }
}
