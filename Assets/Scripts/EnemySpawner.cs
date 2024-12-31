using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] Vector3 SpawnSizes;
    [SerializeField] List<GameObject> AllEnemies;


    private void Awake()
    {
        instance = this;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .25f);
        //
        Gizmos.DrawCube(transform.position, SpawnSizes);
    }

    private void Start()
    {
        //InvokeRepeating(nameof(spawnenemy), 1, Random.Range(.5f, 2f));
    }

    public void spawnenemy()
    {
        Vector3 enemypos = transform.position + new Vector3(Random.Range(-SpawnSizes.x/2,SpawnSizes.x/2),
                                                            Random.Range(-SpawnSizes.y/2,SpawnSizes.y/2),
                                                            Random.Range(-SpawnSizes.z/2,SpawnSizes.z/2));
        GameObject newenemy = Instantiate(EnemyPrefab, enemypos, transform.rotation);
        newenemy.transform.SetParent(transform);

        AllEnemies.Add(newenemy);
    }

    public void resetallenemies()
    {
        //for (int i = 0; i < AllEnemies.Count; i++)
        //{
        //    AllEnemies[i].SetActive(false);
        //}

        //AllEnemies.Clear();
    }
    
}
