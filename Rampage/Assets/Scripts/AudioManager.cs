using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] soundArray;

     void Awake()
    {
        foreach(Sound s in soundArray)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volime;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
       Sound s = Array.Find(soundArray, sound => sound.name == name);
       s.source.Play();
    }
}
