using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPool : ObjectPool<HealthBar>
{
    public HealthBar bossHealthBar;
    public HealthBar playerHealthBar;
    public Canvas enemyHealthBarCanvas;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.healthBarPool = this;
    }

    public HealthBar GetFromPool(Transform spawnPosition, Transform newParent = null)
    {
        HealthBar obj;
        if (poolDictionary[prefabs[0].name].Count > 0)
        {
            obj = poolDictionary[prefabs[0].name].Dequeue();
        }
        else // 풀에 남은 오브젝트가 없으면 새로 생성
        {
            obj = Instantiate(prefabs[0]).GetComponent<HealthBar>();
            obj.name = prefabs[0].name;
        }

        if (newParent != null) obj.transform.SetParent(newParent);
        else obj.transform.SetParent(enemyHealthBarCanvas.transform);

        obj.transform.position = Camera.main.WorldToScreenPoint(spawnPosition.position);
        obj.gameObject.SetActive(true);

        return obj;
    }
}
