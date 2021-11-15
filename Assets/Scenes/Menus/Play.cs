using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
    [SerializeField] string levelName;

    void OnMouseDown()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(levelName);
    }
}
