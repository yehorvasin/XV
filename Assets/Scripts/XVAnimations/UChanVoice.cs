using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChanVoice : MonoBehaviour
{
	public AudioClip breathing;
	public AudioClip hehe;

	AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
