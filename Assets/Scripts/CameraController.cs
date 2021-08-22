using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	GameObject[] ninja;
	public float smooothing = 5f;
	Vector3 offset;
	
	int i;
	void Start()
	{
		
		offset = new Vector3(transform.position.x - ninja[0].transform.position.x,ninja[0].transform.position.y,-10f);
	}

	void FixedUpdate()
	{
		if (GameManager.checkPice1)
			i = 1;
		else if (GameManager.checkPice2)
			i = 2;
		else if (GameManager.checkPice3)
			i = 3;
		else
			i = 0;
		if (!PlayerController.isEnd)
    { 
		Vector3 camPos =new Vector3( ninja[i].transform.position.x + offset.x, transform.position.y,-10f);
		transform.position = Vector3.Lerp(transform.position, camPos, smooothing * Time.deltaTime); 
		}
	}

}

