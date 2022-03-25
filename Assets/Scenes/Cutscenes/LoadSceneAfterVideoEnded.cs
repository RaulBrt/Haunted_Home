using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public VideoPlayer VP; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    bool isCheckPlaying, triggered;
    IEnumerator espera(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isCheckPlaying = true;
    }
    void Start()
    {
        isCheckPlaying = false;
        triggered = false;
        VP.loopPointReached += LoadScene;
    }

    void Update()
    {
        if(!triggered && !isCheckPlaying)
        {
            triggered = true;
            StartCoroutine(espera(1f));
        }
        if ((!VP.isPlaying && isCheckPlaying) || Input.GetKey(KeyCode.E))
        {

            SceneManager.LoadScene(SceneName);
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        for(int i = 0; i < 3; i++)
        {
            PlayerStats.setDefeated(i, false);
        }
        SceneManager.LoadScene(SceneName);
    }
}
