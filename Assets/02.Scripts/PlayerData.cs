using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [Header("Progress")]
    public int currentChapter = 1;
    public int currentStage = 1;
    public int defeatedEnemyCount = 0;

    [Header("Currency")]
    public float coin = 0;
    public float blueCoin = 0;

    [Header("Upgrades")]
    public int autoAttackLevel = 0;
    public int critDamageLevel = 0;
    public int goldBonusLevel = 0;

    //테스트 코드
    public int[] statLevel = new int[] { 1, 0, 0, 0 };

    //[Header("WeaponData")]
    public string equippedWeaponID;
}
