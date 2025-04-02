using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IAttackable
{
    public string entityName;
    public float maxHp;
    public float curHp;

    public virtual void TakeDamage(int damage, bool isCrit) { }

    public virtual void Dead() { }
    public virtual void DropItem() { }
}
