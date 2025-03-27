using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: �÷��̾� ������ Ŭ���� ���� ������ �� ����
public enum StatType
{
    CriticalDamage,          //ġ��Ÿ ������
    GoldGainRate,            //��� ȹ�淮
    AutoAttackInterval       //�ڵ� ���� ���� (ª������ �̵�)
}

[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
public class SkillSO : ScriptableObject
{
    public StatType type;            //�÷��� ���� Ÿ��
                                     //����, ���� Ÿ������ �޾Ƶ� ������

    public string statName;          //�ɷ�ġ �̸� (ex - ġ��Ÿ ������)
    public int maxLevel;             //�ִ� ����   (ex - 10)
    public int basicPrice;           //�⺻ ����
        
    public int impressionPrice;      //�λ� ���� �� (n��� ����)
    public int impressionStat;       //�ɷ�ġ �λ� �� (n��ŭ ����)

    public Sprite Icon;               //����� �̹��� ����
    public string description;       //����� ��ų ����
                                     //(ex - $"{statName} +{�÷��̾� ��ų ���� ���� * impressionStat}"
}
