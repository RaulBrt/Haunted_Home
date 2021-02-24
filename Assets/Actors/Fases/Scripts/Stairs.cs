using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : MonoBehaviour{
    [SerializeField] string LevelName;
    bool Hit;
    void Start(){
        Hit = false;
    }
    void Update(){
        if (Hit){
            Debug.Log("Next Level\n");
            SceneManager.LoadScene(LevelName);
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
