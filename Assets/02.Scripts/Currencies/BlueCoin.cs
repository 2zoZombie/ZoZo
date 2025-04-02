using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCoin : DropItem
{
    public override void StartIdle()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.DOColor(Color.white, 0.7f)
           .SetLoops(-1, LoopType.Yoyo)
           .SetEase(Ease.InOutSine)
           .SetId("Blink");
    }

    protected override void GetItem()
    {
        DOTween.Kill("Blink");
        GameManager.Instance.GetBlueCoin(1);
    }
}
