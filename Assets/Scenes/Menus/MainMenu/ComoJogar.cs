using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComoJogar : MonoBehaviour
{
    GameObject mainMenu;
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("Menu");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (PlayerStats.getEn())
        {
            spriteRenderer.sprite = sprites[1];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

    void OnMouseDown()
    {
        transform.position = new Vector3(100, 100, 0);
        mainMenu.transform.position = Vector3.zero;
    }


}
