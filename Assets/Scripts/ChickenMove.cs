using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMove : MonoBehaviour {
	
	public int direction=1;
	public float speed=1;
	public bool stayInCamera=false;
	// Use this for initialization
	private Vector2 scale;
	void Start () {
		Vector2 scale=transform.localScale*direction;
	}
	
	// Update is called once per frame
	
	void Update () {
		scale=transform.localScale;
		if(transform.position.x<=-37f||transform.position.x>=138f){
			speed*=-1;
			scale.x*=-1;
		}
		transform.localScale = scale;
		transform.position=new Vector2(transform.position.x+speed*Time.deltaTime,transform.position.y);
	}
	void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag=="Cube"){
			stayInCamera=true;
		}			
	}
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag=="Cube"){
			stayInCamera=false;
		}			
	}
}
