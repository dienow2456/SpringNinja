using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoxoController : MonoBehaviour
{
    class PoolObject
    {
        public Transform transform;
        public bool isUse;
        public PoolObject(Transform t) { transform = t; }
        public void Use() { isUse = true; }
        public void Dispose() { isUse = false; }
    }

    [System.Serializable]
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }

    
    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public YSpawnRange ySpawnRange;

    float spawnTimer;
    PoolObject[] poolObjects;
 
    void Awake()
    {
        Configure();
        Transform t = GetPoolObject();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.x = Camera.main.transform.position.x + 14f ;
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
    }

   

    void Update()
    {
        
        Shift();
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate&&!PlayerController.isEnd)
        {
            Spawn();
            spawnTimer = 0;
        }
    }

    void Configure()
    {
      
        poolObjects = new PoolObject[poolSize];
        for (int i = 0; i < poolObjects.Length; i ++)
        {
            GameObject go = Instantiate(Prefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjects[i] = new PoolObject(t);
        }
       
    }
    void Spawn()
    {
        Transform t = GetPoolObject();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.x = Camera.main.transform.position.x +21f ;
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;

        t = GetPoolObject();
        if (t == null) return;
        pos = Vector3.zero;
        pos.x = Camera.main.transform.position.x + 31f ;
        pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
        t.position = pos;
    }


    void Shift()
    {
        for(int i=0; i<poolObjects.Length; i++)
        {
            poolObjects[i].transform.localPosition += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]);
        }
    }

    void CheckDisposeObject(PoolObject poolObject)
    {
        if(poolObject.transform.position.x < Camera.main.transform.position.x-2f )
        {
            poolObject.Dispose();
            poolObject.transform.position=Vector3.one*1000;
        }
    }

    Transform GetPoolObject()
    {
        for(int i=0; i<poolObjects.Length; i++)
        {
            if (!poolObjects[i].isUse)
            {
                poolObjects[i].Use();
                return poolObjects[i].transform ;
            }
        }
        return null;
    }
}
