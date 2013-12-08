using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	
	LevelManager lm;
	private Transform _target;
	private float _moveSpeed = 3f;
	private float _rotationSpeed = 1f;
	private Vector3 _startPos;
	private float _elapsedTime = 0f;
	private bool _canCameraGo = false;
	private bool _canCameraGoBack = false;
	private bool _canPinchExit = false;
	private AudioSource _whooshSource;
	private bool _isAudioClipPlaying = false;
	
	void Start () {	
		lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
		lm.altCamera.transform.gameObject.SetActive(false);
		_whooshSource = gameObject.AddComponent<AudioSource>();
	}
	
	void Update () {
		
		if(_canCameraGo)
		{
   			if (!_isAudioClipPlaying) 
			StartCoroutine(CameraWhoosh("go"));
			GoCamera(AltCamPos(_target.name));
			_elapsedTime += Time.deltaTime;
			
		} 
		else if(_canCameraGoBack) 
		{
   			if (!_isAudioClipPlaying)
    		StartCoroutine(CameraWhoosh("back"));
			BackCamera();
			_elapsedTime += Time.deltaTime;
		}
		
		if(_canPinchExit)
		{
			foreach (Touch touch in Input.touches) 
			{
				if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
					_canCameraGoBack = true;
				if (Input.touchCount == 1)
				{
					
				}
			}
		}
	}
	
	public void Settings(Transform item)
	{ 
		lm.altCamera.transform.position = lm.mainCamera.transform.position;
		lm.altCamera.transform.rotation = lm.mainCamera.transform.rotation;
		lm.altCamera.transform.gameObject.SetActive(true);
		lm.altCamera.enabled = true;		
		_startPos = lm.mainCamera.transform.position;
		_target = item;
		_canCameraGo = true;
		RemoveJoystick();
	}
	
	
	public Vector3 AltCamPos(string item)
	{	
		Vector3 moveTo = new Vector3();
		switch (item)
		{		
			case "_keypad_col":
				moveTo = new Vector3(-0.8179191f, 3.020854f, -0.562142f);
			break;
			
			case "_parchment_col":
				moveTo = new Vector3(-5.709576f, 2.366149f, 9.88822f);
			break;
			
			case "_calendar_col":
				moveTo = new Vector3(1.729705f, 2.375716f, -10.01958f);
			break;
		}
		
		return moveTo;
	}
	
	private void GoCamera(Vector3 newPosition)
	{	

		// Camera to Destination position
		lm.altCamera.transform.position = Vector3.Lerp(
			lm.altCamera.transform.position, 
			newPosition, 
			Interpolate.EaseInQuad(
				0f, 
				4f, 
				_elapsedTime, 
				_moveSpeed
			)
		);
		
		// Camera to target rotation
		Vector3 newRotation = Quaternion.LookRotation(_target.position - lm.altCamera.transform.position).eulerAngles;
		lm.altCamera.transform.rotation = Quaternion.Slerp(
			lm.altCamera.transform.rotation, 
			Quaternion.Euler(newRotation), 
			Interpolate.EaseInQuad(
				0f, 
				4f, 
				_elapsedTime, 
				_rotationSpeed
			)
		);
		
		// Fix rotation of Camera.main
		Vector3 fixRot = Quaternion.LookRotation(_target.position - lm.mainCamera.transform.position).eulerAngles;
		lm.mainCamera.transform.rotation = Quaternion.Slerp(
			lm.mainCamera.transform.rotation, 
			Quaternion.Euler(fixRot), 
			Interpolate.EaseInQuad(
				0f, 
				4f, 
				_elapsedTime, 
				_rotationSpeed
			)
		);
			
		if(_elapsedTime >= _rotationSpeed)
		{
			_elapsedTime = 0;
			_canCameraGo = false;
			_canPinchExit = true;
		}
										
	}
	
	private void BackCamera()
	{
		// Camera back to original position
		lm.altCamera.transform.position = Vector3.Lerp
		(
			lm.altCamera.transform.position, 
			_startPos, 
			Interpolate.EaseInQuad(0f, 4f, _elapsedTime, _moveSpeed)
		);
		
		lm.altCamera.transform.rotation = Quaternion.Slerp
		(
			lm.altCamera.transform.rotation, 
			lm.mainCamera.transform.rotation, 
			Interpolate.EaseInQuad(0f, 4f, _elapsedTime, _rotationSpeed)
		);
		
	
		if(_elapsedTime >= _rotationSpeed)
		{	
			_elapsedTime = 0;
			_canCameraGoBack = false;
			_canPinchExit = false;
			lm.altCamera.enabled = false;
			lm.altCamera.transform.gameObject.SetActive(false);
			BringJoystick();
		}	
		
	}
	
	private IEnumerator CameraWhoosh( string type)
	{
	    _isAudioClipPlaying = true;
		if(type == "go"){
			_whooshSource.clip = Resources.Load("SoundFX/Whoosh2") as AudioClip;
			_whooshSource.priority = 0;
		}
		else{
			_whooshSource.clip = Resources.Load("SoundFX/Whoosh3") as AudioClip;
			_whooshSource.priority = 0;
		}
	    audio.PlayOneShot( _whooshSource.clip );
		yield return new WaitForSeconds( _whooshSource.clip.length + 0.2f);
	    _isAudioClipPlaying = false;
	}
	
	public void RemoveJoystick() {
		lm.moveTouchPad.Disable();
		lm.rotateTouchPad.Disable();
	}
	
	public void BringJoystick() {
		lm.moveTouchPad.Restart();
		lm.rotateTouchPad.Restart();
		lm.moveTouchPad.Enable();
		lm.rotateTouchPad.Enable();
	}

}
