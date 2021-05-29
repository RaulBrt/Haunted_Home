using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfHB : MonoBehaviour
{
    int health;
    SpriteRenderer spriteRenderer;
    Bookshelf book;
    public Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        book = FindObjectOfType<Bookshelf>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (int)book.getHealth() / 10;
        if (health > 0)
        {
            health -= 1;
        }
        spriteRenderer.sprite = sprites[health];
    }
}

