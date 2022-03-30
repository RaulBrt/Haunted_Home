using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (player_coll != null)
        {
            if (PlayerStats.getSliding() != 0 && !PlayerStats.getInvincible())
            {
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 10);
                Debug.Log("Escorregou na parede");
                PlayerStats.setSliding(0);
            }
        }
    }
}
