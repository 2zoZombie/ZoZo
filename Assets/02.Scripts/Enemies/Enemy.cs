using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;



public class Enemy : Entity
{

    public EnemyStatsTable enemyStatsTable;
    public int enemyIndex;
    EnemyStats currentEnemyStats;

    float damage;

    [SerializeField] private float attack;

    private int moveSpeed = 40;

    private Rigidbody2D rigidbody;
    private Animator animator;

    PlayerData playerData;

    HealthBar healthBar;
    private float positionx;


    private void Awake()
    {
        currentEnemyStats = enemyStatsTable.enemyStatsList[enemyIndex];

        playerData = GameManager.Instance.playerData;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        positionx = Random.Range(1.5f, 2.4f);

        SetStats();
    }

    private void Start()
    {
        healthBar = UIManager.Instance.healthBarPool.GetFromPool(this.transform);
        healthBar.SetTarget(this as Entity);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(20, false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            TakeDamage(20, true);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.velocity = Vector3.left * moveSpeed * Time.deltaTime;

        if (gameObject.transform.position.x < positionx)
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
            if (curHp <= 0)
            {
                Dead();
                return;
            }
            else
            {
                Attack();
            }
            healthBar.OnHit();
            GameManager.Instance.DamageEffect(damage, isCrit, this.transform);
        }
    }

    public void Attack()
    {
        Debug.Log(playerData.curHp);
        playerData.curHp -= currentEnemyStats.attackDamage;
        Debug.Log( "몬스터 힘 " + currentEnemyStats.attackDamage);
        Debug.Log( "플레이어" + playerData.curHp);
        animator.SetTrigger("OnAttack");
        //플레이어 hp를 받아와 데미지 
    }
    public override void Dead()
    {
        animator.SetBool("IsDead" ,true);
        EnemyManager.Instance.curspawncout--;
        Destroy(gameObject, 3f);
        DropItem();
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
            DropItem drops = GameManager.Instance.dropItemPool.GetFromPool(GameManager.Instance.dropItemPool.prefabs[0], this.transform);
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

    float StatCalculator(float baseStat, float growthStat)
    {
        return baseStat + growthStat * ((GameManager.Instance.playerData != null) ? GameManager.Instance.playerData.currentChapter - 1 : 0);
    }
}
