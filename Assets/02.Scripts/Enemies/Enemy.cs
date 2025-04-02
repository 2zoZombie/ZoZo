using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;



public class Enemy : Entity
{

    public EnemyStatsTable enemyStatsTable;
    public EnemyType enemyType;
    public int enemyIndex;
    protected EnemyStats currentEnemyStats;
    protected EnemyManager enemyManager;
    protected float damage;

    [SerializeField] protected float attack;

    protected float moveSpeed = 5f;

    protected Rigidbody2D rb;
    protected Animator animator;

    protected PlayerData playerData;

    protected HealthBar healthBar;
    protected float positionx;


    protected void Awake()
    {
        switch(enemyType)
        {
            case EnemyType.Normal:
                currentEnemyStats = enemyStatsTable.enemyStatsList[enemyIndex];
                break;
            case EnemyType.Boss:
                currentEnemyStats = enemyStatsTable.bossStatsList[enemyIndex];
                break;
            case EnemyType.Treasure:
                currentEnemyStats = enemyStatsTable.treasureList[enemyIndex];
                break;
        }
       

        playerData = GameManager.Instance.playerData;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyManager = GetComponent<EnemyManager>();

        SetStats();
    }

    protected virtual void Start()
    {
        positionx = UnityEngine.Random.Range(1.0f, 2.4f);
        healthBar = UIManager.Instance.healthBarPool.GetFromPool(this.transform);
        healthBar.SetTarget(this as Entity);
        StartCoroutine(CoroutineAttck());
    }

    protected void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < positionx)
        {
            moveSpeed = 0;
        }
    }

    public override void TakeDamage(int damage, bool isCrit)//대미지값 int로 수정함
    {
        if (curHp > 0)
        {
            curHp -= damage;
            animator.SetTrigger("OnDamaged");
            healthBar?.OnHit();
            int rand = UnityEngine.Random.Range(0, 100);
            if(rand > 90) GameManager.Instance.dropItemPool.GetFromPool(GameManager.Instance.dropItemPool.prefabs[2], this.transform).PlayBounce(this.transform);
            GameManager.Instance.DamageEffect(damage, isCrit, this.transform);
            if (curHp <= 0)
            {
                StopCoroutine(CoroutineAttck());
                Dead();
                return;
            }
        }
    }

    protected IEnumerator CoroutineAttck()
    {
        while (curHp > 0)
        {
            int attacksec = UnityEngine.Random.Range(5, 10);
            yield return new WaitForSeconds(attacksec);
            if (curHp>0)
            {
                animator.SetTrigger("OnAttack");
                GameManager.Instance.player.TakeDamage(Mathf.RoundToInt(damage));
            }
        }
    }

    public override void Dead()
    {
        EnemyManager.Instance.RemoveEnemy(this);
        animator.SetBool("IsDead", true);
        DropItem();
        Destroy(gameObject, 3f);
    }

    public override void DropItem()
    {
        int quantity = Mathf.CeilToInt(currentEnemyStats.dropQuantity * (1 + 0.5f * GameManager.Instance.playerData.goldBonusLevel));
        StartCoroutine(CoroutineDropItem(quantity));
    }

    IEnumerator CoroutineDropItem(int quantity)
    {
        float interval = 1f / quantity;
        WaitForSeconds wait = new WaitForSeconds(interval);
        for (int i = 0; i < quantity; i++)
        {
            int index = enemyType != EnemyType.Treasure ? 0 : 1;
            DropItem drops = GameManager.Instance.dropItemPool.GetFromPool(GameManager.Instance.dropItemPool.prefabs[index], this.transform);
            drops.PlayBounce(this.transform);
            yield return wait;
        }
    }
    public void SetStats()
    {
        entityName = currentEnemyStats.enemyName;
        maxHp = StatCalculator(currentEnemyStats.maxHp, currentEnemyStats.growthHP);
        curHp = maxHp;
        damage = StatCalculator(currentEnemyStats.attackDamage, currentEnemyStats.growthDamage);
    }

    protected float StatCalculator(float baseStat, float growthStat)
    {
        return baseStat + growthStat * ((GameManager.Instance.playerData != null) ? GameManager.Instance.playerData.currentChapter - 1 : 0);
    }

}
