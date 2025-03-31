using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{

    
    public Dictionary<int, WeaponSO> WeaponList = new Dictionary<int, WeaponSO>();

    WeaponData weaponData;


    protected override void Awake()
    {
        base.Awake();
        WeaponDataSet();
    }

    public void Start()
    {
        weaponData = new WeaponData(WeaponList[0],0);
    }

    public void WeaponDataSet()
    {
        WeaponList.Add(0, Resources.Load<WeaponSO>("ScriptableObject/W_WoodShovel"));
        WeaponList.Add(1, Resources.Load<WeaponSO>("ScriptableObject/W_HarvestShovel"));
        WeaponList.Add(2, Resources.Load<WeaponSO>("ScriptableObject/W_LargeSickle"));
        WeaponList.Add(3, Resources.Load<WeaponSO>("ScriptableObject/W_Shotgun"));
        WeaponList.Add(4, Resources.Load<WeaponSO>("ScriptableObject/W_MachineGun"));
        Debug.Log("무기정보세팅 완료");
    }

    public void PurchaseWeapon()
    {

    }

    public void EnhanceWeapon()
    {

    }

    public void EquipWeapon()
    {

    }
}
