using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour {
	public float speed=-1f;
	private float direction=1f;
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 scale=transform.localScale;
		if(transform.position.x<=56f||transform.position.x>=230f){
			speed*=-1;
			scale.x*=-1;
		}
		transform.localScale = scale;
		transform.position=new Vector2(transform.position.x+speed*Time.deltaTime,transform.position.y);
	}
}
