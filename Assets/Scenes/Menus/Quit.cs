using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
