using UnityEngine;
using System.Collections;

public class CalPointerCheck : MonoBehaviour {
	
	LevelManager lm;
	private bool isCalACheck, isCalBCheck, isCalCCheck, isNight, isCalActive = false;
	private float duration = 2.0F;

	
	void Start () {
		lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
		if(!isCalActive)
		{
			isCalACheck = false;
			isCalBCheck = false;
			isCalCCheck = false;
			isNight = false;
		}
	}
	
	
	void Update () {
		if(isCalActive && !isNight) 
		{
			lm.sunLight.intensity -= duration * Time.deltaTime;
			if(lm.sunLight.intensity <= 0.1f){ Night(); }
			
		} 
		else if (!isCalActive && isNight) 
		{
			lm.sunLight.intensity += duration * Time.deltaTime;
			if(lm.sunLight.intensity >= 0.35f){ Day(); lm.sunLight.intensity = 0.35f;}
		}
	}
	
	private void Night() {
		
		isNight = true;
		lm.cookieLight.gameObject.SetActive (true);
		RenderSettings.skybox = lm.skyboxB;
	}

	private void Day() {
		
		isNight = false;
		lm.cookieLight.gameObject.SetActive (false);
		RenderSettings.skybox = lm.skyboxA;
	}
	
    void OnTriggerEnter(Collider collider) {
		if(collider.name == "_cal_a_p_check") 
		{
			isCalActive = true;
		}
	}
	
	void OnTriggerExit(Collider collider) {
		if(collider.name == "_cal_a_p_check") 
		{
			isCalActive = false;
		}
	}
}
