using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemCollector : MonoBehaviour
{
    List<DropItem> dropItems = new List<DropItem>();

    [Header("AbsorbPosition")]
    public Transform coinposition;
    public Transform blueCoinposition;
    public Transform playerPosition;

    private void Awake()
    {
        GameManager.Instance.dropItemCollector = this;
    }

    public void AddListDrops(DropItem item)
    {
        dropItems.Add(item);
    }

    public void AbsorbAllDrops()//스테이지 종료시 실행
    {
        if(coinposition == null || blueCoinposition == null || playerPosition == null) return;

        float interval = 1f/ dropItems.Count;
        StartCoroutine(CoroutineAbsorbAllDrops(interval));
    }

    IEnumerator CoroutineAbsorbAllDrops(float interval)
    {
        WaitForSeconds wait = new WaitForSeconds(interval);

        foreach (DropItem item in dropItems)
        {
            if (item is Coin)
            {
                item.AbsorbTo(coinposition);
            }
            else if (item is BlueCoin)
            {
                item.AbsorbTo(blueCoinposition);
            }
            else if (item is Heart)
            {
                item.AbsorbTo(playerPosition);
            }

            yield return wait;
        }

        ClearDrops();
    }


    public void ClearDrops()
    {
        dropItems.Clear();
    }
}
