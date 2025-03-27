using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: 플레이어 데이터 클래스 보고 수정될 수 있음
public enum StatType
{
    CriticalDamage,          //치명타 데미지
    GoldGainRate,            //골드 획득량
    AutoAttackInterval       //자동 공격 간격 (짧아져야 이득)
}

[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
public class SkillSO : ScriptableObject
{
    public StatType type;            //올려줄 스탯 타입
                                     //증가, 감소 타입으로 받아도 좋을듯

    public string statName;          //능력치 이름 (ex - 치명타 데미지)
    public int maxLevel;             //최대 레벨   (ex - 10)
    public int basicPrice;           //기본 가격
        
    public int impressionPrice;      //인상 가격 폭 (n배로 증가)
    public int impressionStat;       //능력치 인상 폭 (n만큼 증가)

    public Sprite Icon;               //띄워줄 이미지 정보
    public string description;       //띄워줄 스킬 정보
                                     //(ex - $"{statName} +{플레이어 스킬 레벨 변수 * impressionStat}"
}
