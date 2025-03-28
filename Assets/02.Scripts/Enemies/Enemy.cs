using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI DamageText;

    public EnemyStatsTable enemyStatsTable;
    public int enemyIndex;

    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;


    private int moveSpeed = 15;

    private Rigidbody2D rigidbody;
    private Animator animator;

    private float positionx;
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

        Debug.Log(currentEnemyStats.enemyName + "ÀÇ Ã¼·Â: " + currentEnemyStats.maxHp);

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        positionx = Random.Range(1.5f, 2.7f);
    }

    private void FixedUpdate()
    {
        Move();
        TakeDamage(20);
    }

    private void Move()
    {
       rigidbody.velocity = Vector3.left * moveSpeed * Time.deltaTime;


        if (gameObject.transform.position.x < positionx)
        {
            moveSpeed = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage * Time.deltaTime;
        //ShowDamageUI(damage);
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

    //private void ShowDamageUI(int damage)
    //{
    //    TextMeshProUGUI damageText = Instantiate(DamageText , transform.position , Quaternion.identity);
    //    damageText.text = damage.ToString();
    //    Destroy(damageText.gameObject, 3f);
    //}
}
