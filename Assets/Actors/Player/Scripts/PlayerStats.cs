using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
    static int i;
    static int health = 100;
    static bool[] DefeatedBosses = new bool[2];
    static bool DealtDmg, Invincible;
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
    public static void setDealtDmg(bool dmg) {
        DealtDmg = dmg;
    }
    public static bool getDealtDmg(){
        return DealtDmg;
    }
    public static void setInvincible(bool inv)
    {
        Invincible = inv;
    }
    public static bool getInvincible()
    {
        return Invincible;
    }
    private static void Start(){
        for(i = 0; i < 2; i++){
            DefeatedBosses[i] = false;
        }
        DealtDmg = false;
        Invincible = false;
    }
}
