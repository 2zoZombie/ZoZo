using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private PlayerData playerData;

    [Header("UI")]
    public CoinDisplayUI coinDisplayUI;
    public ErrorPopup errorPopup;

    protected override void Awake()
    {
        base.Awake();
        playerData = GameManager.Instance.playerData;
    }

    public void Refresh()
    {

    }
}
