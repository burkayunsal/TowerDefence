using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class PoolHandler : Singleton<PoolHandler>
{

    [System.Serializable] internal struct Pool
    {
        internal Queue<PoolObject> pooledObj;
        [MustBeAssigned] [SerializeField] internal PoolObject objPrefab;
        [PositiveValueOnly] [SerializeField] internal int poolLength;
    }

    [SerializeField] private Pool[] pools;
    
    bool isInit = false;

    public bool IsInitialized() => isInit;

    private void Start()
    {
        CreatePools();
    }

    void CreatePools()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].pooledObj = new Queue<PoolObject>();

            for (int j = 0; j < pools[i].poolLength; j++)
            {
                PoolObject poolObject = Instantiate(pools[i].objPrefab, transform);
                
                pools[i].pooledObj.Enqueue(poolObject);
                
                poolObject.OnCreated();
            }
        }

        isInit = true;
    }
    
    public T GetObject<T>() where T : PoolObject
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if(typeof(T) == pools[i].objPrefab.GetType())
            {
                T obj = pools[i].pooledObj.Dequeue() as T;
                pools[i].pooledObj.Enqueue(obj);
                obj.OnSpawn();
                return obj;
            }
        }
        return default;
    }
}
