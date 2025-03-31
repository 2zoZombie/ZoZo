using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinDisplayUI : MonoBehaviour
{
    public ParticleSystem coinParticle;
    public RectTransform coin;
    public RectTransform blueCoin;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI blueCoinText;

    private void Awake()
    {
        UIManager.Instance.coinDisplayUI = this;
        GameManager.Instance.OnBlueCoinChange += SetCoinText;
        GameManager.Instance.OnCoinChange += SetCoinText;
    }

    public void SetCoinText()
    {
        coinText.text = GameManager.Instance.playerData.coin.ToString();
        blueCoinText.text = GameManager.Instance.playerData.blueCoin.ToString();
    }

}