using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatIndex
{
    CriticalDamage,          //치명타 데미지
    GoldGainRate,            //골드 획득량
    AutoAttackInterval       //자동 공격 간격
}

//ScriptableObject를 만들 때 빠르게 만들 수 있도록
//에셋 생성 메뉴창에 추가해주는 어트리뷰트
[CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
public class SkillSO : ScriptableObject
{
    public StatIndex index;           //올려줄 스탯 인덱스

    public string skillName;          //능력치 이름 (ex - 치명타)
    public string skillDescription;   //아래 출력될 설명 내지 풀네임? (ex - 치명타 데미지)

    public int maxLevel;              //최대 레벨 (ex - 10)
    public int basicPrice;            //기본 가격
        
    public int impressionPrice;       //인상 가격 폭 (n배로 증가)
    public int impressionStat;        //능력치 인상 폭 (n만큼 증가)

    public Sprite Icon;               //띄워줄 이미지 정보
}
