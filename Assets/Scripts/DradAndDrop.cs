using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DradAndDrop : MonoBehaviour{

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	// ドラッグ前の位置
    

    public void OnDrag()
    {
		Debug.Log("Dragging");
        Vector2 TapPos = Input.mousePosition;
        transform.position = TapPos;
    }
	
}
