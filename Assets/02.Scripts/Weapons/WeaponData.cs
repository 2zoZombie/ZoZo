using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class WeaponData : ScriptableObject
{
    //무기 정보
    [Header("Info")]
    public int weaponLevel;               //무기 레벨
    public string weaponName;             //무기 이름
    public int attackValum;               //무기 공격력
    [Range(0f,100f)]                      //치명타 확률 범위
    public float criticalChance;          //치명타 확률 %단위
    [Range(0f,600f)]                        //치명타 배율 범위
    public float criticalMultiplier = 100f; //치명타 배율 기본 100%

    //무기 업그레이드 정보
    [Header("UpgradeInfo")]
    public int upgradeNumber;       //강화 횟수
    public int attackValum_Up;      //공격력 강화 수치
    public float criticalChance_Up; //치명타 확률 강화 수치
    public float criticalPower_Up;  //치명타 배율 강화 수치
    public int upgradeCost;         //업그레이드 비용
    public int upgradeCost_UP;      //업그레이드 비용 상승 수치

}
