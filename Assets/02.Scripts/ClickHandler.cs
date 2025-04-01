using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer;   // 클릭 가능 레이어
    private bool isGamePaused = false; // 게임이 일시 정지 상태인지 확인하는 변수
    public InputAction clickAction;    // 마우스 클릭

    public float attackSpeed;

    public Coroutine autoAttackCoroutine;   //자동공격 코루틴 저장
    void Awake()
    {
        if (clickAction != null)
        {
            // 클릭 이벤트 처리 함수 등록
            clickAction.performed += _ => ProcessClick();   // _는 이벤트 인자의 자리

            // InputAction 활성화
            clickAction.Enable();
        }

        // 자동공격 레벨이 0보다 크면 자동공격 시작
        if (GameManager.Instance.playerData.autoAttackLevel > 0)
        {
            StartAutoAttack();
        }
    }
    void OnDisable()
    {
        // InputAction 비활성화

        if (clickAction != null)
        {
            clickAction.Disable();
        }
    }
    void Update()
    {
        // 게임이 진행 중이고, 왼쪽 마우스 버튼이 눌렸을 때 클릭 처리
        if (isGamePaused) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        ProcessClick();
    }

    void ProcessClick()
    {
        //UI위에서 클릭했다면 무시
        if (IsPointerOverUI()) return;

        Vector2 worldClickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // 클릭 위치에서 특정 레이어에 속한 오브젝트 탐색
        RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, clickableLayer);

        if (hit.collider == null) return;

        // 클릭된 오브젝트가 존재하면 공격 실행
        HandleAttack(hit.collider.gameObject);
    }
    bool IsPointerOverUI()
    {   // UI 요소 위에서 클릭했는지 확인
        return EventSystem.current.IsPointerOverGameObject();
    }

    void HandleAttack(GameObject target)
    {
        // 대상 오브젝트 공격
        Attack(target);
    }

    void Attack(GameObject target)
    {
        GameManager.Instance.OnAttack();
    }

    //자동공격 시작
    void StartAutoAttack()
    {
        if (autoAttackCoroutine == null)
        {
            Debug.Log("자동공격 실행");
            autoAttackCoroutine = StartCoroutine(AutoAttackRoutine());
        }
        else
        {
            Debug.Log("이미 자동공격 중");
        }
    }

    // 자동 공격 중지
    public void StopAutoAttack()
    {
        if (autoAttackCoroutine != null)
        {
            Debug.Log("자동공격 중지");
            StopCoroutine(autoAttackCoroutine);
            autoAttackCoroutine = null;
        }
        else
        {
            Debug.Log("자동공격 실행 중이 아님.");
        }
    }

    IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            attackSpeed = 1.0f / (1 + GameManager.Instance.playerData.autoAttackLevel * 0.2f);
            Debug.Log($"다음 자동 공격까지 대기 시간: {attackSpeed}초");

            //↑예시 계산 : autoAttackLevel = 1이라면, attackSpeed = 1.0f / (1 + 1 * 0.2f) = 1.0f / 1.2f  0.8333초

            yield return new WaitForSeconds(attackSpeed);
            GameManager.Instance.OnAttack();
        }
    }

    // 자동 공격 레벨 변경 시 실행
    public void UpdateAutoAttack()
    {
        Debug.Log("자동 공격 레벨 변경됨. 자동 공격 상태 업데이트.");

        StopAutoAttack();

        if (GameManager.Instance.playerData.autoAttackLevel > 0)
        {
            StartAutoAttack();
        }
    }

    // 일시정지 상태 변경하는 함수
    void SetPauseState(bool isPaused)
    {
        isGamePaused = isPaused;

        if (isGamePaused)
        {
            Debug.Log("게임이 일시 정지됨. 자동 공격 중지.");
            StopAutoAttack();
        }
        else
        {
            Debug.Log("게임이 재개됨.");
            if (GameManager.Instance.playerData.autoAttackLevel > 0)
            {
                Debug.Log("자동 공격 재시작.");
                StartAutoAttack();
            }
        }
    }
}
