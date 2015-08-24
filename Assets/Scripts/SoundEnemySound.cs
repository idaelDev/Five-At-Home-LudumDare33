using UnityEngine;
using System.Collections;

public class SoundEnemySound : MonoBehaviour {

    public AudioClip detection;
    public AudioClip die;

    AudioSource audio;

    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    public void PlayDetection()
    {
        if (audio.clip != detection)
        {
            audio.clip = detection;
        }
        audio.loop = false;
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void PlayDie()
    {
        if (audio.clip != die)
        {
            audio.clip = die;
        }
        audio.loop = false;
        audio.Play();
    }
}
