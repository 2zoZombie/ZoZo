using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinDisplayUI : MonoBehaviour
{
    public RectTransform coin;
    public RectTransform blueCoin;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI blueCoinText;

    private void Awake()
    {
        UIManager.Instance.coinDisplayUI = this;
    }
}