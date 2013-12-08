using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	EventDispatcher evd;
	
	// Use this for initialization
	void Start () {
		evd = this.gameObject.AddComponent<EventDispatcher>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Touch touch in Input.touches) 
		{
			
			if (touch.phase == TouchPhase.Began && touch.tapCount == 1)
			{		
				evd.FadeEffect("screen");
				evd.NewScene(null, "Game");						
			}
		}
	}
}
