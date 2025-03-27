using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponData
{
    public WeaponSO Weapon;              //���� ������Ʈ
    public int WeaponID;                 //���� ID
    public string WeaponName;
    public int WeaponLevel;
    public bool WeaponEquip;


    public WeaponData(WeaponSO weapon, int weaponID, string weaponName, int weaponLevel, bool weaponEquip)
    {
        Weapon = weapon;
        WeaponID = weaponID;
        WeaponName = weaponName;
        WeaponLevel = weaponLevel;
        WeaponEquip = weaponEquip;
    }
}
