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

    private int moveSpeed = 15;

    private Rigidbody2D rigidbody;
    private Animator animator;

    EnemyManager enemyManager;
    StageUI stageUI;
    HealthBar healthBar;
    private float positionx;


    private void Awake()
    {
        currentEnemyStats = enemyStatsTable.enemyStatsList[enemyIndex];
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity, EnemyLayer))
        //    {
        //        if (hit.collider.gameObject == gameObject)
        //        {
        //            TakeDamage(20f);
        //        }
        //    }
        //}

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
        animator.SetBool("Dead", true);
        DropItem();
        EnemyManager.Instance.spawncount--;
        Destroy(gameObject, 3f);
    }

    public override void DropItem()
    {

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


    public void BossGrowthStats()
    {
        EnemyStats bossStats = enemyStatsTable.enemyStatsList[3];

        maxHp -= bossStats.growthHP;
        damage -= bossStats.growthDamage;
    }


}
