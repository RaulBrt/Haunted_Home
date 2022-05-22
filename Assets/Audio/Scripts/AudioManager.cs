using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    bool playing;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1;
        }
        playing = false;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        playing = false;
        foreach (Sound h in sounds)
        {
            if(h.source.isPlaying)
            {
                playing = true;
            }
        }
        if (!playing)
        {
            s.source.Play();
        }
    }

}
