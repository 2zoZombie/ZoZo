using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    Enemy enemy;

    [SerializeField] GameObject[] Enemys;

    int stage = 0;
    int spawncount = 0;


    public void Start()
    {
        StartCoroutine(StageCoroutine());
    }

    private IEnumerator StageCoroutine()
    {
        while (spawncount == 0)
        {
            stage++;
            Debug.Log("Stage: " + stage);
            SpawnEnemy();
            yield return new WaitForSeconds(2f * Time.deltaTime);
        }
    }
    public void SpawnEnemy()
    {

        int enmys = 5;
        for (int i = 0; i < enmys ; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(2.5f, 3), Random.Range(1f, 3f), Random.Range(0f, 10f));
            int num = Random.Range(0, 2);
            GameObject enemyObject = Instantiate(Enemys[num], spawnPosition, Quaternion.identity);
            spawncount++;
            if (i % 3 == 0)
            {
                GameObject bossObject = Instantiate(Enemys[3], spawnPosition, Quaternion.identity);
                spawncount++;
            }
        }
    }
}

