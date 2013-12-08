using UnityEngine;
using System.Collections;

// Add this class to the particle colider buttons

public class KeyPad : MonoBehaviour {
	
	public Camera alt_camera;
	public AudioClip buttonSound;
	
	private AudioSource audioSource2;
	private AudioSource audioSource3;

	private Transform button;
	private bool buttonPush = false;
	
	private string[] Passcode = new string[3];
	private string[] Combination = new string[3];
	private bool CombEntered = false;
	private int count = 0; 
		
	private LayerMask KeypadButtons = 1 << 14;
	
    private float timer = 0;
	private float translationTime = 0.07f;
	Vector3 originalPos;
	
	// Use this for initialization
	void Start () {
    	AudioSource[] aSources = transform.GetComponents<AudioSource>();
		audioSource2 = aSources[1];
		audioSource3 = aSources[2];

		Combination[0] = "Keypad_btn_B";
		Combination[1] = "Keypad_btn_D";
		Combination[2] = "Keypad_btn_C";
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(alt_camera.enabled){
			
			foreach (Touch touch in Input.touches) 
			{
				Ray ray = alt_camera.ScreenPointToRay(touch.position);
	       		RaycastHit hit;
				
				if (touch.phase == TouchPhase.Began && touch.tapCount == 1)
				{	
	            	if (Physics.Raycast (ray, out hit, 2f, KeypadButtons)) 
					{	
						if(!audio.isPlaying){
							button = hit.transform;
							originalPos = button.position;
							buttonPush = true;
							audio.clip = buttonSound;
							audio.Play();
							KeyPadResult(button.name);
						}
					}
					
				}
			}
			
			if(buttonPush){
				timer += Time.deltaTime;
				if(timer <= translationTime){
					button.position = Vector3.Slerp(originalPos, new Vector3(originalPos.x, originalPos.y, originalPos.z + 0.003f), timer/translationTime);
				}else if(timer >= translationTime) {
					button.position = Vector3.Slerp(originalPos, new Vector3(originalPos.x, originalPos.y, originalPos.z), timer/translationTime);
					buttonPush = false;
				}
			}else{
				timer = 0;
			}
		
		}
	}
	
	
	private void KeyPadResult(string button_name){
		if(!CombEntered){
			Passcode[count] = button_name;
			count +=1;
			if(Passcode[0] != null && Passcode[1] != null && Passcode[2] != null) {
				if(Passcode[0] == Combination[0] && Passcode[1] == Combination[1] && Passcode[2] == Combination[2]) {
					CombEntered = true;
					StartCoroutine(KeyPadSuccessMessage());
				} else {
					audio.clip = audioSource3.clip;
					audio.Play();
					count = 0;
					Passcode[0] = null;
					Passcode[1] = null;
					Passcode[2] = null;
				}
			}
		}
	}
	
	private IEnumerator KeyPadSuccessMessage(){
		yield return new WaitForSeconds (1.3f);
			audio.clip = audioSource2.clip;
			audio.Play();
	}
	
	
}
