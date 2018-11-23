using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

	private Camera _mainCamera;

	void Start () {
		// カメラオブジェクトを取得します
		GameObject obj = GameObject.Find ("Main Camera");
		_mainCamera = obj.GetComponent<Camera> ();

		// 座標値を出力
		Debug.Log (getScreenTopLeft ().x + ", " + getScreenTopLeft ().y);
		Debug.Log (getScreenBottomRight ().x + ", " + getScreenBottomRight ().y);
	}

	public Vector2 getScreenTopLeft() {
		// 画面の左上を取得
		Vector2 topLeft = _mainCamera.ScreenToWorldPoint (Vector2.zero);
		// 上下反転させる
		topLeft.Scale(new Vector2(1f, -1f));
		return topLeft;
	}

	public Vector2 getScreenBottomRight() {
		// 画面の右下を取得
		Vector2 bottomRight = _mainCamera.ScreenToWorldPoint (Vector2.one);
		// 上下反転させる
		bottomRight.Scale(new Vector2(1f, -1f));
		return bottomRight;
	}
}
