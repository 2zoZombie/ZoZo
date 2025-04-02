using DG.Tweening;
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
        StageManager.Instance.onStageComplete.AddListener(AbsorbAllDrops);
    }

    private void Start()
    {
        ClearDrops();
    }

    public void AddListDrops(DropItem item)
    {
        dropItems.Add(item);
    }

    public void AbsorbAllDrops()//스테이지 종료시 실행
    {
        if (coinposition == null || blueCoinposition == null || playerPosition == null) return;

        StartCoroutine(CoroutineAbsorbAllDrops());

        
    }


    //간격이 너무짧아지면 코루틴이 폭주 성능저하 + interval 무한이 될가능성 존재
    IEnumerator CoroutineAbsorbAllDrops()
    {
        yield return new WaitForSeconds(2f);

        List<DropItem> copiedList = new List<DropItem>(dropItems); //for문 실행중 add된다면 unvalidate 에러 발생 그러므로 참조형이 아닌 깊은복사를 하여 새로운 리스트 생성후 복사
        ClearDrops();

        float interval = Mathf.Clamp(1f / copiedList.Count, 0.002f, 0.1f);

        for (int i = 0; i < copiedList.Count; i++)
        {
            DropItem item = copiedList[i];

            if (item is Coin) item.AbsorbTo(coinposition);
            else if (item is BlueCoin) item.AbsorbTo(blueCoinposition);
            else if (item is Heart) item.AbsorbTo(playerPosition);

            yield return new WaitForSeconds(interval); // 순차적으로 처리
        }
    }


    public void ClearDrops()
    {
        dropItems.Clear();
    }
}
