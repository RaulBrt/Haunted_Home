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
        for (int i = 0; i < 3; i++)
        {
            PlayerStats.Awake();
            PlayerStats.setDefeated(i, false);
        }
        SceneManager.LoadScene(levelName);
    }
}
