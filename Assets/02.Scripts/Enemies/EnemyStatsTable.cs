using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Zombie Data", menuName = "Scriptable Object/Zombie Data", order = int.MaxValue)]
public class EnemyStatsTable : ScriptableObject
{
    public List<EnemyStats> enemyStatsList;

}
