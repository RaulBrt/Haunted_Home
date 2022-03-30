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
<<<<<<< HEAD
=======
        for (int i = 0; i < 3; i++)
        {
            PlayerStats.Awake();
            PlayerStats.setDefeated(i, false);
        }
>>>>>>> e9060f2cafd8266c7768c6b4545aa34b15d53a17
        SceneManager.LoadScene(levelName);
    }
}
