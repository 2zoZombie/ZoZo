using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : HealthBar
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.OnHealthChange += OnHit;
    }
    public override void UpdateHP()
    {
        if (target == null) return;
        //entity와 연결 후 current hp max hp 받아오기
        targetFill = GameManager.Instance.playerData.curHp / GameManager.Instance.playerData.maxHp;
    }

    protected override void Die()
    {
        Revive(5f);
    }
}
