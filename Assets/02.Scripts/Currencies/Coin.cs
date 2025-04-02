using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : DropItem
{
    public override void StartIdle()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.DOColor(Color.yellow, 0.7f)
           .SetLoops(-1, LoopType.Yoyo)
           .SetEase(Ease.InOutSine)
           .SetId("Blink");
    }

    protected override void GetItem()
    {
        DOTween.Kill("Blink");
        GameManager.Instance.GetCoin(1);
    }
}
