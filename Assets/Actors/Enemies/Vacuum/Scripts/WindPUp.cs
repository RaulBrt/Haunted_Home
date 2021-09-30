using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            PlayerStats.setPowerup(2, true);
            Object.Destroy(gameObject);
        }

    }
}
