using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSFX : MonoBehaviour
{
    public List<AudioClip> clips;
    public void Play()
    {
        GetComponent<AudioSource>().clip = clips[Random.Range(0, clips.Count)];
        GetComponent<AudioSource>().Play();
    }
}
