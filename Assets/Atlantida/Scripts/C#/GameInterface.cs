using UnityEngine;
using System.Collections;

public class GameInterface : MonoBehaviour {
	
	LevelManager lm;
	CameraMove camMove;
	
	private GameObject item;
	private LayerMask altCamLMask = 1 << 11;
	
	void Start () {
		lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
		camMove = this.gameObject.AddComponent<CameraMove>();
	}

	void Update () {
		if(!lm.altCamera.enabled)
			touchEvents();
	}	
		
	private void touchEvents()
	{
		foreach (Touch touch in Input.touches) 
		{
			Ray ray = Camera.main.ScreenPointToRay(touch.position);
       		RaycastHit hit;
			if (touch.phase == TouchPhase.Began && touch.tapCount == 2)
			{	
            	if (Physics.Raycast (ray, out hit, 4, altCamLMask)) 
				{	
					item = hit.transform.gameObject;						
					camMove.Settings( item.transform );
					
				} 
			}						
		}
	}
	
}
