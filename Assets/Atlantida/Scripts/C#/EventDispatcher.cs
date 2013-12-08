using UnityEngine;
using System.Collections;

public class EventDispatcher : MonoBehaviour {
	
	CameraFade cameraFade;	

	void Start () {
		cameraFade = this.gameObject.AddComponent<CameraFade>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void FadeEffect(string type){
		cameraFade.StartFade(new Color(192,192,192,0.3f), 1);
	}
	
	
	public void NewScene(GameObject theObjeto, string scene)
	{
		if(scene == null) scene = theObjeto.name;
		AutoFade.LoadLevel(scene ,0.5f,2f,Color.black);
	}
	
		
}
