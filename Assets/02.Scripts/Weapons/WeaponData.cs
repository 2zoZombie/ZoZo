using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public WeaponSO weaponSO;              //무기 오브젝트
    public int weaponLevel = 0;
    public bool isEquip = false;
    public bool isPurchased = false;


    public WeaponData(WeaponSO weapon)
    {
        weaponSO = weapon;
    }
}
