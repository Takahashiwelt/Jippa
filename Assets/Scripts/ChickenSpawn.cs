using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawn : MonoBehaviour {

	// Use this for initialization
	public GameObject chicken;
	void Start () {
		
	}
	
	// Update is called once per frame
	public int n=0;
	float timeOut=0.1f;
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
		
		float y = Random.Range(-14.0f,79.0f);
		float spawnX;
		int direction=1;
		if(Random.Range(-1,1)>=0){
			spawnX=126f;
			direction=1;
		}else{
			spawnX=-36f;
			direction=-1;
		}
		GameObject spawnedChicken = Instantiate(chicken,new Vector2(spawnX,y),Quaternion.identity);
		ChickenMove cm=spawnedChicken.GetComponent<ChickenMove>();
		cm.speed=Random.Range(-8f,-15f);
		cm.direction=direction;

		
	}
}
