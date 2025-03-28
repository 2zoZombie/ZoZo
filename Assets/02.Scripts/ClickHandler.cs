using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer;   // Ŭ�� ������ ���̾�
    private bool isGamePaused = false; // ������ �Ͻ� ���� �������� Ȯ���ϴ� ����
    public InputAction clickAction;    // ���콺 Ŭ��

    void Awake()
    {
        if (clickAction != null)
        {
            // Ŭ�� �̺�Ʈ ó�� �Լ� ���
            clickAction.performed += _ => ProcessClick();   // _�� �̺�Ʈ ������ �ڸ�

            // InputAction Ȱ��ȭ
            clickAction.Enable();
        }
    }
        void Update()
        {
            // ������ ���� ���̰�, ���� ���콺 ��ư�� ������ �� Ŭ�� ó��
            if (isGamePaused) return;
        }

        void ProcessClick()
        {
            //UI������ Ŭ���ߴٸ� ����
            if (IsPointerOverUI()) return;

            Vector2 worldClickPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // Ŭ�� ��ġ���� Ư�� ���̾ ���� ������Ʈ Ž��
            RaycastHit2D hit = Physics2D.Raycast(worldClickPosition, Vector2.zero, Mathf.Infinity, clickableLayer);

            if (hit.collider == null) return;

            // Ŭ���� ������Ʈ�� �����ϸ� ���� ����
            HandleAttack(hit.collider.gameObject);
        }
        bool IsPointerOverUI()
        {   // UI ��� ������ Ŭ���ߴ��� Ȯ��
            return EventSystem.current.IsPointerOverGameObject();
        }

        void HandleAttack(GameObject target)
        {
            // ��� ������Ʈ ����
            Attack(target);
        }

        void Attack(GameObject target)
        {
            // ���� ����
            Debug.Log("Attacking " + target.name);

            //  TODO �߰������� ���� ��� �� ó��
        }

        // �Ͻ����� ���� �����ϴ� �Լ�
        void SetPauseState(bool isPaused)
        {
            isGamePaused = isPaused;
        }
    private void OnDisable()
    {
        // InputAction ��Ȱ��ȭ

        if (clickAction != null)
        {
            clickAction.Disable();
        }
    }
}
