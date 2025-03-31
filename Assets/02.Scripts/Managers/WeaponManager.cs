using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public List<WeaponSO> WeaponList = new List<WeaponSO>();

    WeaponData weaponData;


    protected override void Awake()
    {
        base.Awake();
        weaponData = new WeaponData(WeaponList[0],0);
    }

    public void Start()
    {
        
    }
}
