using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public AudioSource[] backgroundAudio;
    private void Start()
    {
        Instance = this;
    }
    private void Awake()
    {
        backgroundAudio[GameManager.index].Play();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("background");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
   
}
