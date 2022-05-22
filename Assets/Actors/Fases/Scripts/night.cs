using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class night : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerStats.getEn())
        {
            spriteRenderer.sprite = sprites[0];
            ;
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
        }
    }
}
