using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleController : MonoBehaviour
{
    public static float distance = 0f;
    float spawnTimer;
    public float spawnTimeRate;
    [SerializeField]
    GameObject ninja;
    [System.Serializable]
    public struct XSpawnRange
    {
        public float min;
        public float max;
    }
    [System.Serializable]
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }
    public XSpawnRange xSpawnRange;
    public YSpawnRange ySpawnRange;

     void Start() 
     { 
        GameObject obj = PoolObject.Instance.SpawnPole();
        obj.transform.position = new Vector2(Camera.main.transform.position.x+15f, Random.Range(ySpawnRange.min, ySpawnRange.max));
        float m= Random.Range(0.1f, 1f);
        LeanTween.move(obj, new Vector2( m, obj.transform.position.y), 0.8f);
        new WaitForSeconds(0.5f);
        distance = m;

        obj = PoolObject.Instance.SpawnPole();
        obj.transform.position = new Vector2(Camera.main.transform.position.x + 15f, Random.Range(ySpawnRange.min, ySpawnRange.max));
        m = Random.Range(1f, 2f);
        LeanTween.move(obj, new Vector2(distance+m, obj.transform.position.y), 1f);
        new WaitForSeconds(0.5f);
        distance = m;
    }
    void Update()
    {
        Check();
        int xs = Random.Range(0, 8);
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTimeRate&&!PlayerController.isEnd)
        {
            GameObject obj = PoolObject.Instance.SpawnPole();
            obj.transform.localPosition = new Vector2(distance + Random.Range(xSpawnRange.min, xSpawnRange.max), Random.Range(ySpawnRange.min, ySpawnRange.max));
            if (xs == 4)
            {
                GameObject obj1 = PoolObject.Instance.SpawnBird();
                obj1.transform.localPosition = new Vector2(obj.transform.position.x, obj.transform.position.y + 0.15f);
            }
            else if (xs == 2 || xs == 6)
            {
                GameObject obj2 = PoolObject.Instance.SpawnBird();
                obj2.transform.localPosition = new Vector2(obj.transform.position.x-0.3f, obj.transform.position.y + 0.15f);
                obj2 = PoolObject.Instance.SpawnBird();
                obj2.transform.localPosition = new Vector2(obj.transform.position.x + 0.3f, obj.transform.position.y + 0.15f);
                obj2.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            distance = obj.transform.position.x;
            spawnTimer = 0;

        }
    }
    void Check()
    {
        for(int i=0; i < 10; i++)
        {
            if (PoolObject.Instance.pole[i].transform.position.x < (Camera.main.transform.position.x - 4))
            {
                PoolObject.Instance.pole[i].SetActive(false);
            }
            if (PoolObject.Instance.bird[i].transform.position.x < (Camera.main.transform.position.x - 4))
            {
                PoolObject.Instance.bird[i].SetActive(false);
            }
        }
    }

}
