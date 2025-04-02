using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatIndex
{
    CriticalDamage,          //ġ��Ÿ ������
    GoldGainRate,            //��� ȹ�淮
    AutoAttackInterval       //�ڵ� ���� ����
}

//ScriptableObject�� ���� �� ������ ���� �� �ֵ���
//���� ���� �޴�â�� �߰����ִ� ��Ʈ����Ʈ
[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
public class SkillSO : ScriptableObject
{
    public StatIndex index;           //�÷��� ���� �ε���

    public string skillName;          //�ɷ�ġ �̸� (ex - ġ��Ÿ)
    public string skillDescription;   //�Ʒ� ��µ� ���� ���� Ǯ����? (ex - ġ��Ÿ ������)

    public int maxLevel;              //�ִ� ���� (ex - 10)
    public int basicPrice;            //�⺻ ����
        
    public int impressionPrice;       //�λ� ���� �� (n��� ����)
    public int impressionStat;        //�ɷ�ġ �λ� �� (n��ŭ ����)

    public Sprite Icon;               //����� �̹��� ����
}
