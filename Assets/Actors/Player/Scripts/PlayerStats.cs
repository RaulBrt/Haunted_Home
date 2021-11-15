using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats{
    static int i, activeWeapon;
    static int maxWeapon = 3;
    static int health = 120;
    static bool[] DefeatedBosses = new bool[3];
    static bool[] Powerup = new bool[maxWeapon+1];
    static bool DealtDmg, Invincible;
    static bool started = false;
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

    public static void setPowerup(int index, bool valor)
    {
        Powerup[index] = valor;
    }
    public static bool getPowerup(int index)
    {
        return Powerup[index];
    }

    public static int weaponWheel(int sum)
    {
        if (activeWeapon + sum > maxWeapon)
        {
            activeWeapon = 0;
        }
        else if (activeWeapon + sum < 0)
        {
            activeWeapon = maxWeapon;
        }
        else
        {
            activeWeapon += sum;
        }
        if (!getPowerup(activeWeapon))
        {
            weaponWheel(sum);
        }
        return activeWeapon;

    }
    /*public static void saveGame(){
        Debug.Log("Check2");
        string[] dados = new string[] { health.ToString(), DefeatedBosses[0].ToString(), DefeatedBosses[1].ToString(), DefeatedBosses[2].ToString() };
        Debug.Log("Check3");
        File.WriteAllLines("Data/save.txt", dados);
        Debug.Log("Check4");
    }
    public static void loadGame(){
        string[] dados = new string[] { };
        dados = File.ReadAllLines("Data/save.txt");
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
    }*/
    public static void Awake(){
        if (!started)
        {
            for (i = 0; i < 3; i++)
            {
                DefeatedBosses[i] = false;
            }
            setPowerup(0, true);
            for (i = 1; i < maxWeapon; i++)
            {
                setPowerup(i, false);
            }
            DealtDmg = false;
            Invincible = false;
            activeWeapon = 0;
            started = true;
        }
    }
}
