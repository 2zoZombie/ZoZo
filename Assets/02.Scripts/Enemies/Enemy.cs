using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHp;
    public float curHp;

    [SerializeField] private float moveSpeed;


    private Rigidbody2D rigidbody;
    private Animator animator;
    private void Start()
    {
        curHp = maxHp;
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        Move();
        TakeDamage(20);
    }

    private void Move()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
