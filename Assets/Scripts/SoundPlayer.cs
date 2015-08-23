using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    AudioSource audio;
    public float pitchVariation;

    public void PlaySound()
    {
        float r = Random.Range(-pitchVariation, +pitchVariation);
        audio.pitch = 1 + r;
        audio.Play();
    }
}
