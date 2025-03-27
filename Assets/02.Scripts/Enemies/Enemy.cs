using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public EnemyStatsTable enemyStatsTable;
    public int enemyIndex;

    public float maxHp;
    public float curHp;
    public float moveSpeed;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private void Start()
    {

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
        moveSpeed = currentEnemyStats.moveSpeed;

        Debug.Log(currentEnemyStats.enemyName + "ÀÇ Ã¼·Â: " + currentEnemyStats.maxHp);

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        TakeDamage(20);
    }

    private void Move()
    {
        rigidbody.velocity = Vector3.left * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage * Time.deltaTime;
        if (curHp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        animator.SetBool("Dead", true);
        Destroy(gameObject, 3f);
    }
}
