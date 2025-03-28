using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private PlayerData playerData;

    [Header("UI")]
    public CoinDisplayUI coinDisplayUI;
    public ErrorPopup errorPopup;
    public GameObject pausePanel;
    public GameObject enhancePanel;
    public GameObject dimBackground;


    protected override void Awake()
    {
        base.Awake();
        playerData = GameManager.Instance.playerData;
    }

    public void Refresh()
    {
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
}
