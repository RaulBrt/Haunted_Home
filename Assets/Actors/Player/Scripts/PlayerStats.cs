using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
    static int i;
    static int health = 100;
    static bool[] DefeatedBosses = new bool[2];
    public static int getHealth() {
        return health;
    }
    public static void setHealth(int num){
        health = num;
    }
    public static void setDefeated(int index, bool state){
        DefeatedBosses[index] = state;
    }
    public static bool getDefeated(int index){
        return DefeatedBosses[index];
    }
    private static void Start(){
        for(i = 0; i < 2; i++){
            DefeatedBosses[i] = false;
        }
    }
}
