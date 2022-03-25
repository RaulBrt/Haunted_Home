using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    GameObject cam;
    GameObject[] enemy;
    bool change;
    static bool paused;
    
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
        if (Input.GetKeyDown(KeyCode.Escape)){
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
