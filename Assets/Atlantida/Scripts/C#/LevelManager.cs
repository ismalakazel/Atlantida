using UnityEngine;
using System.Collections;

public class LevelManager : GameManager<LevelManager> {
	
	SaveGame sv;
	public Transform player;
	public Transform spawn;
	public Camera mainCamera;
	public Camera altCamera;
	public Camera rCamera;
	public Joysticks moveTouchPad;
	public Joysticks rotateTouchPad;
	
	// Lights
	public Light sunLight;
	public Color slColorA, slColorB = Color.black;
	public Light cookieLight;
	//
	
	// SkyBox
	public Material skyboxA, skyboxB;
	//
		
	private void Start () {
		sv = gameObject.AddComponent<SaveGame>();
		//sv.loadItems();
		altCamera.enabled = false;
		StartCoroutine(RenderCamera());
		if(Manager.playerPos != new Vector3(0,0,0)) 
		{
			player.position = Manager.playerPos;
			player.rotation = Manager.playerRot;
		} else {
			player.position = spawn.position;
			player.rotation = spawn.rotation;
		}
		mainCamera.transform.position = player.position;
		mainCamera.transform.rotation = player.rotation;
		slColorA = sunLight.color;
	}
	
	private void Update () {
	
	}
	
	private void FixedUpdate() {
		//Manager.playerPos = player.position;
		//Manager.playerRot = player.rotation;
	}
	
	IEnumerator RenderCamera(){
		rCamera.transform.gameObject.SetActive(true);
		rCamera.enabled = false;
		yield return new WaitForSeconds(3);
			rCamera.transform.gameObject.SetActive(false);
	}
	
	private void OnApplicationQuit() {
		sv.saveLevel();
	}
	
	private void OnApplicationPause(bool pause) {
        if(pause) {
		} else {
		}
    }
}
