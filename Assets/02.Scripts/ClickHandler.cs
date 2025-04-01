using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class ClickHandler : MonoBehaviour
{
    public LayerMask clickableLayer;   // Ŭ�� ���� ���̾�
    private bool isGamePaused = false; // ������ �Ͻ� ���� �������� Ȯ���ϴ� ����
    public InputAction clickAction;    // ���콺 Ŭ��

    public float attackSpeed;

    public Coroutine autoAttackCoroutine;   //�ڵ����� �ڷ�ƾ ����
    void Awake()
    {
        if (clickAction != null)
        {
            // Ŭ�� �̺�Ʈ ó�� �Լ� ���
            clickAction.performed += _ => ProcessClick();   // _�� �̺�Ʈ ������ �ڸ�

            // InputAction Ȱ��ȭ
            clickAction.Enable();
        }

        // �ڵ����� ������ 0���� ũ�� �ڵ����� ����
        if (GameManager.Instance.playerData.autoAttackLevel > 0)
        {
            StartAutoAttack();
        }
    }
    void OnDisable()
    {
        // InputAction ��Ȱ��ȭ

        if (clickAction != null)
        {
            clickAction.Disable();
        }
    }
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
        // ��� ������Ʈ ����
        Attack(target);
    }

    void Attack(GameObject target)
    {
        GameManager.Instance.OnAttack();
    }

    //�ڵ����� ����
    void StartAutoAttack()
    {
        if (autoAttackCoroutine == null)
        {
            Debug.Log("�ڵ����� ����");
            autoAttackCoroutine = StartCoroutine(AutoAttackRoutine());
        }
        else
        {
            Debug.Log("�̹� �ڵ����� ��");
        }
    }

    // �ڵ� ���� ����
    public void StopAutoAttack()
    {
        if (autoAttackCoroutine != null)
        {
            Debug.Log("�ڵ����� ����");
            StopCoroutine(autoAttackCoroutine);
            autoAttackCoroutine = null;
        }
        else
        {
            Debug.Log("�ڵ����� ���� ���� �ƴ�.");
        }
    }

    IEnumerator AutoAttackRoutine()
    {
        while (true)
        {
            attackSpeed = 1.0f / (1 + GameManager.Instance.playerData.autoAttackLevel * 0.2f);
            Debug.Log($"���� �ڵ� ���ݱ��� ��� �ð�: {attackSpeed}��");

            //�迹�� ��� : autoAttackLevel = 1�̶��, attackSpeed = 1.0f / (1 + 1 * 0.2f) = 1.0f / 1.2f  0.8333��

            yield return new WaitForSeconds(attackSpeed);
            GameManager.Instance.OnAttack();
        }
    }

    // �ڵ� ���� ���� ���� �� ����
    public void UpdateAutoAttack()
    {
        Debug.Log("�ڵ� ���� ���� �����. �ڵ� ���� ���� ������Ʈ.");

        StopAutoAttack();

        if (GameManager.Instance.playerData.autoAttackLevel > 0)
        {
            StartAutoAttack();
        }
    }

    // �Ͻ����� ���� �����ϴ� �Լ�
    void SetPauseState(bool isPaused)
    {
        isGamePaused = isPaused;

        if (isGamePaused)
        {
            Debug.Log("������ �Ͻ� ������. �ڵ� ���� ����.");
            StopAutoAttack();
        }
        else
        {
            Debug.Log("������ �簳��.");
            if (GameManager.Instance.playerData.autoAttackLevel > 0)
            {
                Debug.Log("�ڵ� ���� �����.");
                StartAutoAttack();
            }
        }
    }
}
