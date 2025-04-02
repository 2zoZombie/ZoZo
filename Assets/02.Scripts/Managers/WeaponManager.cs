using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    //장착아이템

    public WeaponData selectedWeapon;
    public EquipWeaponInfo equipWeaponInfo;


    //무기 데이터 리스트
    public List<WeaponSO> weaponSOList = new List<WeaponSO>();
    public List<WeaponData> weaponDatas = new List<WeaponData>();

    protected override void Awake()
    {
        base.Awake();
        //WeaponSOLoad();

    }

    public void Start()
    {
        //NewWeaponData();
        //StageManager.Instance.onChapterStart.AddListener(WeaponDataToPlayerData);
    }

    //public void WeaponSOLoad()
    //{
    //    //리소스 폴더의 무기 정보들을 저장
    //    weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_WoodShovel"));
    //    weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_HarvestShovel"));
    //    weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_LargeSickle"));
    //    weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_Shotgun"));
    //    weaponSOList.Add(Resources.Load<WeaponSO>("ScriptableObject/W_MachineGun"));
    //    Debug.Log("메니저에 무기정보세팅 완료");
    //}

    public void NewWeaponData()//newgame에서 호출
    {
        for (int i = 0; i < weaponSOList.Count; i++)
        {
            weaponDatas.Add(new WeaponData(weaponSOList[i]));
        }
        
        weaponDatas[0].isPurchased = true;
        weaponDatas[0].isEquip = true;
        equipWeaponInfo.SetEquipData(weaponDatas[0]);
        WeaponDataToPlayerData();
    }

    public void LoadWeaponData()//loadgame에서 호출
    {
        if(GameManager.Instance.playerData ==null) return;

        weaponDatas = GameManager.Instance.playerData.weaponData;

        foreach(var weaponData in weaponDatas)
        {
            if(weaponData.isEquip == true)
            {
                equipWeaponInfo.SetEquipData(weaponData);
            }
        }
    }

    public void WeaponDataToPlayerData()
    {
        GameManager.Instance.playerData.weaponData = weaponDatas;
    }
}
