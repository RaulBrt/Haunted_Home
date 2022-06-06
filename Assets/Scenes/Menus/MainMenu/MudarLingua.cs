using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudarLingua : MonoBehaviour
{
    GameObject menu;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu");
        spriteRenderer = menu.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {

        if (!PlayerStats.getEn())
        {
            spriteRenderer.sprite = sprites[1];
            PlayerStats.setEn(true);
        }

        else if (PlayerStats.getEn())
        {
            spriteRenderer.sprite = sprites[0];
            PlayerStats.setEn(false);
        }
    }
}
