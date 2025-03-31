using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public WeaponSO Weapon;              //무기 오브젝트
    public int WeaponLevel;
    public bool WeaponEquip = false;
    public bool isPurchased = false;


    public WeaponData(WeaponSO weapon, int weaponLevel)
    {
        Weapon = weapon;
        WeaponLevel = weaponLevel;
    }
}
