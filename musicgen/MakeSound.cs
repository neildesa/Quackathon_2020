using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Make sure there is an Audio Source component on the GameObject
[RequireComponent(typeof(AudioSource))]
public class MakeSound : MonoBehaviour
{

    public int startingPitch = -12;
    public int timeToSwitch = 10;
    public AudioSource toneAudioSource;
    public AudioSource pluckAudioSource;

    void Start()
    {
        //Fetch the AudioSource from the GameObject
        if (toneAudioSource == null)
            toneAudioSource = GetComponent<AudioSource>();

        //Initialize the pitch
        if (pluckAudioSource.volume > 0.99f)
            pluckAudioSource.volume = 0.5f;
        toneAudioSource.pitch = startingPitch;
    }

    int[] pitches = new int[] {-12, -8, -5, -3, 0, 4, 7, 9, 12};

    void Update()
    {
        //While the pitch is over 0, decrease it as time passes.
        /*
        if (audioSource.pitch > 0)
        {
            audioSource.pitch -= Time.deltaTime * startingPitch / timeToSwitch;
        }
        */
        if (!toneAudioSource.isPlaying)
        {
            int nextPitch = pitches[Random.Range(0, pitches.Length - 1)];
            toneAudioSource.pitch = Mathf.Pow( 1.05946f,nextPitch);
            toneAudioSource.time = Random.Range(1, 8) * 0.05f;
            toneAudioSource.Play();
        }

        int[] pluckPitches = new int[] { -12, -5, 0, 7 };
        if (!pluckAudioSource.isPlaying)
        {
            int nextPitch = pluckPitches[Random.Range(0, pluckPitches.Length - 1)];
            pluckAudioSource.pitch = Mathf.Pow(1.05946f, nextPitch);
            toneAudioSource.time = Random.Range(1, 4) * 0.05f;
            pluckAudioSource.Play();
        }
    }

}
