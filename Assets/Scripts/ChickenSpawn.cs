using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawn : MonoBehaviour {

	// Use this for initialization
	public GameObject chicken;
	void Start () {
		
	}
	
	// Update is called once per frame
	int n=0;
	float timeOut=0.5f;
	float timeElapsed=0f;
	void Update () {
		timeElapsed+=Time.deltaTime;
		if(timeElapsed>=timeOut){
			if(n<100){
				Spawn();
				n++;
			}
			timeElapsed=0f;
		}
	}
	void Spawn(){
		
		float y = Random.Range(-20.0f,90.0f);
		GameObject spawnedChicken = Instantiate(chicken,new Vector2(126f,y),Quaternion.identity);
		ChickenMove cm=spawnedChicken.GetComponent<ChickenMove>();
		cm.speed=Random.Range(-8f,-12f);	
		
	}
}
