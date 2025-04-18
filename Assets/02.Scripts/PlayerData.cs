﻿using System;
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
    public List<StageData> stageDatas;
    public ChapterInfo currentChapterInfo;

    [Header("Currency")]
    public float coin = 0;
    public float blueCoin = 0;

    [Header("HP")]
    public float maxHp = 1000;
    public float curHp = 1000;

    [Header("Upgrades")]
    public int autoAttackLevel = 0;
    public int critDamageLevel = 0;
    public int goldBonusLevel = 0;

    [Header("WeaponData")]
    public List<WeaponData> weaponData;
}
