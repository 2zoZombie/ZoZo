using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] Enemy[] Enemys;

    int stage = 1;
    int spawncount = 0;

    EnemyStats currentEnemyStats;

    public void Start()
    {
        StartCoroutine(StageCoroutine());
    }

    private IEnumerator StageCoroutine()
    {
        for (int i = stage; i < 15;)
        {
            while (spawncount == 0)
            {
                Debug.Log("Stage: " + stage);
                SpawnEnemy();
            }
            yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
            yield return new WaitForSeconds(5f);
            NextStage();

        }
    }
    public void SpawnEnemy()
    {
        int enmys = 5;
        for (int i = 0; i < enmys + (stage *2) ; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(2.5f, 3), Random.Range(-2f, 3f), 0);
            int num = Random.Range(0, 3);
            GameObject enemyObject;
            if (stage % 3 == 0 && stage != 0)
            {
                enemyObject = Instantiate(Enemys[3].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Boss Enemy";
                spawncount++;
                return;
            }
            else
            {
                enemyObject = Instantiate(Enemys[num].gameObject, spawnPosition, Quaternion.identity);
                enemyObject.name = "Normal Enemy" + i;
                spawncount++;
            }
        }
    }

    public void NextStage()
    {
        stage++;
        Debug.Log(stage);
        spawncount = 0;
        SpawnEnemy();
    }
}

