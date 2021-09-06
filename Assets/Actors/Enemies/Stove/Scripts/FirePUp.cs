using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            PlayerStats.setPowerup(0,true);
            Object.Destroy(gameObject);
        }
        
    }
}
