using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public WeaponSO Weapon;              //���� ������Ʈ
    public int WeaponLevel;
    public bool WeaponEquip = false;
    public bool isPurchased = false;


    public WeaponData(WeaponSO weapon, int weaponLevel)
    {
        Weapon = weapon;
        WeaponLevel = weaponLevel;
    }
}
