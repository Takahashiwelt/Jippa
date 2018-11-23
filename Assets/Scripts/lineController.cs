﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineController : MonoBehaviour {

	public GameObject lineObject;
	private List<GameObject> lines=new List<GameObject>();
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			GameObject line= Instantiate(lineObject,new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
			line.transform.parent = this.transform;
			lines.Add(line);
		}
		
		if(Input.GetMouseButtonUp(0)){
			if(IsCircle(lines[0])==true){
				calcChicken();
			}
			for(int i=0;i<lines.Count;i++){
				Destroy(lines[i]);
			}
			lines.Clear();
		}
		if(Input.GetKeyDown("space"))calcChicken();
		
	}
	struct Senbun{
		public Vector2 p1;
		public Vector2 p2;
		public Senbun(Vector2 p1,Vector2 p2){
			this.p1=p1;
			this.p2=p2;
		}
	}
	private List<Senbun> circle=new List<Senbun>();//円を構成する線分
	bool IsCircle(GameObject line){//引いた線が閉じているか
		int count=line.GetComponent<LineRenderer>().positionCount;
		//Debug.Log(count);
		Vector3[] positions=new Vector3[count];
		List<Senbun> senbun=new List<Senbun>();
		for(int i=0;i<count;i++){//連続する2点の線分を格納する
			positions[i]=line.GetComponent<LineRenderer>().GetPosition(i);
			
			if(i>=1){
				Vector2 q1=new Vector2(positions[i-1].x,positions[i-1].y);
				Vector2 q2=new Vector2(positions[i].x,positions[i].y);
				senbun.Add(new Senbun(q1,q2));
				Debug.Log(senbun[i-1].p1+":"+senbun[i-1].p2);
			}
		}
		int calCount=0;
		for(int i=0;i<senbun.Count;i++){//それぞれの線分が重なっているか判定する
			if(i>=1){
				for(int j=0;j<i;j++){
					calCount++;
					Vector2 p1=senbun[j].p1;
					Vector2 p2=senbun[j].p2;
					Vector2 p3=senbun[i].p1;
					Vector2 p4=senbun[i].p2;
					
					if(i-j==1)continue;

					 var d = (p2.x - p1.x) * (p4.y - p3.y) - (p2.y - p1.y) * (p4.x - p3.x);

    				if (d == 0.0f)
    				{
        				continue;
    				}

				    var u = ((p3.x - p1.x) * (p4.y - p3.y) - (p3.y - p1.y) * (p4.x - p3.x)) / d;
    				var v = ((p3.x - p1.x) * (p2.y - p1.y) - (p3.y - p1.y) * (p2.x - p1.x)) / d;

				    if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
    				{
    				    continue;
    				}
					Debug.Log("Cross! "+j+":"+i);
					for(int n=j;n<=i;n++){//閉じた円の部分を取り出す
						circle.Add(senbun[n]);
					}
					return true;
				}
			}
		}
		return false;
	}

	public int calcChicken(){
		int chickenNum=0;
		GameObject[] chickens=GameObject.FindGameObjectsWithTag("Chicken");
		chickenNum=chickens.Length;


		for(int i=0;i<chickenNum;i++){
			ChickenMove cm=chickens[i].GetComponent<ChickenMove>();
			if(cm.stayInCamera==true){
				Ray2D ray=new Ray2D(chickens[i].transform.position,new Vector2(1f,0f));

  				//Rayの長さ
				float maxDistance = 50;

				RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
				if(hit.collider.gameObject.tag=="Line"){
					Debug.Log("Checken in the circle!!!");
				}
				Debug.DrawRay (ray.origin, ray.direction*50f, Color.red, 10f, false);
			}
		}

		Debug.Log("chicken is "+chickenNum);
		return chickenNum;
	}
}
