using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class WeaponSO : ScriptableObject
{
    //���� ����
    [Header("Info")]
    public int weaponID;                        //���� ID
    public string weaponName;                   //���� �̸�
    public int baseAttack;                      //���� �⺻ ���ݷ�

    [Range(0f,100f)]                            //ġ��Ÿ Ȯ�� ����
    public float baseCriticalChance = 3f;       //ġ��Ÿ Ȯ�� %����

    [Range(0f,600f)]                            //ġ��Ÿ ���� ����
    public float baseCriticalMultiplier = 100f; //ġ��Ÿ ���� �⺻ 100%

    //���� ���׷��̵� ����
    [Header("UpgradeInfo")]
    public int attackValum_Up = 5;       //���ݷ� ��ȭ ��ġ
    public float criticalChance_Up = 1;  //ġ��Ÿ Ȯ�� ��ȭ ��ġ
    public float criticalPower_Up = 5;   //ġ��Ÿ ���� ��ȭ ��ġ
    public int upgradeCost = 15;         //���׷��̵� ���
    public int upgradeCost_UP = 2;       //���׷��̵� ��� ��� ��ġ (����)
}
