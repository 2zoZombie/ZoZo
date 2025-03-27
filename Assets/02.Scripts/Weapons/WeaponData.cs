using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public int WeaponLevel;
    public string WeaponName;
    public int AttackValum;
    public int CriticalProbability;

    [Header("Upgrade")]
    public int ATK_ValumUp;
    public int CRI_ProbabilityUp;

}
