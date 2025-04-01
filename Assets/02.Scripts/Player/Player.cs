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
        GameManager.Instance.player = this;
        if(healthBar != null )
        {
            healthBar.SetTarget( this );
        }

    }


    public override void TakeDamage(int damage,bool isCrit = false) 
    {
        if (curHp > 0)
        {
            //enemyUI.ShowDamageUI(damage);
            curHp -= damage;
            healthBar.OnHit();
            GameManager.Instance.DamageEffect(damage, isCrit, this.transform);


            if (curHp <= 0)
            {
                Dead();
            }
        }
    }

    public override void Dead() 
    {
        //죽음 애니메이션
        UIManager.Instance.errorPopup.ShowErrorMessage("5초 뒤 부활합니다.");
        healthBar.Revive(5f);
        curHp = maxHp;
    }
}
