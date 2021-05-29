using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
    static int i;
    static int health = 120;
    static bool[] DefeatedBosses = new bool[3];
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
    public static void saveGame(){
        string[] dados = new string[] { health.ToString(), DefeatedBosses[0].ToString(), DefeatedBosses[1].ToString(), DefeatedBosses[2].ToString() };
        File.WriteAllLines("save.txt", dados);
    }
    public static void loadGame(){
        string[] dados = new string[] { };
        dados = File.ReadAllLines("save.txt");
        health = int.Parse(dados[0]);
        for(i = 1; i < 4; i++){
            if (dados[i].Equals("True")){
                DefeatedBosses[i - 1] = true;
            }
            else if (dados[i].Equals("False"))
            {
                DefeatedBosses[i - 1] = false;
            }

        }
    }
    private static void Start(){
        for(i = 0; i < 3; i++){
            DefeatedBosses[i] = false;
        }
        DealtDmg = false;
        Invincible = false;
    }
}
