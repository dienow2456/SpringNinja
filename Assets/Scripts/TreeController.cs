using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public static float ds = 0f;
    float spawnTimer;
    public float spawnTimeRate;
 
    [System.Serializable]
    public struct XSpawnRange
    {
        public float min;
        public float max;
    }
   
    public XSpawnRange xSpawnRange;

    void Start()
    {
        GameObject obj = PoolObject.Instance.SpawnTree1();
        obj.transform.position = new Vector2(0, -0.05f); 
        ds = 0;
    }
    void Update()
    {
        Check();
       
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimeRate && !PlayerController.isEnd)
        {
            GameObject obj = PoolObject.Instance.SpawnTree1();
            obj.transform.localPosition = new Vector2(ds + Random.Range(xSpawnRange.min, xSpawnRange.max), -0.05f);
            ds = obj.transform.position.x;
            spawnTimer = 0f;
        }
    }
    void Check()
    {
        for (int i = 0; i < PoolObject.Instance.tree1.Count; i++)
        {
            if (PoolObject.Instance.tree1[i].transform.position.x < (Camera.main.transform.position.x - 6))
            {
                PoolObject.Instance.tree1[i].SetActive(false);
            }
           
        }
    }

}
