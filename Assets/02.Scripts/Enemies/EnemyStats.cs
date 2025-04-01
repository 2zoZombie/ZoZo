using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyStats
{
    public string enemyName;
    public float maxHp;
    public float growthHP;
    public float attackDamage;
    public float growthDamage;
    public EnemyType enemyType;
    public int dropQuantity;
    public GameObject prefab;
}
public enum EnemyType
{
    Normal,
    Boss
}