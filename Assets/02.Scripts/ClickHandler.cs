using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer; // Ŭ�� ������ ���̾�
    private bool isGamePaused = false; // ������ �Ͻ� ���� �������� Ȯ���ϴ� ����

    void Update()
    {
        // ������ ���� ���̰�, ���� ���콺 ��ư�� ������ �� Ŭ�� ó��
        if (isGamePaused) return;
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        ProcessClick();
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
        // ��� ������Ʈ�� ���� ���� ó��
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
}
