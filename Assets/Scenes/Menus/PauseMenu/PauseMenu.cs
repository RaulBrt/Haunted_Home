using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    GameObject cam;
    GameObject[] enemy;
    SpriteRenderer spriteRenderer;
    bool change;
    static bool paused;
    public Sprite[] sprites;

    static public bool getPaused()
    {
        return paused;
    }
    void pause()
    {
        paused = true;
        gameObject.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y,0);
        foreach (GameObject go in enemy)
        {
            go.SetActive(false);
        }
        Debug.Log("Pause");
    }
    void continuar()
    {
        foreach (GameObject go in enemy)
        {
            go.SetActive(true);
        }
        paused = false;
        gameObject.transform.position = new Vector3 (1000, 1000, 0);
        Debug.Log("Continuar");
    }
    void Start()
    {
        change = false;
        paused = false;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
        if (paused)
        {
            continuar();
        }
    }

    // Update is called once per frame
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
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick 1 button 8") || Input.GetKeyDown("joystick 1 button 9")){
            change = !change;
        }
        if (change)
        {
            change = false;
            if (!paused)
            {
                pause();
            }
            else
            {
                continuar();
            }
        }
    }
}
