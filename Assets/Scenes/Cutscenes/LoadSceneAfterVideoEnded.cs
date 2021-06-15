using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAfterVideoEnded : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public string SceneName;
    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
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
