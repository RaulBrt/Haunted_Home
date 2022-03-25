using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[PlayerStats.weaponWheel(0)];
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[PlayerStats.weaponWheel(0)];
        
    }
}
