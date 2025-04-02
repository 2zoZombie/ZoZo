using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossEnemy : Enemy
{
    protected override void Start()
    {
        positionx = Random.Range(1.0f, 2.4f);
        healthBar = UIManager.Instance.healthBarPool.bossHealthBar;
        healthBar.gameObject.SetActive(true);
        healthBar.SetTarget(this as Entity);
        StartCoroutine(CoroutineAttck());
    }

    public override void Dead()
    {
        StopCoroutine(CoroutineAttck());
        EnemyManager.Instance.RemoveEnemy(this);
        animator.SetBool("IsDead", true);
        DropItem();
        //EnemyManager.Instance.curspawncout--;
        Destroy(gameObject, 3f);
    }
}
