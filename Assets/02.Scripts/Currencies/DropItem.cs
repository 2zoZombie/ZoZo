using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public void PlayBounce(Transform spawnPos)
    {
        transform.position = spawnPos.position;

        // 랜덤 방향 오프셋 생성 타원형 (y만 줄인 원형)범위의 오프셋
        Vector2 randomCircle = Random.insideUnitCircle.normalized * Random.Range(0.5f, 2f);
        randomCircle.y = randomCircle.y / 3;
        Vector3 randomOffset = new Vector3(randomCircle.x, randomCircle.y, 0f);
        Vector3 targetPos = spawnPos.position + randomOffset;

        // DOJump 사용: (목표위치, 점프파워, 점프횟수, 지속시간)
        transform.DOJump(targetPos, 3f, 1, 0.4f)
                 .SetEase(Ease.OutQuad)
                 .OnComplete(() => StartIdle());
    }

    public virtual void StartIdle()
    {
    }

    public void AbsorbTo(Transform target)
    {
        transform.DOMove(target.position, 0.5f)
                 .SetEase(Ease.InCubic)
                 .OnComplete(() =>
                 {
                     GetItem();
                     GameManager.Instance.dropItemPool.ReturnToPool(this.gameObject);
                 });
    }

    protected virtual void GetItem()
    {

    }
}
