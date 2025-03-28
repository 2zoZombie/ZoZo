using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public SkillSO critDamage;
    public SkillSO coinMultiplier;
    public SkillSO autoAttack;

    private void Awake()
    {
        GameManager.Instance.playerStat = this;
    }
}
