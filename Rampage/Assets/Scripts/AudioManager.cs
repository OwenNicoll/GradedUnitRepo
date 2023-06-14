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
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
       Sound s = Array.Find(soundArray, sound => sound.name == name);
       s.source.Play();
    }

    public void PlayRandPitch(string name)
    {
        Sound s = Array.Find(soundArray, sound => sound.name == name);
        s.source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        s.source.Play();
    }
}
