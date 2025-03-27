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
    public float baseCriticalChance = 5f;       //치명타 확률 %단위
    public int upgradeCost;                     //업그레이드 비용

    //무기 업그레이드 정보
    [Header("UpgradeInfo")]
    public int attackValum_Up;       //공격력 강화 수치
    public int upgradeCost_UP;       //업그레이드 비용 상승 수치 (곱셈)

    //무기 아이콘
    [Header("Icon")]
    public Sprite weaponIcon;
}
