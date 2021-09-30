using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPUp : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            PlayerStats.setPowerup(1, true);
            Object.Destroy(gameObject);
        }

    }
}
