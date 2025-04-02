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

    public WeaponSwap weaponSwap;
    public PlayerData playerData;
    public HealthBar healthBar;

    public Animator playerAnim;

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

    private void OnValidate()
    {
        weaponSwap = GetComponentInChildren<WeaponSwap>();
    }

    // healthBar 연결
    private void Awake()
    {
        entityName = "Player";
        playerState = StateType.Move;

        GameManager.Instance.player = this;
        
    }

    private void Start()
    {
        playerData = GameManager.Instance.playerData;
        healthBar = UIManager.Instance.healthBarPool.playerHealthBar;
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.SetTarget(this as Entity);
        }
    }
    public override void TakeDamage(int damage, bool isCrit = false)
    {
        if (playerData.curHp > 0)
        {
            //enemyUI.ShowDamageUI(damage);
            playerData.curHp -= damage;
            healthBar.OnHit();
            GameManager.Instance.DamageEffect(damage, isCrit, this.transform);

            //Damaged 애니메이션
            PlayerState = StateType.Damaged;

            if (playerData.curHp <= 0)
            {
                Dead();
            }
        }
    }

    public override void Dead()
    {
        if (playerState == StateType.Dead) return;
        //Dead 애니메이션
        PlayerState = StateType.Dead;

        UIManager.Instance.errorPopup.ShowErrorMessage("5초 뒤 부활합니다.");
        healthBar.Revive(5f);
        Invoke("Revive", 5f);
    }

    public void Revive()
    {
        playerData.curHp = playerData.maxHp;
        PlayerState = StateType.Idle;
    }

    private void ChangeState()
    {
        switch (PlayerState)
        {
            case StateType.Idle:
                playerAnim.SetBool("IsMove", false);
                playerAnim.SetBool("IsDead", false);
                weaponSwap.weaponAnim.SetBool("IsMove", false);
                weaponSwap.weaponAnim.SetBool("IsDead", false);
                break;

            case StateType.Move:
                playerAnim.SetBool("IsMove", true);
                weaponSwap.weaponAnim.SetBool("IsMove", true);
                break;

            case StateType.Attack:
                playerAnim.SetTrigger("OnAttack");
                weaponSwap.weaponAnim.SetTrigger("OnAttack");
                break;

            case StateType.Damaged:
                playerAnim.SetTrigger("OnDamaged");
                weaponSwap.weaponAnim.SetTrigger("OnDamaged");

                break;

            case StateType.Dead:
                playerAnim.SetBool("IsDead", true);
                weaponSwap.weaponAnim.SetBool("IsDead", true);
                break;
        }
    }


    public void Test()
    {
        playerAnim.SetTrigger("OnDamaged");
        weaponSwap.weaponAnim.SetTrigger("OnDamaged");
    }
}
