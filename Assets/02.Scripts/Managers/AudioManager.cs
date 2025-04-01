using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer audioMixer;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip bgmClip;
    public AudioClip[] sfxClip;

    protected override void Awake()
    {
        base.Awake();
        bgmSource.loop = true;
    }
    private void Start()
    {
        //PlayBGM(bgmClip);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;

            bgmSource.Play();
    }


    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }

    }

    public void SetBGMVolume(float value)
    {
        float safeValue = Mathf.Clamp(value, 0.0001f, 1f);//log10 (0)일경우 무한수렴 방어코드
        audioMixer.SetFloat("BGM", Mathf.Log10(safeValue) * 20);
    }

    public void SetSFXVolume(float value)
    {
        float safeValue = Mathf.Clamp(value, 0.0001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(safeValue) * 20);
    }


}
