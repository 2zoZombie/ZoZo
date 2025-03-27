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

    //���� ���׷��̵� ����
    [Header("UpgradeInfo")]
    public int attackValum_Up = 5;       //���ݷ� ��ȭ ��ġ
    public float criticalChance_Up = 1;  //ġ��Ÿ Ȯ�� ��ȭ ��ġ
    public float criticalPower_Up = 5;   //ġ��Ÿ ���� ��ȭ ��ġ
    public int upgradeCost = 15;         //���׷��̵� ���
    public int upgradeCost_UP = 2;       //���׷��̵� ��� ��� ��ġ (����)


    public WeaponData(WeaponSO weapon, int weaponID, string weaponName)
    {
        WeaponName = weaponName;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
