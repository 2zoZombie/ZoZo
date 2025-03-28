using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject pausePannel;
    public Button pauseButton;
    public Button resumeButton;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        pausePannel.SetActive(false);

    }

    public void OnPause()
    {
        pausePannel.SetActive(true);
    }
}
