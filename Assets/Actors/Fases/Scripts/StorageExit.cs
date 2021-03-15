using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorageExit : MonoBehaviour{
    [SerializeField] string NextLevel;
    bool Hit, BossDead;
    Vacuum vac;
    void Start(){
        Hit = false;
        BossDead = false;
        vac = FindObjectOfType<Vacuum>();
    }
    void Update(){
        if (Hit && PlayerStats.getDefeated(0)){
            Debug.Log("Next Level\n");
            SceneManager.LoadScene(NextLevel);
        }
    }
    void OnCollisionEnter2D(Collision2D coll){
        Player play = coll.gameObject.GetComponent<Player>();
        if (!Hit && play != null){
            Hit = true;
        }
        else{
            Hit = false;
        }
    }
}
