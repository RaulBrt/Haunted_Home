using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partiture : MonoBehaviour
{
    public int mod;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        Nota note = collision.gameObject.GetComponent<Nota>();
        Piano piano = collision.gameObject.GetComponent<Piano>();
        Player player_coll = collision.gameObject.GetComponent<Player>();
        if (note != null || piano != null) { }
        else
        {
            if (player_coll != null && !PlayerStats.getInvincible())
            {
                PlayerStats.setDealtDmg(true);
                PlayerStats.setHealth(PlayerStats.getHealth() - 10);
            }
        }
    }
    IEnumerator rotacionar(float segundos)
    {
        Debug.Log("Rodando");
        while (true)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, mod*0.55f));
            yield return new WaitForSeconds(segundos);
        }
    }

    void Start()
    {
        mod = 1;
        StartCoroutine(rotacionar(0.01f));
    }
}
