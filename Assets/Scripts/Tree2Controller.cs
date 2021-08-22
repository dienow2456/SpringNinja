using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree2Controller : MonoBehaviour
{
    public static float ds1 = 0f;
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
        GameObject obj = PoolObject.Instance.SpawnTree2();
        obj.transform.position = new Vector2(1, 0.4f);
        ds1 = 1;
    }
    void Update()
    {
        Check();

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimeRate && !PlayerController.isEnd)
        {
            GameObject obj = PoolObject.Instance.SpawnTree2();
            obj.transform.localPosition = new Vector2(ds1 + Random.Range(xSpawnRange.min, xSpawnRange.max), 0.4f);
            ds1 = obj.transform.position.x;
            spawnTimer = 0f;
        }
    }
    void Check()
    {
        for (int i = 0; i < PoolObject.Instance.tree2.Count; i++)
        {
            if (PoolObject.Instance.tree2[i].transform.position.x < (Camera.main.transform.position.x - 6))
            {
                PoolObject.Instance.tree2[i].SetActive(false);
            }

        }
    }

}
