using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            PlayerStats.setPowerup(2, true);
            Object.Destroy(gameObject);
        }

    }
}
