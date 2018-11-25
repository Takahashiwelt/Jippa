using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class lineController : MonoBehaviour {

	public GameObject lineObject;
	private GameObject cube;
	private BoxCollider2D cubeCollider;
	private List<GameObject> lines=new List<GameObject>();
	private GameObject mCamera;
	private Score sc;
	private GameObject spawn;
	private StartScene sscene;
	private AudioSource[] audioSource;
	public AudioClip karaage;
	public AudioClip niceJippa;
	public AudioClip bgm;
	
	void Start(){
		cube=GameObject.Find("Cube");
		cubeCollider=cube.GetComponent<BoxCollider2D>();
		mCamera=GameObject.Find("Canvas/Text");
		sc=mCamera.GetComponent<Score>();
		spawn=GameObject.Find("Spawn");
		sscene=GameObject.Find("Start").GetComponent<StartScene>();

		audioSource = gameObject.GetComponents<AudioSource>();
        audioSource[0].clip = karaage;
		audioSource[1].clip=niceJippa;
		audioSource[2].clip=bgm;
		audioSource[2].Play();

	}
	
	// Update is called once per frame
	private float time=0;
	void Update () {
		if(Input.GetMouseButton(0)&&sscene.GameEnd==false){
			GameObject line= Instantiate(lineObject,new Vector2(transform.position.x,transform.position.y),Quaternion.identity);
			line.transform.parent = this.transform;
			lines.Add(line);
		}
		
		if(Input.GetMouseButtonUp(0)&&sscene.GameEnd==false){
			if(IsCircle(lines[0])==true){
				audioSource[0].Play();
				int countC = calcChicken();
				if(countC==10){
					audioSource[1].Play();
				}
			}
			for(int i=0;i<lines.Count;i++){
				Destroy(lines[i]);
			}
			lines.Clear();
		}
		
		
		
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

	private Vector2[] points;
	private int count;
	bool IsCircle(GameObject line){//引いた線が閉じているか
		count=line.GetComponent<LineRenderer>().positionCount;
		//Debug.Log(count);
		Vector3[] positions=new Vector3[count];
		points=new Vector2[count];
		List<Senbun> senbun=new List<Senbun>();
		for(int i=0;i<count;i++){//連続する2点の線分を格納する
			positions[i]=line.GetComponent<LineRenderer>().GetPosition(i);
			points[i]=new Vector2(positions[i].x,positions[i].y);

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

	public List<GameObject> chickenInCircle;
	private int score;
	public int calcChicken(){
		chickenInCircle=new List<GameObject>();

		cubeCollider.enabled=false;//linesのcolliderと衝突するので処理が終わるまで消す
		int chickenNum=0;
		GameObject[] chickens=GameObject.FindGameObjectsWithTag("Chicken");
		chickenNum=chickens.Length;
		lines[0].GetComponent<PolygonCollider2D>().points=points;

		for(int i=0;i<chickenNum;i++){
			ChickenMove cm=chickens[i].GetComponent<ChickenMove>();
			//Debug.Log(cm.stayInCamera);
			//if(cm.stayInCamera==true){//ニワトリが画面内にいたら
				float maxDistance = 200f;

				Ray2D rayR=new Ray2D(chickens[i].transform.position,new Vector2(1f,0f));//Ray(右方向)
				RaycastHit2D hitR = Physics2D.Raycast((Vector2)rayR.origin, (Vector2)rayR.direction, maxDistance);
				//Debug.DrawRay(rayR.origin, rayR.direction*maxDistance, Color.red, 1f, false);
				if(hitR!=null&&hitR.collider.gameObject.tag=="Line"){
					Ray2D rayL=new Ray2D(chickens[i].transform.position,new Vector2(-1f,0f));//Ray(左方向)
					RaycastHit2D hitL = Physics2D.Raycast((Vector2)rayL.origin, (Vector2)rayL.direction, maxDistance);
					//Debug.DrawRay(rayL.origin, rayL.direction*maxDistance, Color.red, 1f, false);
					if(hitL.collider.gameObject.tag=="Line"){
						Ray2D rayU=new Ray2D(chickens[i].transform.position,new Vector2(0f,1f));//Ray(上方向)
						RaycastHit2D hitU = Physics2D.Raycast((Vector2)rayU.origin, (Vector2)rayU.direction, maxDistance);
						//Debug.DrawRay(rayU.origin, rayU.direction*maxDistance, Color.red, 1f, false);
						if(hitU.collider.gameObject.tag=="Line"){
							Ray2D rayD=new Ray2D(chickens[i].transform.position,new Vector2(0f,-1f));//Ray(下方向)
							RaycastHit2D hitD = Physics2D.Raycast((Vector2)rayD.origin, (Vector2)rayD.direction, maxDistance);
							//Debug.DrawLine(rayD.origin, rayD.direction*maxDistance, Color.red, 1f, false);
							if(hitD.collider.gameObject.tag=="Line"){
								chickenInCircle.Add(chickens[i]);
							}
						}
					}
				}
			//}
		}
		Destroy(lines[0].GetComponent<PolygonCollider2D>());
		cubeCollider.enabled=true;//処理が終わったので衝突判定をアクティブに
		
		score=chickenInCircle.Count;
		CalcScore(score);
		Debug.Log(score+" chicken(s) in circle!");
		for(int i=0;i<score;i++){
			Destroy(chickenInCircle[i]);
		}
		ChickenSpawn cs=spawn.GetComponent<ChickenSpawn>();
		cs.n-=score;

		return chickenInCircle.Count;
	}
	
	private int calcedScore=0;
	void CalcScore(int score){
		calcedScore=score;
		if(score==10){
			calcedScore=50;
		}
		
		sc.score+=calcedScore;

	}
	/*
	void WaEffect(){
		time=0;
		Font font;
		if(score==10){
			font=Resources.Load<Font>("Fonts/YAKITORI");
			waSuu.font=font;
			waSuu.text="ジッパ！";
		}else{
			font=Resources.Load<Font>("Arial");
			waSuu.font=font;
			waSuu.text=score+"羽";
		}
		howmany.SetActive(true);
		

	}
	*/
}
