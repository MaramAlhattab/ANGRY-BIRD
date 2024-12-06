using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    void Awake()
    {

        if (instance == null)
        {
            instance =this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayClib(AudioClip Clip,AudioSource Source)
    {

        Source.clip = Clip;
        Source.Play();
    }
    public void PlayRandomClib(AudioClip[] Clips, AudioSource Source)
    {
        int randomIndex = Random.Range(0, Clips.Length);
        Source.clip = Clips[randomIndex];
        Source.Play();
    }
}
