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
        //���ҽ� ������ ���� �������� ����
        Debug.Log("���濡 ���⼼�� �Ϸ�");
    }

    public void Refresh()
    {

    }


}
