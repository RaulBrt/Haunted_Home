using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
    Player play;
    Collider2D coll;
    Rigidbody2D rig;
    int player_dir;
    Vector3 direcao;


    void Awake()
    {
        play = FindObjectOfType<Player>();
    }
    /*int getDir()
    {
        int dir = 0;

        direcao = play.getDirecao();

        if (direcao.x != 0)
        {
            if (direcao.x > 0)
            {
                dir = 2;
            }
            else
            {
                dir = 4;
            }
        }
        else if (direcao.y != 0)
        {
            if (direcao.y < 0)
            {
                dir = 3;
            }
            else
            {
                dir = 1;
            }
        }

        return dir;
    }*/
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Agua");
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            //PlayerStats.setSliding(getDir());
        }
    }

}
