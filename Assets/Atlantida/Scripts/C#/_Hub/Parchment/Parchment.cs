using UnityEngine;
using System.Collections;

public class Parchment : MonoBehaviour {
	
	public Camera alternate_cam;
	public AudioClip page_turn;
	
	private LayerMask interactiveLayerMask = 1 << 10;
	private bool isOpened = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if(alternate_cam.enabled)	
			touchEvents();
		else if(!alternate_cam.enabled)
			if(isOpened)
				CloseIt();
	}
	
	private void touchEvents()
	{
		foreach (Touch touch in Input.touches) 
		{

			Ray ray = alternate_cam.ScreenPointToRay(touch.position);
       		RaycastHit hit;
			
			if (touch.phase == TouchPhase.Began && touch.tapCount == 1)
			{	
            	if (Physics.Raycast (ray, out hit, 1.5f, interactiveLayerMask)) 
				{		
					if(hit.transform.name == "Helix001"){
						if(!isOpened){
				 			transform.animation.CrossFade("Unfolded");
							isOpened = true;
							PlaySound();
						}
					}
				} 
			}						
			
		}
	}
	
	private void CloseIt(){
		transform.animation.CrossFade("FoldBack");
		isOpened = false;
		PlaySound();
	}
	
	private void PlaySound(){
		audio.clip = page_turn;
		audio.Play();
	}
	
}
