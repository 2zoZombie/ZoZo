using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    WeaponManager weaponManager;
    public Dictionary<int, WeaponData> WeaponDataList = new Dictionary<int, WeaponData>();

    private void Awake()
    {
        weaponManager = WeaponManager.Instance;

    }

    public void Start()
    {

    }

    public void WeaponDataSet()
    {
        //리소스 폴더의 무기 정보들을 저장
        Debug.Log("가방에 무기세팅 완료");
    }

    public void Refresh()
    {

    }


}
