using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioManager _instans = null;

    private AudioSource _source;
    private AudioClip _clip = null;

    public AudioManager Instans
	{
		get
		{
            if(_instans != null)
			{
                _instans = this;
			}
            return _instans;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySE()
	{
        _source.PlayOneShot(_clip);
	}

    public void PlaySE(AudioClip clip)
	{
        _source.PlayOneShot(_clip);
    }

    public void PlayBGM()
    {
        _source.PlayOneShot(_clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        _source.PlayOneShot(_clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
