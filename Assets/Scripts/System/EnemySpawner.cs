using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner :  MonoBehaviour
{
    public GameObject Enemy;

    List<GameObject> enemies;

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

    IEnumerator CR_Spawn()
    {
        while (true)
        {
            enemies.Add(Instantiate(Enemy, new Vector3(-12, -4.91f, -3.32f), Quaternion.identity));
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator CR_Despawn()
    {

        yield return null;
    }

}