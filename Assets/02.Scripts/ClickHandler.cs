using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer;   // 클릭 가능한 레이어
    private bool isGamePaused = false; // 게임이 일시 정지 상태인지 확인하는 변수
    public InputAction clickAction;    // 마우스 클릭

    void Awake()
    {
        if (clickAction != null)
        {
            // 클릭 이벤트 처리 함수 등록
            clickAction.performed += _ => ProcessClick();   // _는 이벤트 인자의 자리

            // InputAction 활성화
            clickAction.Enable();
        }
    }
        void Update()
        {
            // 게임이 진행 중이고, 왼쪽 마우스 버튼이 눌렸을 때 클릭 처리
            if (isGamePaused) return;
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
            // 공격 로직
            Debug.Log("Attacking " + target.name);

            //  TODO 추가적으로 피해 계산 및 처리
        }

        // 일시정지 상태 변경하는 함수
        void SetPauseState(bool isPaused)
        {
            isGamePaused = isPaused;
        }
    private void OnDisable()
    {
        // InputAction 비활성화

        if (clickAction != null)
        {
            clickAction.Disable();
        }
    }
}
