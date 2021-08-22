using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public static PoolObject Instance;
    public GameObject Pole;
    public GameObject Tree1;
    public GameObject Tree2;
    public GameObject Bird;

    [SerializeField]
    public List<GameObject> pole, bird, tree1, tree2;
    private void Awake()
    {
        Instance = this;
        pole = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(Pole, transform);
            obj.SetActive(false);
            pole.Add(obj);

            obj = Instantiate(Bird, transform);
            obj.SetActive(false);
            bird.Add(obj);

            obj = Instantiate(Tree1, transform);
            obj.SetActive(false);
            tree1.Add(obj);

            obj = Instantiate(Tree2, transform);
            obj.SetActive(false);
            tree2.Add(obj);
        }
        
    }
    public GameObject SpawnPole()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!pole[i].activeSelf)
            {
                pole[i].SetActive(true);
                return pole[i];
            }
        }
        GameObject obj = Instantiate(Pole, transform);
        pole.Add(obj);
        obj.SetActive(true);
        return obj;

    }

    public GameObject SpawnBird()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!bird[i].activeSelf)
            {
                bird[i].SetActive(true);
                return bird[i];
            }
        }
        GameObject obj = Instantiate(Bird, transform);
        bird.Add(obj);
        obj.SetActive(true);
        return obj;
    }

    public GameObject SpawnTree1()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!tree1[i].activeSelf)
            {
                tree1[i].SetActive(true);
                return tree1[i];
            }
        }
        GameObject obj = Instantiate(Tree1, transform);
        tree1.Add(obj);
        obj.SetActive(true);
        return obj;

    }
    public GameObject SpawnTree2()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!tree2[i].activeSelf)
            {
                tree2[i].SetActive(true);
                return tree2[i];
            }
        }
        GameObject obj = Instantiate(Tree2, transform);
        tree2.Add(obj);
        obj.SetActive(true);
        return obj;

    }
}
