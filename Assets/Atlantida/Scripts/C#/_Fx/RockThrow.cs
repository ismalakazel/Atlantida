using UnityEngine;
using System.Collections;

public class RockThrow : MonoBehaviour {
	
	LevelManager lm;	
	private GameObject _rockThrow;
	private GameObject _rightHand;
	private GameObject _bunch_of_rocks;
	private Vector3 rightHandOri;
	private Vector3 _rockOri;
	private Vector3 IpadDir; 
	private float ShootElapsedTime = 0;
	private bool ShootMagnitudeStart = false;	
	private bool ShootRestart = false;
	private bool rightHandBack = false;
	private float rightHandBackTime = 0;
	private LayerMask _rockThrowLayer = 1 << 12;
	private bool isRockAvailable = false;
	
	void Start () {
		lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
		_rockThrow = GameObject.Find("_rockThrow");
		_rightHand = GameObject.Find("_right_hand");
		_bunch_of_rocks = GameObject.Find("_bunch_of_rocks");
		rightHandOri = _rightHand.transform.localPosition;
	}
	
	void Update () {
		
		if(rightHandBack){
			rightHandBackTime = Time.deltaTime * 2;
			_rightHand.transform.localPosition = Vector3.Slerp(_rightHand.transform.localPosition, rightHandOri, rightHandBackTime);
			if(rightHandBackTime >= 1.5f){
				rightHandBack = false;
				rightHandBackTime = 0;
			}
		}
		
		TouchEvent();
		ShootEvent();
	}
	
	private void ShootEvent() 
	{
	    IpadDir = Vector3.zero;
   		IpadDir.y = Input.acceleration.y;
		if(ShootRestart){
			if (IpadDir.y >= 0.8f) {
				ShootMagnitudeStart = true;
			}
		}
		
		if (ShootMagnitudeStart) {
			ShootElapsedTime += Time.deltaTime;
			if (ShootElapsedTime <= 0.2f) {
				if (IpadDir.y <= 0.5f) 
				{
					ShootMagnitudeStart = false;
					ShootRestart = false;
					StartCoroutine(ShootRock());
				}
			} 
			else 
			{
				ShootMagnitudeStart = false;
				ShootElapsedTime = 0;
			}
		}
	}
	
	private IEnumerator ShootRock()
	{
		_rockThrow.rigidbody.isKinematic = false;
		_rockThrow.rigidbody.AddForce(lm.mainCamera.transform.forward * 12f, ForceMode.Impulse);
		_rockThrow.transform.parent = _bunch_of_rocks.transform.parent;
		yield return new WaitForSeconds(3);
			PutRockBack();
	}
	
	private void GetRock()	{
		_rockThrow.transform.parent = _rightHand.transform;
		_rightHand.transform.localPosition = new Vector3(.2f, -0.5f, .5f);
		ShootRestart = true;
		_rockThrow.rigidbody.isKinematic = true;
		_rockThrow.transform.position = _rockThrow.transform.parent.transform.position;
		_rockThrow.transform.rotation = _rockThrow.transform.parent.transform.rotation;
		rightHandBack = true;
	}
	
	private void PutRockBack() {
		_rockThrow.rigidbody.isKinematic = true;
		_rockThrow.transform.parent = _bunch_of_rocks.transform;
		_rightHand.transform.localPosition = new Vector3(0f, 0f, 0f);
		_rockThrow.transform.position = _rockThrow.transform.parent.transform.position;
		_rockThrow.transform.rotation = _rockThrow.transform.parent.transform.rotation;
		isRockAvailable = false;
	}
	
	private void TouchEvent(){
		
		if(!lm.altCamera.enabled && !isRockAvailable){
			foreach (Touch touch in Input.touches) 
			{
	
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
	       		RaycastHit hit;
				
				if (touch.phase == TouchPhase.Began && touch.tapCount == 1)
				{	
	            	if (Physics.Raycast (ray, out hit, 5, _rockThrowLayer)) 
					{		
						GetRock();
						isRockAvailable = true;
					} 
				}						
			}
		}
		
	}
	

}
