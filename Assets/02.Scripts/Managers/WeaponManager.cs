using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    //장착아이템
    public WeaponData weaponInfo;


    //무기 데이터 리스트
    public List<WeaponSO> weaponSOList = new List<WeaponSO>();
    public List<WeaponData> weaponDatas = new List<WeaponData>();

    protected override void Awake()
    {
        base.Awake();
        WeaponSOLoad();
        NewWeaponData();
    }

    public void Start()
    {
        
    }

    public void WeaponSOLoad()
    {
        //리소스 폴더의 무기 정보들을 저장
        weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_WoodShovel"));
        weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_HarvestShovel"));
        weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_LargeSickle"));
        weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_Shotgun"));
        weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_MachineGun"));
        Debug.Log("메니저에 무기정보세팅 완료");
    }

    public void NewWeaponData()//newgame에서 호출
    {
        for (int i = 0; i < weaponSOList.Count; i++)
        {
            weaponDatas.Add(new WeaponData(weaponSOList[i]));
        }
        weaponDatas[0].isPurchased = true;
        weaponDatas[0].isEquip = true;
    }

    public void LoadWeaponData()//loadgame에서 호출
    {
        if(GameManager.Instance.playerData ==null) return;

        weaponDatas = GameManager.Instance.playerData.weaponData;
    }

    public void PurchaseWeapon()
    {

    }

    public void EnhanceWeapon()
    {

    }

    public void EquipWeapon()
    {
        int i;
        //기존의 장착된 무기 해제
        for (i = 0; i < weaponSOList.Count; i++)
        {
            weaponDatas[i].isEquip = false;
        }
        //현재 누른 무기 장착
        weaponDatas[weaponInfo.weaponSO.weaponID].isEquip = true;
    }
}
