using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Creditos : MonoBehaviour
{
    [SerializeField] string levelName;

    void OnMouseDown()
    {
        SceneManager.LoadScene(levelName);
    }
}
