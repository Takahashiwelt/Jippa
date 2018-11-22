using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour {

	public GameObject lineObject;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			Instantiate(lineObject,new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
		}
	}
}
