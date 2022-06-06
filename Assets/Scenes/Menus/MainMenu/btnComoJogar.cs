using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnComoJogar : MonoBehaviour
{
    GameObject mainMenu, controles;
    void Start()
    {
        mainMenu = GameObject.FindGameObjectWithTag("Menu");
        controles = GameObject.FindGameObjectWithTag("Controles");
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        mainMenu.transform.position = new Vector3(100, 100, 0);
        controles.transform.position = Vector3.zero;
        
    }
}
