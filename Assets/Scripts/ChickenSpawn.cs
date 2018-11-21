using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSpawn : MonoBehaviour {

	// Use this for initialization
	public GameObject chicken;
	private int i=0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Invoke("Spawn",3f);
	}
	void Spawn(){
		float y = Random.Range(29.0f,145.0f);
		GameObject spawnedChicken = Instantiate(chicken,new Vector2(225f,y),Quaternion.identity);
		ChickenMove cm=spawnedChicken.GetComponent<ChickenMove>();
		cm.speed=Random.Range(-8f,-12f);
		for(int j=0;j<=1000;j++);
		i++;
	}
}
