using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public SkillSO critDamage;
    public SkillSO coinMultiplier;
    public SkillSO autoAttack;

    public HealthBar healthBar;

    private void Awake()
    {
        GameManager.Instance.playerStat = this;
        healthBar = UIManager.Instance.healthBarPool.playerHPbar;
    }


    public virtual void TakeDamage(int damage, bool isCrit) { }

    public virtual void Dead() { }
}
