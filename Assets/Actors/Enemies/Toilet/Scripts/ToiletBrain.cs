using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBrain : MonoBehaviour
{
    private int health, action;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int vida)
    {
        health = vida;
    }

    // Update is called once per frame
    void Update()
    {
        action = UnityEngine.Random.Range(0, 100);
        if(action >= 99)
        {
            //Debug.Log("Teleport");
        }
        else if(action < 99 && action >= 97)
        {
            //Debug.Log("Shoot");
        }
        else if(action < 97 && action >= 95)
        {
            //Debug.Log("Jet");
        }
    }


}
