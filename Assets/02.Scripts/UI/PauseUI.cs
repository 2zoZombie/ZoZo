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

    private void Awake()
    {
        UIManager.Instance.pausePanel = this.gameObject;
    }

    private void Start()
    {
        if (pauseButton != null) pauseButton.onClick.AddListener(OnPause);
        if (resumeButton != null) resumeButton.onClick.AddListener(OnResume);
        if (bgmSlider != null) bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBGMVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        pausePannel.SetActive(false);

    }

    public void OnPause()
    {
        GameManager.Instance.GameStop();
        pausePannel.SetActive(true);
        UIManager.Instance.DIM(true);
    }

    public void OnResume()
    {
        GameManager.Instance.GameResume();
        UIManager.Instance.DIM(false);
        pausePannel.SetActive(false);
    }
}
