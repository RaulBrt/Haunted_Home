using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumPUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            PlayerStats.setPowerup(3, true);
            Object.Destroy(gameObject);
        }

    }
}
