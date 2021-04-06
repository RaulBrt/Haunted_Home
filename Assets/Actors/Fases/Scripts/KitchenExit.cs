using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenExit : MonoBehaviour
{
    [SerializeField] string NextLevel;
    bool Hit, BossDead;
    Stove sto;
    void Start()
    {
        Hit = false;
        BossDead = false;
        sto = FindObjectOfType<Stove>();
    }
    void Update()
    {
        if (Hit && PlayerStats.getDefeated(1))
        {
            Debug.Log("Next Level\n");
            SceneManager.LoadScene(NextLevel);
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Player play = coll.gameObject.GetComponent<Player>();
        if (!Hit && play != null)
        {
            Hit = true;
        }
        else
        {
            Hit = false;
        }
    }
}
