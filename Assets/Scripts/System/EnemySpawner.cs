using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner :  MonoBehaviour
{
    public GameObject Enemy;

    List<GameObject> enemies;

    public Transform[] SpawnerPoint;

    void Awake()
    {
        enemies = new List<GameObject>();
    }

    public void StartSpawn()
    {
        StartCoroutine(CR_Spawn());
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void RemoveEnemy()
    {

    }

    Transform RandomPoint()
    {
        Transform result;
        System.Random random = new System.Random();
        int temp = random.Next(0, 8);
        result = SpawnerPoint[temp];
        return result;
    }

    IEnumerator CR_Spawn()
    {
        while (true)
        {
            Transform temp = RandomPoint();
            enemies.Add(Instantiate(Enemy, temp.position, Quaternion.identity));
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator CR_Despawn()
    {

        yield return null;
    }

}