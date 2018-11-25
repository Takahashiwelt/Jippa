using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartScene : MonoBehaviour {
	public Text timerText;
	public float totalTime=31;
	int seconds;
	public GameObject scores;
	private Score score;
	public bool GameEnd=false;
	private AudioSource audioSource;
	public AudioClip whistle;
	// Use this for initialization
	void Start () {
		score=scores.GetComponent<Score>();

		audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = whistle;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime -= Time.deltaTime;
		seconds = (int)totalTime;
		timerText.text= seconds.ToString();
		if(totalTime<=0&&GameEnd==false){
			audioSource.Play();
			naichilab.RankingLoader.Instance.SendScoreAndShowRanking (score.score);
			GameEnd=true;
		}
	}
	
}
