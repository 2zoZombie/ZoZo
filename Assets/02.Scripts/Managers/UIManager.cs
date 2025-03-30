using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private PlayerData playerData;

    [Header("UI")]
    public CoinDisplayUI coinDisplayUI;
    public ErrorPopup errorPopup;
    public GameObject pausePanel;
    public GameObject enhancePanel;
    public GameObject dimBackground;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("Buttons")]
    public Button newGameButton;
    public Button loadGameButton;

    protected override void Awake()
    {
        base.Awake();
        playerData = GameManager.Instance.playerData;
        if (newGameButton != null) newGameButton.onClick.AddListener(OnNewGame);
        if (loadGameButton != null) loadGameButton.onClick.AddListener(OnLoadGame);
    }

    public void Refresh()
    {
    }

    public void OnNewGame()
    {
        GameManager.Instance.NewGame();
    }

    public void OnLoadGame()
    {
        GameManager.Instance.LoadGame();
    }

    public void OpenPause()
    {
        CloseAllPanels();
        pausePanel.SetActive(true);
        dimBackground.SetActive(true);
        GameManager.Instance.GameStop();
    }

    public void OpenWeaponEnhance()
    {
        CloseAllPanels();
        enhancePanel.SetActive(true);
        dimBackground.SetActive(true);
        GameManager.Instance.GameStop();
    }


    public void CloseAllPanels()
    {
        pausePanel.SetActive(false);
        enhancePanel.SetActive(false);
        dimBackground.SetActive(false);
        GameManager.Instance.GameResume();
    }

    public void FadeOut(System.Action onComplete)//액션을 이용해 메서드를 매개변수로 받기
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1, fadeDuration).OnComplete(() =>
        {
            onComplete?.Invoke(); // 콜백
        });
    }

    public void FadeIn()
    {
        fadeImage.DOFade(0, fadeDuration).OnComplete(() =>
        {
            fadeImage.raycastTarget = false;
        });
    }
}
