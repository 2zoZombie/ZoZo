using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public WeaponSO Weapon;              //무기 오브젝트
    public int WeaponID;                 //무기 ID
    public string WeaponName;
    public int WeaponLevel;
    public bool WeaponEquip;

    //무기 업그레이드 정보
    [Header("UpgradeInfo")]
    public int attackValum_Up = 5;       //공격력 강화 수치
    public float criticalChance_Up = 1;  //치명타 확률 강화 수치
    public float criticalPower_Up = 5;   //치명타 배율 강화 수치
    public int upgradeCost = 15;         //업그레이드 비용
    public int upgradeCost_UP = 2;       //업그레이드 비용 상승 수치 (곱셈)


    public WeaponData(WeaponSO weapon, int weaponID, string weaponName)
    {
        WeaponName = weaponName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
