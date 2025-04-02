<div align="center">

# ZOZO
![ZoZoTitle](https://github.com/user-attachments/assets/fc71839d-b3cc-4fa5-9bd0-35c906f0a530)

**세상 밖으로 나온 좀비들을 다시 돌려보내자!**  
좀비 아포칼립스 세계에서 잃어버린 여동생을 찾기 위해 여행을 떠난 ZoZo의 기묘한 이야기.  
단순하지만 중독성 강한 2D 클리커 액션 게임입니다!

</div>
<br>

----

## 📌 프로젝트 개요
  
- **프로젝트**: ZoZo
- **개발환경**: Unity, C#  
- **개발인원**: 5

----
  
## 👥 팀원 소개 및 역할 분담

| 이름   | 역할                 |
|--------|----------------------|
| 황희돈 | 게임 매니저, 오브젝트 풀링 관리 |
| 최동호 | 무기 업그레이드 시스템 |
| 윤혜진 | 플레이어 능력치 및 업그레이드 시스템 |
| 박승규 | 적 및 스테이지 관리 |
| 예병권 | 클릭 이벤트 및 자동 공격 |

----

## 🎥 게임 시연 영상
[![게임 시연 영상](http://img.youtube.com/vi/GszJ58LgZjM/0.jpg)](https://youtu.be/GszJ58LgZjM)

----

## ⚙️ 핵심 코드 소개

### 🛠️ 황희돈 (오브젝트 풀링 및 오브젝트 관리)

- **Object Pooling**  
  - 대량의 오브젝트가 생성되는 특성상 오브젝트를 오브젝트풀로 관리하고 대부분의 움직임은 `Dotween`으로 제어하였습니다.
    ![image (12)](https://github.com/user-attachments/assets/5210593f-0438-495f-82a6-2ffb90ed4f5a)

  - 오브젝트풀은 제네릭으로 구성하였으며 캔버스상의 오브젝트의 경우 일부 오버라이드를 통해 재사용하였습니다.
    ![image (14)](https://github.com/user-attachments/assets/bbfd482d-0c02-4200-a651-3f8925bcfe73)

<br>

### ⚔️ 최동호 (무기 업그레이드 시스템)

- **WeaponManager**
  - 게임 시작 시 무기 데이터 초기화 및 로드 기능을 구현하였습니다.
  > ```cs
  > public class WeaponManager : Singleton<WeaponManager>
  > {
  >     //장착아이템
  > 		public WeaponData selectedWeapon;
  > 		public EquipWeaponInfo equipWeaponInfo;
  > 		
  > 		//무기 데이터 리스트
  > 		public List<WeaponSO> weaponSOList = new List<WeaponSO>();
  > 		public List<WeaponData> weaponDatas = new List<WeaponData>();
  > 		
  > 		//새로하기 데이터 세팅
  > 		public void NewWeaponData()
  > 		{
  > 		    for (int i = 0; i < weaponSOList.Count; i++)
  > 		    {
  > 		        weaponDatas.Add(new WeaponData(weaponSOList[i]));
  > 		    }
  > 		    
  > 		    weaponDatas[0].isPurchased = true;
  > 		    weaponDatas[0].isEquip = true;
  > 		    equipWeaponInfo.SetEquipData(weaponDatas[0]);
  > 		    WeaponDataToPlayerData();
  > 		}
  > 		
  > 		//이어하기 데이터 세팅
  > 		public void LoadWeaponData()
  > 		{
  > 		    if(GameManager.Instance.playerData ==null) return;
  > 		
  > 		    weaponDatas = GameManager.Instance.playerData.weaponData;
  > 		
  > 		    foreach(var weaponData in weaponDatas)
  > 		    {
  > 		        if(weaponData.isEquip == true)
  > 		        {
  > 		            equipWeaponInfo.SetEquipData(weaponData);
  > 		        }
  > 		    }
  > 	}
  > }
  >````
  
- **InvenPopup**
  - 무기 가방의 무기 리스트를 저장
  - 각 슬롯에 번호와 무기 정보를 부여하는 함수
  - 무기 가방 팝업을 띄우는 함수
  - 모든 슬롯을 최신화하는 함수
    
- **InvenSlot**
  - InvenPopup에서 받아온 무기 정보를 구매 여부에 따라 슬롯에 세팅
  - 장착과 강화버튼에 클릭시 작동할 함수

  > ```cs
  > public class InvenSlot : MonoBehaviour
  > {
  >     public void Init(InvenPopup inven)
  >     {
  >         invenPopup = inven;
  >         weaponManager = WeaponManager.Instance;
  >         //슬롯 별 구매 코스트
  >         BuyCost.text = weaponManager.weaponSOList[slotIndex].buyCost.ToString("N0");
  >     }
  > 
  > 		//구매여부에 따라 무기데이터 세팅
  >     public void SetData(WeaponData data)
  >    {
  >         bool isPuchased = data.isPurchased;
  >         weaponData = data;
  >         weaponSO = data.weaponSO;// 아래 if문으로 처리
  > 
  > 
  >         if (isPuchased)
  >         {
  >             WeaponIcon.sprite = weaponSO.weaponIcon;
  >             WPName.text = weaponSO.weaponName;
  >             WPLevel.text = "Lv." + $"{data.weaponLevel}";
  >             ATKVolum.text = CalculateATK().ToString();
  >             CRITVolum.text = weaponSO.baseCriticalChance.ToString("N1") + "%";
  >             UpgradeCost.text = CalculateCost().ToString();
  >         }
  >     }
  > 		
  > 		//무기 공격력
  >     int CalculateATK()
  >     {
  >         return weaponSO.baseAttack + weaponSO.attackVolume_Up * weaponData.weaponLevel;
  >     }
  > 
  >     public void OnBuyButton()
  >     {
  >         if (weaponData == null) return;
  > 
  >         if (GameManager.Instance.SpendBlueCoin(weaponSO.buyCost))
  >         {
  >             //구매 여부
  >             weaponData.isPurchased = true;
  > 
  >             //장비 정보 불러오기
  >             SetData(weaponData);
  >             //코스트 소모하기
  > 
  >             RefreshSlot();
  >         }
  >     }
  > 
  > 		//무기 강화
  >     public void OnUpgradeButton()
  >     {
  >         if (weaponData == null) return;
  > 
  >         if (GameManager.Instance.SpendBlueCoin(CalculateCost()))
  >         {
  >             weaponData.weaponLevel++; //무기레벨업
  >             SetData(weaponData);
  >             RefreshSlot();//업그레이드 버튼시에도 리프레쉬 
  >             if (weaponData.isEquip) weaponManager.equipWeaponInfo.SetEquipData(weaponData);//Setequipdata해주기 현재 장착된 경우에만
  >         }//코스트 소모하기
  >     }
  > 
  > 		//업그레이드에 소모되는 코스트
  >     int CalculateCost()
  >     {
  >         return weaponSO.upgradeCost + weaponSO.upgradeCost * weaponData.weaponLevel;
  >     }
  > 
  > 		//무기 장착
  >     public void OnEquip()
  >     {
  >         if (weaponData == null) return;
  > 
  >         weaponManager.equipWeaponInfo.equipedWeapon.isEquip = false;
  >         weaponData.isEquip = true;
  >         weaponManager.equipWeaponInfo.SetEquipData(weaponData);
  >         invenPopup.RefreshAllSlots();
  >         GameManager.Instance.player.weaponSwap.Equip(weaponSO.weaponPrefab);
  >     }
  > 
  >     public void RefreshSlot()
  >    {
  >         //장착 여부
  >         EquipButton.SetActive(!weaponData.isEquip);
  >         //구매 여부
  >         BuyButton.SetActive(!weaponData.isPurchased);
  >         //강화 버튼
  >         UpgradeButton.SetActive(weaponData.isPurchased);
  >         //강화/구매 버튼
  >         Equip_UpgradeBtn.SetActive(weaponData.isPurchased);
  >     }
  > }
  > ```


- **EquipWeaponInfo**
  - 장착된 무기의 정보를 실시간으로 업데이트하고 UI에 표시합니다.
    
  > ```cs
  > public class EquipWeaponInfo : MonoBehaviour
  > {
  >     public void SetEquipData(WeaponData data)
  >     {
  >         GameManager.Instance.curWeaponData = data;
  > 
  >         equipedWeapon = data;
  > 
  >         equipName.text = equipedWeapon.weaponSO.weaponName;
  >         equipIcon.sprite = equipedWeapon.weaponSO.weaponIcon;
  > 
  >         equipATK.text =
  >         $"공격력 : {equipedWeapon.weaponSO.baseAttack + equipedWeapon.weaponLevel * equipedWeapon.weaponSO.attackVolume_Up}";
  >         equipCRIT.text =
  >         $"치명타 확률 : {equipedWeapon.weaponSO.baseCriticalChance} %";
  >     }
  > }
  > ```

- **ScriptableObject**
  - 무기 정보 관리
    > ```cs
    > public class WeaponSO : ScriptableObject
    > {
    >     //무기 정보
    >     [Header("Info")]
    >     public int weaponID;                        //무기 ID
    >     public string weaponName;                   //무기 이름
    >     public int baseAttack;                      //무기 기본 공격력
    >     [Range(0f,100f)]                            //치명타 확률 범위
    >     public float baseCriticalChance = 5f;       //치명타 확률 %단위
    >     public int buyCost;                         //구매비용
    >     public int upgradeCost;                     //업그레이드 비용
    > 
    >     //무기 업그레이드 정보
    >     [Header("UpgradeInfo")]
    >     public int attackVolume_Up;       //공격력 강화 수치
    >     public int upgradeCost_UP;       //업그레이드 비용 상승 수치 (곱셈)
    > 
    >     //무기 아이콘
    >     [Header("Icon")]
    >    public Sprite weaponIcon;
    > 
    >     [Header("Prefab")]
    >     public GameObject weaponPrefab;
    > }
    > ```

<br>

### 🛡️ 윤혜진 (플레이어 능력치 강화 시스템)

### 스킬 강화 기능 (Skill.cs, SkillSO.cs)
- 플레이어가 가진 능력치를 소지 코인을 통해 강화시킬 수 있는 기능입니다.

- **스킬 정보 `SkillSO.cs`**
    - `ScriptableObject`를 통해 스킬을 구현하기 위해 필요한 정보들을 구성해주었습니다.
    - `StatIndex`라는 `enum`을 만들어 Skill이 접근할 `PlayerDate`의 `index`를 만들어주었습니다.
- **해당 코드**
  > ```cs
  > using UnityEngine;
  > 
  > public enum StatIndex
  > {
  >     CriticalDamage,          //치명타 데미지
  >     GoldGainRate,            //골드 획득량
  >     AutoAttackInterval       //자동 공격 간격
  > }
  > 
  > //ScriptableObject를 만들 때 빠르게 만들 수 있도록
  > //에셋 생성 메뉴창에 추가해주는 어트리뷰트
  > [CreateAssetMenu(fileName = "Skill", menuName = "New Skill")]
  > public class SkillSO : ScriptableObject
  > {
  >     public StatIndex index;           //올려줄 스탯 인덱스
  > 
  >     public string skillName;          //능력치 이름 (ex - 치명타)
  >     public string skillDescription;   //아래 출력될 설명 (ex - 치명타 데미지)
  > 
  >     public int maxLevel;              //최대 레벨 (ex - 10)
  >     public int basicPrice;            //기본 가격
  >         
  >    public int impressionPrice;       //인상 가격 폭 (n배로 증가)
  >     public int impressionStat;        //능력치 인상 폭 (n만큼 증가)
  > 
  >     public Sprite Icon;               //띄워줄 이미지 정보
  > }
  > ```

<br>

- **스킬 강화 `Skill.cs`**
    - **`Skill.cs`** 에서는 ****할당된 **`SkillSO`** 에 맞춰 플레이어의 Skill 정보와 UI를 관리합니다.
    - 캔버스에 있는 LevelUP 버튼이 눌릴 때마다 할당된 스킬 레벨이 1 오르도록 했으며
    - **프로퍼티**를 통해 **스킬의 레벨이 변경될 때를 감지**해 `PlayerData`값과 `UI`의 Text를 함께 변경시켜주었습니다.
      
 - **해당 코드**
    > ```cs
    > private int currentLevel;
    >  public int CurrentLevel
    >  {
    >      get
    >      {
    >          return currentLevel;
    >      }
    >      set
    >      {
    >          //이미 한계 레벨이라면 변경하지 않는다.
    >          if (!CheckMaxLevel(CurrentLevel)) return;
    >
    >          //플레이어가 현재 가격만큼의 코인을 가지고 있는지 체크 (false시 실행X)
    >          if (!GameManager.Instance.SpendCoin(currentPrice))
    >          {
    >              return;
    >          }
    >
    >          //CurrentLevel이 바뀔 때마다 playerData의 값을 변경
    >          currentLevel = value;
    >          UpdateSkillLevel(currentLevel);
    >
    >          //가격 갱신
    >          currentPrice *= data.impressionPrice;
    >
    >          //UI 갱신
    >          UIRefresh(data.index);
    >      }
    >  }
    > ```

<br>

- **연속 강화 `Skill.cs`**
    - **1초 이상** 버튼을 누르고 있으면 **0.2초 간격으로 강화**하는 기능입니다.
    - **`EventTrigger`** 컴포넌트를 추가해 **커서가 버튼을 누르는 순간**과 **커서가** **버튼에서 떨어지는 순간**을 감지할 수 있도록 했습니다.
- **해당 코드**
  
  ![image (8)](https://github.com/user-attachments/assets/4155e8b1-d131-4286-a0d4-479ca763c843)
      
      

<br>

### 🎯 박승규 (적 및 스테이지 관리 시스템)

- **적 관리**
  - 적 유형을 ScriptableObject로 관리하여 적의 데이터를 효율적으로 관리했습니다.
    ![1 PNG](https://github.com/user-attachments/assets/e52bc9a7-ce3e-45af-9568-6339c4ddbac7)

  - 적이 살아있는 동안 랜덤한 주기로 플레이어를 공격하는 시스템을 코루틴을 활용하여 구현했습니다.
    ![dsda PNG](https://github.com/user-attachments/assets/60011dd1-4419-47fe-bc39-6e149e4d7f4a)


<br>

### 🖱️ 예병권 (클릭 및 자동 공격 시스템)

- **클릭 이벤트 처리**
  - Input System을 사용하여 마우스 클릭 감지 및 클릭한 위치의 적을 공격하도록 구현했습니다.

- **자동 공격 시스템**

  - 자동 공격의 시작, 정지 및 업그레이드에 따라 공격 간격을 조정하는 기능을 구현했습니다.
    ![자동공격시작](https://github.com/user-attachments/assets/c8861f22-6d61-4cc8-baec-d4c956592ab9)
  - StartAutoAttack()으로 자동 공격 시작.
    ![자동 공격기능](https://github.com/user-attachments/assets/7deefc46-526b-46de-8466-5e692eb85259)
    - AutoAttackRoutine()에서 일정 간격으로 자동 공격 수행.
    - UpdateAutoAttack()에서 자동 공격 레벨 변경 시 자동으로 반영.

<br>

----


## 🧩 트러블 슈팅

### 🚧 황희돈

- **CoinCollector 리스트 동기화 이슈**
  - 대량의 동전이 연속적으로 드랍되며 이것이 역시 연속적으로 흡수되는것이 핵심인데 다량의 코인이 오브젝트풀에서 나와 collector에 리스트로 추가되는데 이때 흡수 코루틴 실행중에 리스트의 변경이 일어나면 오류가 발생하였습니다.

- **해결방법**
  - ![image (15)](https://github.com/user-attachments/assets/7fa1c10d-1427-4c55-ab83-acae7400dcb9) 
  - 코루틴이 진행되기 전에 현재의 인스턴스 리스트를 복제하여 현재의 리스트에 존재하는 인스턴스에 대하여 흡수하는 코루틴을 진행하고자 하였습니다.
  - 기본적으로 인스턴스는 참조형으로 인식되어 `copiedList = dropItemList` 를 적용했을 때 `dropitemList`가 변경된다면 `copiedlist` 역시 변경되어 깊은 복사를 적용하였습니다 깊은 복사를 적용하게 된다면 복사자체는 참조형으로 진행되지만 두 리스트는 다른 객체로서 존재하게 되어 `dropitemlist`가 변경되어도 `copiedlist`는 안전하게 코루틴을 진행할 수 있었습니다.
  

- **PlayerData 클래스의 역할 혼재**
  - ![image (16)](https://github.com/user-attachments/assets/f9d4a165-b14c-46fc-b56a-497020ef4fa0)
  - 현재 playerdata는 직렬화와 역직렬화의 역할을 맡음과 동시의 현재 진행상황과 업그레이드, 무기 데이터까지 역할을 맡고 있습니다 이는 playerdata의 책임을 모호하게 하며 코드진행과 협업에 불편을 겪게 하였습니다.
  - `SaveData` 클래스를 별도로 구성하여 플레이 데이터와 직렬화용 데이터를 하는 것이 프로젝트에 더 적절하다고 판단하였습니다. 현재 프로젝트에선 시간이 부족하여 구현하지 못하였지만 업그레이드나 아이템 획득 시 UI가 즉각적으로 변환되는 게임 특성상 옵저버 패턴을 플레이데이터에 적용하는 것이 바람직하다고 생각했습니다.

<br>

### 🚧 최동호

- **무기 장착 버튼 갱신 이슈**
  - 무기 가방에서 착용 중인 무기가 아닌 다른 무기의 강화를 한 후 장착하면 장착버튼이 사라지지 않음
  - 무기 강화시 슬롯을 업데이트 하지 않았고 코드의 서순이 꼬여있던 문제로 미리 만들어둔 RefreshSlot()함수를 추가하고 서순을 바로잡아 해결
  - 오류가 생기는 부분이 아니라서 찾는데 많은 도움을 받았습니다. 결과적으로는 간단한 문제였지만 찾는 과정에서 코드의 서순과 데이터 업데이트 타이밍의 중요성에 대해 알 수 있었습니다.

<br>

### 🚧 윤혜진

- **OnValidate 함수 빌드 오류**  
  - 문제: 게임을 만들고 모바일 빌드를 해보니 스킬 업그레이드 부분만 제대로 작동하지 않는 것을 확인했습니다.
  - 원인: OnValidate에서 너무 많은 작업을 진행했기 때문에 일어난 문제였습니다.
  - 설명:
    - OnValidate는 유니티에서만 실행되는 에디터 함수이고, 저는 지금까지 OnValidate에서 할당해 준 것들은 직접 손으로 끌어서 할당해준 것과 같은 취급을 받는다고 이해하고 있었습니다.
    - 그러나 OnValidate에서 GetComponent로 에디터에 있는 게임 오브젝트의 컴포넌트를 찾아오는 것은 문제가 되지 않지만, 플레이를 할 때 세팅되어야 하는 것까지 OnValidate에서 세팅해준다면 문제가 발생할 수 있다는 튜터님의 피드백을 받았습니다.
  - 해결 방법: OnValidate에서 처리해주던 코드 몇가지를 Start로 옮겨주니 빌드 시에도 제대로 작동하는 것을 확인할 수 있었습니다.

<br>

### 🚧 박승규

- **적 사망 시 공격 코루틴 중단 이슈**  
  - 적이 자동으로 플레이어를 공격하는 코루틴에서 hp가 0이 되었을 때도 호출이 되어 플레이어를 공격한 후에 죽는 현상이 있었다.
  - while(curHp > 0)에서 계속 반복되어 while에서도 다시  if(curHp > 0) 일 때 호출할 수 있도록 한 번 더 검사해 주었더니 해결되었다.
    
<br>

