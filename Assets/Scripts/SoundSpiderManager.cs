using UnityEngine;
using System.Collections;

public class SoundSpiderManager : MonoBehaviour {

    public AudioClip walk;
    public AudioClip eat;
    public AudioClip die;
    public AudioClip levelUp;

    public AudioSource audio;

	// Use this for initialization
	void Start () {
	}

    public void PlayWalk()
    {
        if(audio.clip != walk)
        {
            audio.clip = walk;
        }
        audio.loop = true;
        if(!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void PlayEat()
    {
        if (audio.clip != eat)
        {
            audio.clip = eat;
        }
        audio.loop = false;
        audio.Play();
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

    public void PlayLevelUp()
    {
        if (audio.clip != levelUp)
        {
            audio.clip = levelUp;
        }
        audio.loop = false;
        audio.Play();
    }

    public void Stop()
    {
        audio.Stop();
    }
}
