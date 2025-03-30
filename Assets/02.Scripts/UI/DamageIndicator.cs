using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public CanvasGroup canvasGroup;
    public float floatUpSpeed = 50f;
    public float duration = 1f;
    public Vector3 moveOffset = new Vector3(0, 100, 0);

    private Vector3 startPos;
    private float timer;

    public void Show(int damage, bool isCrit)
    {
        damageText.text = damage.ToString();
        damageText.fontSize = isCrit ? 48 : 32;
        damageText.color = isCrit ? Color.yellow : Color.white;
        canvasGroup.alpha = 1;
        timer = 0f;

        startPos = transform.localPosition;

        // DOTween으로 위로 올라가면서 페이드
        transform.DOLocalMove(startPos + moveOffset, duration).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(0, duration).SetEase(Ease.InSine).OnComplete(() =>
        {
            GameManager.Instance.damageIndicatorPool.ReturnToPool(this.gameObject);
        });
    }
}