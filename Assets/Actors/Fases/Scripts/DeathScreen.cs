using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour{
    [SerializeField] string LevelName;
    bool isDead,terminou;
    public Animator anim;
    IEnumerator delay(int seconds){
        yield return new WaitForSeconds(seconds);
        terminou = true;

    }
    void Start(){
        isDead = false;
        terminou = false;
        anim = GetComponent<Animator>();
        isDead = true;
        anim.SetBool("isDead", isDead);
        StartCoroutine(delay(3));
        
    }
    void Update() {
        if (terminou){
            SceneManager.LoadScene(LevelName);
        }

    }
}
