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
    public float baseCriticalChance = 5f;       //ġ��Ÿ Ȯ�� %����
    public int upgradeCost;                     //���׷��̵� ���

    //���� ���׷��̵� ����
    [Header("UpgradeInfo")]
    public int attackValum_Up;       //���ݷ� ��ȭ ��ġ
    public int upgradeCost_UP;       //���׷��̵� ��� ��� ��ġ (����)

    //���� ������
    [Header("Icon")]
    public Sprite weaponIcon;
}
