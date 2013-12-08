using UnityEngine;
using System.Collections;

public class ParticleCollider : MonoBehaviour {
	
	private int layerMask = 1 << 14;
	public Camera alt_camera;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(alt_camera.transform.position == Camera.main.transform.position){
			
		}else{
			
		}
		
		foreach (Touch touch in Input.touches) 
		{

			Ray ray = alt_camera.ScreenPointToRay(touch.position);
       		RaycastHit hit;
			
			if (touch.phase == TouchPhase.Began && touch.tapCount == 2)
			{	
            	if (Physics.Raycast (ray, out hit, layerMask)) 
				{						
					Debug.Log(hit.transform.name);						
				}
				
			}						
			
		}
		
	}
}
