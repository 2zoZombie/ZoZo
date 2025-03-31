using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{

    //무기 데이터 리스트
    public Dictionary<int, WeaponSO> WeaponList = new Dictionary<int, WeaponSO>();


    protected override void Awake()
    {
        base.Awake();
        WeaponDataLoad();
    }

    public void Start()
    {
        
    }

    public void WeaponDataLoad()
    {
        //리소스 폴더의 무기 정보들을 저장
        WeaponList.Add(0, Resources.Load<WeaponSO>("ScriptableObject/W_WoodShovel"));
        WeaponList.Add(1, Resources.Load<WeaponSO>("ScriptableObject/W_HarvestShovel"));
        WeaponList.Add(2, Resources.Load<WeaponSO>("ScriptableObject/W_LargeSickle"));
        WeaponList.Add(3, Resources.Load<WeaponSO>("ScriptableObject/W_Shotgun"));
        WeaponList.Add(4, Resources.Load<WeaponSO>("ScriptableObject/W_MachineGun"));
        Debug.Log("메니저에 무기정보세팅 완료");
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
