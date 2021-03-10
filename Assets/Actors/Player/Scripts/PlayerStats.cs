using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
    static int health = 100;
    public static int getHealth() {
        return health;
    }
    public static void setHealth(int num){
        health = num;
    }
}
