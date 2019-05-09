using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundVariator : MonoBehaviour
{
    private AudioSource source;

    private float basePitch;

    // Start is called before the first frame update
    void Start()
    {
        this.source = GetComponent<AudioSource>();        
        this.basePitch = this.source.pitch;
    }

    public void Play()
    {
        float variation = Random.Range(-0.25f, 0.25f);

        this.source.pitch = this.basePitch + variation;

        this.source.Play();
    }
}
