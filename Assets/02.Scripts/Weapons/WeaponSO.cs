using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class WeaponSO : ScriptableObject
{
    //무기 정보
    [Header("Info")]
    public int weaponID;                        //무기 ID
    public string weaponName;                   //무기 이름
    public int baseAttack;                      //무기 기본 공격력

    [Range(0f,100f)]                            //치명타 확률 범위
    public float baseCriticalChance = 3f;       //치명타 확률 %단위

    [Range(0f,600f)]                            //치명타 배율 범위
    public float baseCriticalMultiplier = 100f; //치명타 배율 기본 100%

    //무기 업그레이드 정보
    [Header("UpgradeInfo")]
    public int attackValum_Up = 5;       //공격력 강화 수치
    public float criticalChance_Up = 1;  //치명타 확률 강화 수치
    public float criticalPower_Up = 5;   //치명타 배율 강화 수치
    public int upgradeCost = 15;         //업그레이드 비용
    public int upgradeCost_UP = 2;       //업그레이드 비용 상승 수치 (곱셈)
}
