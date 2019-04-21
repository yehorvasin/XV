using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChanVoice : MonoBehaviour
{
	public AudioClip breathing;
	public AudioClip hehe;
	public AudioClip nice;
	public AudioClip buybuy;
	public AudioClip yata;
	public AudioClip yo;
    
    public AudioClip l0;
    public AudioClip l1;

	AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayBreathing()
    {
        source.clip = breathing;
        source.Play();
    }

    public void PlayHuhu()
    {
        source.clip = hehe;
        source.Play();
    }

    public void PlayNice()
    {
        source.clip = nice;
        source.Play();
    }
    
    public void PlayBuyBuy()
    {
        source.clip = buybuy;
        source.Play();
    }

    public void PlayYata()
    {
        source.clip = yata;
        source.Play();
    }
    
    public void PlayYo()
    {
        source.clip = yo;
        source.Play();
    }

    public void PlayLaught()
    {
        float r = Random.Range(-1, 1);
        source.clip = (r < 0) ? l0 : l1;
        source.Play();
    }
    
}
