﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instans = null;

    // AudioSource取得変数
    private AudioSource _source;

	private void Awake()
	{
		if(instans == null)
		{
            instans = this;
            DontDestroyOnLoad(this.gameObject);
		}
		else
		{
            Destroy(this.gameObject);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // SE再生(重複なし)
    public void PlaySE()
	{
        if (_source.clip == null)
        {
            return;
        }
        _source.PlayOneShot(_source.clip);
	}

    // SE再生(重複なし)
    public void PlaySE(AudioClip clip)
	{
        _source.PlayOneShot(clip);
    }

    // BGM再生(重複なし)
    public void PlayBGM()
    {
        if (_source.clip == null)
        {
            return;
        }
        StopAllCoroutines();
        _source.Stop();
        StartCoroutine(BGM(_source.clip));
    }

    // BGM再生(重複なし)
    public void PlayBGM(AudioClip clip)
    {
        StopAllCoroutines();
        _source.Stop();
        StartCoroutine(BGM(clip));
    }

    // BGMループ(重複なし)
    private IEnumerator BGM(AudioClip clip)
	{
        while(true)
		{
            if(_source.isPlaying == false)
			{
                _source.PlayOneShot(_source.clip);
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
