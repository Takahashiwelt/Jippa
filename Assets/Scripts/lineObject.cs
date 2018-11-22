using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineObject : MonoBehaviour {
	
	public GameObject LineObject;
	private LineRenderer lineRenderer;
	private int lineIndex=1;

	private bool control=true;
	// Use this for initialization
	void Start () {
		lineRenderer=GetComponent<LineRenderer>();
		Debug.Log(lineRenderer);
		lineRenderer.enabled=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)&&control){
			touch();
		}
		if(Input.GetMouseButtonUp(0)){
			control=false;
		}

	}
	void touch(){
		Vector2 screenPoint=Input.mousePosition;
		Vector2 worldPoint=Camera.main.ScreenToWorldPoint(screenPoint);
		Debug.Log(worldPoint);

		lineRenderer.enabled=true;
		lineRenderer.SetVertexCount(lineIndex);
		lineRenderer.SetPosition(lineIndex-1,worldPoint);
		lineIndex++;
	}
}
