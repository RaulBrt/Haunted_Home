using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LibraryExit : MonoBehaviour
{
    [SerializeField] string NextLevel;
    bool Hit, BossDead;
    Bookshelf BS;
    void Start()
    {
        Hit = false;
        BossDead = false;
        BS = FindObjectOfType<Bookshelf>();
    }
    void Update()
    {
        if (Hit && PlayerStats.getDefeated(2))
        {
            Debug.Log("Check1");
            //PlayerStats.saveGame();
            Debug.Log("Check5");
            SceneManager.LoadScene(NextLevel);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Hit = false;
        Player play = coll.gameObject.GetComponent<Player>();
        if (!Hit && play != null)
        {
            Hit = true;
        }
    }
}
