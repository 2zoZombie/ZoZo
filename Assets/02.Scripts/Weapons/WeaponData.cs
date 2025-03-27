using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/Weapon")]
public class WeaponData : ScriptableObject
{
    //���� ����
    [Header("Info")]
    public int weaponLevel;               //���� ����
    public string weaponName;             //���� �̸�
    public int attackValum;               //���� ���ݷ�
    [Range(0f,100f)]                      //ġ��Ÿ Ȯ�� ����
    public float criticalChance;          //ġ��Ÿ Ȯ�� %����
    [Range(0f,600f)]                        //ġ��Ÿ ���� ����
    public float criticalMultiplier = 100f; //ġ��Ÿ ���� �⺻ 100%

    //���� ���׷��̵� ����
    [Header("UpgradeInfo")]
    public int upgradeNumber;       //��ȭ Ƚ��
    public int attackValum_Up;      //���ݷ� ��ȭ ��ġ
    public float criticalChance_Up; //ġ��Ÿ Ȯ�� ��ȭ ��ġ
    public float criticalPower_Up;  //ġ��Ÿ ���� ��ȭ ��ġ
    public int upgradeCost;         //���׷��̵� ���
    public int upgradeCost_UP;      //���׷��̵� ��� ��� ��ġ

}
