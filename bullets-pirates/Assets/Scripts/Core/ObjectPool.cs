using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;

    private GameObject[] pool;
    private Transform spawnTransform;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        spawnTransform = this.transform;
    }

    public void PopulatePool()
    {
        PopulatePool(this.transform);
    }

    public void PopulatePool(Transform spawnTransform)
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(prefab, spawnTransform);
            pool[i].SetActive(false);
        }
    }

    public void PopulatePool(GameObject newPrefab, Transform spawnTransform, int newPoolSize)
    {
        pool = new GameObject[newPoolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(newPrefab, spawnTransform);
            pool[i].SetActive(false);
        }
    }
    
    public void PopulatePool(GameObject[] newPrefabs, Transform spawnTransform, int newPoolSize)
    {
        pool = new GameObject[newPoolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(newPrefabs[Random.Range(0,newPrefabs.Length)], spawnTransform);
            pool[i].SetActive(false);
        }
    }

    public GameObject RequestObjectInPool(Transform newSpawnTransform)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {   
                pool[i].transform.position = newSpawnTransform.position;
                pool[i].transform.rotation = newSpawnTransform.rotation;
                pool[i].SetActive(true);
                return pool[i];                
            }
        }
            return null;
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {   
                pool[i].transform.position = spawnTransform.position;
                pool[i].transform.rotation = spawnTransform.rotation;
                pool[i].SetActive(true);
                return;                
            }
        }
    }

    public void UpdateTransform(Transform newSpawnTransform)
    {
        spawnTransform = newSpawnTransform;
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return null;
        }
    }
}
