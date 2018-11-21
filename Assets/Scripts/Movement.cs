using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	Vector2 velocity;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb=GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity.x=Input.GetAxisRaw("Horizontal")*3.0f;
		velocity.y=Input.GetAxisRaw("Vertical")*3.0f;
	}
	void FixedUpdate(){
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}
}
