using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHall : MonoBehaviour
{
    int i;
    bool win = true;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
       for(i = 0; i < 3; i++)
        {
            if (!PlayerStats.getDefeated(i))
            {
                win = false;
            }
        }
        if (win)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
