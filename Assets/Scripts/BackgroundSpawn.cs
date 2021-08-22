using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer renderBG;

    public float smoothing;

	void LateUpdate()
	{	if(!PlayerController.isOver||!PlayerController.isEnd)
			renderBG.size += new Vector2(smoothing*Time.deltaTime, 0f);
	}
}
