using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageIndicatorPool : ObjectPool<DamageIndicator>
{
    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.damageIndicatorPool = this;
    }

    public DamageIndicator GetFromPool(Transform spawnPosition, Transform newParent = null)
    {
        DamageIndicator obj;
        if (poolDictionary[prefabs[0].name].Count > 0)
        {
            obj = poolDictionary[prefabs[0].name].Dequeue();
        }
        else // 풀에 남은 오브젝트가 없으면 새로 생성
        {
            obj = Instantiate(prefabs[0]).GetComponent<DamageIndicator>();
            obj.name = prefabs[0].name;
        }

        if (newParent != null) obj.transform.SetParent(newParent);

        obj.transform.position = Camera.main.WorldToScreenPoint( spawnPosition.position);
        obj.gameObject.SetActive(true);

        return obj;
    }
}
