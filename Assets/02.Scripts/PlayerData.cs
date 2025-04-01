using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [Header("Progress")]
    public int currentChapter = 1;
    public int currentStage = 0;
    public int defeatedEnemyCount = 0;
    public List<StageData> stageDatas = new List<StageData>();
    public ChapterInfo currentChapterInfo;

    [Header("Currency")]
    public float coin = 0;
    public float blueCoin = 0;

    [Header("HP")]
    public float maxHp;
    public float curHp;

    [Header("Upgrades")]
    public int autoAttackLevel = 0;
    public int critDamageLevel = 0;
    public int goldBonusLevel = 0;

    public int[] statLevel = new int[] { 0, 0, 0 };

    [Header("WeaponData")]
    public string equippedWeaponID;
    public List<WeaponData> weaponData = new List<WeaponData>();
}
