using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoHB : MonoBehaviour
{

    int health;
    SpriteRenderer spriteRenderer;
    Piano piano;
    public Sprite[] sprites;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        piano = FindObjectOfType<Piano>();
    }

    // Update is called once per frame
    void Update()
    {
        health = (int)(piano.getHealth() * 0.24f);
        if (health < 0)
        {
            health = 0;
        }
        spriteRenderer.sprite = sprites[health];
    }
}
