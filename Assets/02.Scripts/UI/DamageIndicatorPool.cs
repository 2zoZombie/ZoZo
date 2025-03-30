using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorPool : ObjectPool<DamageIndicator>
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.damageIndicatorPool = this;
    }

    public  GameObject GetFromPool(Transform spawnPosition, Transform newParent = null)
    {
        GameObject obj;
        if (poolDictionary[prefabs[0].name].Count > 0)
        {
            obj = poolDictionary[prefabs[0].name].Dequeue().gameObject;
        }
        else // 풀에 남은 오브젝트가 없으면 새로 생성
        {
            obj = Instantiate(prefabs[0]);
            obj.name = prefabs[0].name;
        }

        if (newParent != null) obj.transform.SetParent(newParent);
        obj.transform.position = Camera.main.WorldToScreenPoint( spawnPosition.position);
        obj.gameObject.SetActive(true);

        return obj;
    }
}
