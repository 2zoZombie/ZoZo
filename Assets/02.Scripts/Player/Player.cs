using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Playables;

public enum StateType
{
    Idle,
    Move,
    Attack,
    Damaged,
    Dead
}

public class Player : Entity
{
    public SkillSO critDamage;
    public SkillSO coinMultiplier;
    public SkillSO autoAttack;

    public HealthBar healthBar;

    public Animator playerAnim;
    public Animator weaponAnim;

    private StateType playerState;
    public StateType PlayerState
    {
        get { return playerState; }
        set
        {
            playerState = value;
            //상태에 맞는 메서드 연결
            ChangeState();
        }
    }

    // healthBar 연결
    private void Awake()
    {
        playerState = StateType.Idle;

        GameManager.Instance.player = this;
        if(healthBar != null )
        {
            healthBar.SetTarget( this );
        }
    }

    public override void TakeDamage(int damage, bool isCrit = false) 
    {
        if (curHp > 0)
        {
            //enemyUI.ShowDamageUI(damage);
            curHp -= damage;
            healthBar.OnHit();
            GameManager.Instance.DamageEffect(damage, isCrit, this.transform);

            //Damaged 애니메이션
            playerState = StateType.Damaged;

            if (curHp <= 0)
            {
                Dead();
            }
        }
    }

    public override void Dead() 
    {
        //Dead 애니메이션
        playerState = StateType.Dead;

        UIManager.Instance.errorPopup.SetErrorText("5초 뒤 부활합니다.");
        Invoke("Revive", 5f);
    }

    public void Revive()
    {
        healthBar.Revive(5f);
        curHp = maxHp;
        PlayerState = StateType.Idle;
    }

    private void ChangeState()
    {
        switch(PlayerState)
        {
            case StateType.Idle:
                playerAnim.SetBool("IsMove", false);
                playerAnim.SetBool("IsDead", false);
                Debug.Log("Idle");
                break;

            case StateType.Move:
                playerAnim.SetBool("IsMove", true);
                Debug.Log("IsMove");
                break;

            case StateType.Attack:
                playerAnim.SetTrigger("OnAttack");
                Debug.Log("OnAttack");
                break;

            case StateType.Damaged:
                playerAnim.SetTrigger("OnDamaged");
                Debug.Log("OnDamaged");
                break;

            case StateType.Dead:
                playerAnim.SetBool("IsDead", true);
                Debug.Log("IsDead");
                break;
        }
    }

    public void Test(int state)
    {
        PlayerState = (StateType)state;
    }

}
