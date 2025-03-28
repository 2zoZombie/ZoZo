using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class Enemy : MonoBehaviour
{

    public EnemyStatsTable enemyStatsTable;
    public int enemyIndex;

    float damage;

    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private float attack;

    private int moveSpeed = 15;

    private Rigidbody2D rigidbody;
    private Animator animator;

    EnemyManager enemyManager;
    StageUI stageUI;

    private float positionx;


    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        EnemyStats currentEnemyStats = enemyStatsTable.enemyStatsList[enemyIndex];
        switch (currentEnemyStats.enemyType)
        {
            case EnemyType.Normal:
                break;
            case EnemyType.Boss:
                break;
        }


        maxHp = currentEnemyStats.maxHp;
        curHp = maxHp;
        attack = currentEnemyStats.attackDamage;

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        positionx = Random.Range(1.5f, 2.7f);

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
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(20);
        }
    }

    private void Move()
    {
       rigidbody.velocity = Vector3.left * moveSpeed * Time.deltaTime;

        if (gameObject.transform.position.x < positionx)
        {
            moveSpeed = 0;
        }

    }

    private void TakeDamage(float damage)
    {

        if (curHp > 0)
        {
            //enemyUI.ShowDamageUI(damage);
            curHp -= damage;
            if (curHp <= 0)
            {
                Dead();
            }
        }
    }

    private void Dead()
    {
        animator.SetBool("Dead", true);
        enemyManager.spawncount--;
        Destroy(gameObject, 3f);
    }

    public void GrowthStats()
    {
        for (int i = 0; i < enemyStatsTable.enemyStatsList.Count - 1; i++)
        {
            EnemyStats currentEnemyStats = enemyStatsTable.enemyStatsList[i];

            maxHp += currentEnemyStats.growthHP;
            damage += currentEnemyStats.growthDamage;
        }
    }
    public void BossGrowthStats()
    {
        EnemyStats bossStats = enemyStatsTable.enemyStatsList[3];

        maxHp -= bossStats.growthHP;
        damage -= bossStats.growthDamage;
    }
}
