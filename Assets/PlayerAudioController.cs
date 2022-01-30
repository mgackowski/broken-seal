using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int clip)
    {
        soundPlayer.clip = clips[clip];
        if (!soundPlayer.isPlaying)
        {
            soundPlayer.Play();
        }
        if (clip == 0)
        {
            soundPlayer.Play();
        }
    }

    public void StopSound()
    {
        soundPlayer.Stop();
    }
}
